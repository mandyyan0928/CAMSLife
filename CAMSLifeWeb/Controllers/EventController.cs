using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.API.Event.Request;
using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.Models.ViewModel;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CaliphWeb.Controllers
{
    public class EventController : Controller
    {
        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;

        public EventController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
        }

        #region Add/Edit Event

        public async Task<ActionResult> Add()
        {
            var addEventViewModel = new AddEventViewModel
            {
                EventChannelList = await _masterService.GetEventChannelAsync(),
                EventHostList = await _masterService.GetEventHostAsync(),
                EventTypeList = await _masterService.GetEventTypeAsync(),
                AttendantTypeList = await _masterService.GetEventAttendantTypeAsync()
            };

            if (TempData["Attachment"] != null)
            {
                TempData.Remove("Attachment");
            }

            return View(addEventViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Add(FormCollection formCollection)
        {
            var eventDates = JsonConvert.DeserializeObject<List<EventDatesViewModel>>(formCollection["EventDates"].ToString());
            var eventRoles = JsonConvert.DeserializeObject<List<int>>(formCollection["EventRoleIds"].ToString());

            var model = FormCollectionMapper.FormToModel<AddEventRequest>(formCollection);
            model.CreatedBy = UserHelper.GetLoginUser();
            model.EventRoleIds = eventRoles;
            var response = await _caliphAPIHelper.PostAsync<AddEventRequest, ResponseData<string>>(model, "/api/v1/event/add");

            int eventId = Convert.ToInt32(response.Data);
            await AddEditEventDate(eventDates, eventId, model.CreatedBy);
            await AddEventAttachment(eventId, model.CreatedBy);

            return Json(response);
        }

        public async Task AddEditEventDate(List<EventDatesViewModel> eventDates, int eventId, string createdBy)
        {
            if (eventDates != null && eventDates.Count > 0)
            {
                foreach (var item in eventDates)
                {
                    String[] dataRangeSeperator = { " - " };
                    String[] singleDateList = item.FromDate.Split(dataRangeSeperator, StringSplitOptions.RemoveEmptyEntries);

                    CultureInfo culture = new CultureInfo("en-US");

                    EventDateRequest eventDateRequest = new EventDateRequest
                    {
                        EventId = eventId,
                        EventDateFrom = Convert.ToDateTime(singleDateList[0], culture),
                        EventDateTo = Convert.ToDateTime(singleDateList[1], culture),
                        RegClosingDate = Convert.ToDateTime(item.RegCloseDate, culture),
                        CreatedBy = createdBy
                    };

                    if (item.EventDateId == 0)
                    {
                        eventDateRequest.EventId = eventId;
                        eventDateRequest.CreatedBy = createdBy;
                        await _caliphAPIHelper.PostAsync<EventDateRequest, ResponseData<string>>(eventDateRequest, "/api/v1/event/date-add");
                    }
                    else
                    {
                        eventDateRequest.EventDateId = item.EventDateId;
                        eventDateRequest.UpdatedBy = createdBy;
                        await _caliphAPIHelper.PostAsync<EventDateRequest, ResponseData<string>>(eventDateRequest, "/api/v1/event/date-update");
                    }
                }
            }
        }

        public async Task SoftDeleteEventDate(List<EventDatesViewModel> eventDates, string updatedBy)
        {
            if (eventDates != null && eventDates.Count > 0)
            {
                foreach (var item in eventDates)
                {
                    EventDateRequest eventDateRequest = new EventDateRequest
                    {
                        EventDateId = item.EventDateId,
                        UpdatedBy = updatedBy
                    };

                    await _caliphAPIHelper.PostAsync<EventDateRequest, ResponseData<string>>(eventDateRequest, "/api/v1/event/date-delete");
                }
            }
        }

        public async Task AddEventAttachment(int eventId, string createdBy)
        {
            List<EventAttachmentViewModel> eventAttachments = new List<EventAttachmentViewModel>();

            if (TempData["Attachment"] != null)
            {
                eventAttachments = TempData["Attachment"] as List<EventAttachmentViewModel>;
            }

            foreach (var item in eventAttachments.Where(x => x.IsDeleted == false && x.EventAttachmentId == 0).ToList())
            {
                var eventAttachmentRequest = new EventAttachmentRequest
                {
                    EventId = eventId,
                    EventAttachmentName = item.EventAttachmentName,
                    EventAttachmentPath = item.EventAttachmentPath,
                    Remarks = item.Remarks,
                    CreatedBy = createdBy
                };

                await _caliphAPIHelper.PostAsync<EventAttachmentRequest, ResponseData<string>>(eventAttachmentRequest, "/api/v1/event/attachment-add");
            }

            foreach (var item in eventAttachments.Where(x => x.IsDeleted == true && x.EventAttachmentId > 0).ToList())
            {
                var eventAttachmentRequest = new EventAttachmentRequest
                {
                    EventAttachmentId = item.EventAttachmentId,
                    UpdatedBy = createdBy
                };

                await _caliphAPIHelper.PostAsync<EventAttachmentRequest, ResponseData<string>>(eventAttachmentRequest, "/api/v1/event/attachment-delete");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile()
        {
            List<EventAttachmentViewModel> eventAttachments = new List<EventAttachmentViewModel>();

            if (TempData["Attachment"] != null)
            {
                eventAttachments = TempData["Attachment"] as List<EventAttachmentViewModel>;
            }

            if (Request.Files != null)
            {
                try
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            // get a stream
                            var stream = fileContent.InputStream;
                            // and optionally write the file to disk
                            string ext = Path.GetExtension(fileContent.FileName);
                            string path = "/FileUpload/Event/";


                            if (!Directory.Exists(Server.MapPath("~" + path)))
                                Directory.CreateDirectory(Server.MapPath("~" + path));


                            var tempFileName = $@"{DateTime.Now.Ticks}{ext}";
                            var serverPath = Path.Combine(Server.MapPath("~" + path) + tempFileName);
                            using (var fileStream = System.IO.File.Create(serverPath))
                            {
                                await stream.CopyToAsync(fileStream);
                            }

                            eventAttachments.Add(new EventAttachmentViewModel
                            {
                                EventAttachmentName = tempFileName,
                                EventAttachmentPath = path,
                                EventAttachmentFullName = path + tempFileName
                            });
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            TempData["Attachment"] = eventAttachments;

            return PartialView("_EventAttachment", eventAttachments);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAttachment(string name)
        {
            List<EventAttachmentViewModel> eventAttachments = new List<EventAttachmentViewModel>();

            if (TempData["Attachment"] != null)
            {
                eventAttachments = TempData["Attachment"] as List<EventAttachmentViewModel>;
            }

            eventAttachments.Where(x => x.EventAttachmentFullName == name).FirstOrDefault().IsDeleted = true;

            TempData["Attachment"] = eventAttachments;

            return PartialView("_EventAttachment", eventAttachments);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var editEventViewModel = new EditEventViewModel();

            var eventFilterViewModel = new EventFilterViewModel
            {
                EventId = id,
                PageNumber = 1,
                PageSize = 10,
                EventDateFrom = new DateTime(2022, 1, 1),
                EventDateTo = new DateTime(2122, 1, 1)
            };

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/get-by-filter");
            if (responseData.Data != null && responseData.Data.Count > 0)
            {
                var eventResult = responseData.Data.FirstOrDefault();
                editEventViewModel = AutoMapHelper.Map<EventListResponse, EditEventViewModel>(eventResult);
            }

            var eventDateFilterViewModel = new EventFilterViewModel
            {
                EventId = id,
                PageNumber = 1,
                PageSize = 10
            };

            var eventDateList = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventDateListResponse>>>(eventDateFilterViewModel, "/api/v1/event/date-get-by-filter");
            if (eventDateList.Data != null && eventDateList.Data.Count > 0)
            {
                editEventViewModel.EventDates = AutoMapHelper.MapList<EventDateListResponse, EventDateListResponse>(eventDateList.Data);

                if (TempData["EventDates"] != null)
                {
                    TempData.Remove("EventDates");
                }

                TempData["EventDates"] = JsonConvert.SerializeObject(editEventViewModel.EventDates);
            }

            var eventAttachmentList = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventAttachmentListResponse>>>(eventDateFilterViewModel, "/api/v1/event/attachment-get-by-filter");
            if (eventAttachmentList.Data != null && eventAttachmentList.Data.Count > 0)
            {
                editEventViewModel.EventAttachments = AutoMapHelper.MapList<EventAttachmentListResponse, EventAttachmentViewModel>(eventAttachmentList.Data);
                editEventViewModel.EventAttachments.ForEach(x => x.EventAttachmentFullName = x.EventAttachmentPath + x.EventAttachmentName);

                if (TempData["Attachment"] != null)
                {
                    TempData.Remove("Attachment");
                }

                TempData["Attachment"] = editEventViewModel.EventAttachments;
            }

            editEventViewModel.EventChannelList = await _masterService.GetEventChannelAsync();
            editEventViewModel.EventHostList = await _masterService.GetEventHostAsync();
            editEventViewModel.EventTypeList = await _masterService.GetEventTypeAsync();
            editEventViewModel.AttendantTypeList = await _masterService.GetEventAttendantTypeAsync();

            return View(editEventViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateEvent(FormCollection formCollection)
        {
            var eventDates = JsonConvert.DeserializeObject<List<EventDatesViewModel>>(formCollection["EventDates"].ToString());

            var updateEventRequest = FormCollectionMapper.FormToModel<UpdateEventRequest>(formCollection);
            updateEventRequest.UpdatedBy = UserHelper.GetLoginUser();

            var oriEventDates = JsonConvert.DeserializeObject<List<EventDatesViewModel>>(TempData["EventDates"].ToString());
            var deletedEventDates = oriEventDates.Where(p => eventDates.All(p2 => p2.EventDateId != p.EventDateId)).ToList();
           
            
            var eventRoles = JsonConvert.DeserializeObject<List<int>>(formCollection["EventRoleIds"].ToString());
            updateEventRequest.EventRoleIds = eventRoles;
            var response = await _caliphAPIHelper.PostAsync<UpdateEventRequest, ResponseData<string>>(updateEventRequest, "/api/v1/event/update");

            await AddEditEventDate(eventDates, updateEventRequest.EventId, updateEventRequest.UpdatedBy);
            await SoftDeleteEventDate(deletedEventDates, updateEventRequest.UpdatedBy);

            await AddEventAttachment(updateEventRequest.EventId, updateEventRequest.UpdatedBy);

            return Json(response);
        }

        #endregion

        #region Event List

        public async Task<ActionResult> List()
        {
            var eventListViewModel = new EventListViewModel
            {
                EventHostList = await _masterService.GetEventHostAsync(),
                EventTypeList = await _masterService.GetEventTypeAsync()
            };

            var eventFilterViewModel = new EventFilterViewModel
            {
                PageNumber = 1,
                PageSize = 10,
                EventDateFrom = new DateTime(2000, 1, 1),
                EventDateTo = new DateTime(2100, 12, 1)
            };
            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/get-by-filter");
            eventListViewModel.EventList.EventList = responseData.Data;
            eventListViewModel.EventList.Paging.ItemCount = responseData.ItemCount;
            eventListViewModel.EventList.Paging.PageSize = eventFilterViewModel.PageSize;
            eventListViewModel.EventList.Paging.CurrentPage = eventFilterViewModel.PageNumber;

            return View(eventListViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> List(EventFilterViewModel eventFilterViewModel)
        {
            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/get-by-filter");
            var eventListDataViewModel = new EventListDataViewModel();
            eventListDataViewModel.EventList = responseData.Data;
            eventListDataViewModel.Paging.ItemCount = responseData.ItemCount;
            eventListDataViewModel.Paging.PageSize = eventFilterViewModel.PageSize;
            eventListDataViewModel.Paging.CurrentPage = eventFilterViewModel.PageNumber;
            return PartialView("_EventListTable", eventListDataViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ViewAttendanceList(EventFilterViewModel eventFilterViewModel)
        {

            eventFilterViewModel.StatusId = (int)MasterDataEnum.Status.Going;
            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventAttendanceListResponse>>>(eventFilterViewModel, "/api/v1/event/user-get-by-filter");

            var eventListDataViewModel = new EventAttendanceListDataViewModel();
            //Select attend (statusid = 168) agent only
            eventListDataViewModel.EventAttendanceList = responseData.Data;
            eventListDataViewModel.Paging.ItemCount = eventListDataViewModel.EventAttendanceList.Count;
            eventListDataViewModel.Paging.PageSize = eventFilterViewModel.PageSize;
            eventListDataViewModel.Paging.CurrentPage = eventFilterViewModel.PageNumber;
            eventListDataViewModel.EventDateId = eventFilterViewModel.EventDateId;
            eventListDataViewModel.EventId = eventFilterViewModel.EventId;
            return PartialView("_EventAttendanceListTable", eventListDataViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ViewFullAttendanceList(EventFilterViewModel eventFilterViewModel)
        {
            eventFilterViewModel.StatusId = (int)MasterDataEnum.Status.Going;

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventAttendanceListResponse>>>(eventFilterViewModel, "/api/v1/event/user-get-by-filter");
            var eventListDataViewModel = new EventAttendanceListDataViewModel();
            //Select attend (statusid = 168) agent only
            eventListDataViewModel.EventAttendanceList = responseData.Data;
            eventListDataViewModel.Paging.ItemCount = responseData.ItemCount;
            eventListDataViewModel.Paging.PageSize = eventFilterViewModel.PageSize;
            eventListDataViewModel.Paging.CurrentPage = eventFilterViewModel.PageNumber;
            return PartialView("_EventFullAttendanceListTable", eventListDataViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateUserEvent(EventUserViewModel eventUserViewModel)
        {
            UpdateUserEventRequest updateUserEventRequest = new UpdateUserEventRequest()
            {
                UserEventId = eventUserViewModel.UserEventId,
                EventDateId = eventUserViewModel.EventDateId,
                AttendanceId = eventUserViewModel.AttendanceId,
                QuizScoreId = eventUserViewModel.QuizScoreId,
                CPDPoint = eventUserViewModel.CPDPoint,
                IsEmailSent = eventUserViewModel.IsEmailSent,
                Remarks = eventUserViewModel.Remarks,
                UpdatedBy = UserHelper.GetLoginUser()
            };

            var response = await _caliphAPIHelper.PostAsync<UpdateUserEventRequest, ResponseData<string>>(updateUserEventRequest, "/api/v1/event/user-update");


            
            return Json(response);
        }

        #endregion

        #region Upcoming Event

        public async Task<ActionResult> Info(int id)
        {
            var editEventViewModel = new EditEventViewModel();

            var eventFilterViewModel = new EventFilterViewModel
            {
                EventId = id,
                PageNumber = 1,
                PageSize = 10,
                EventDateFrom = new DateTime(2022, 1, 1),
                EventDateTo = new DateTime(2122, 1, 1)
            };

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/get-by-filter");
            if (responseData.Data != null && responseData.Data.Count > 0)
            {
                var eventResult = responseData.Data.FirstOrDefault();
                editEventViewModel = AutoMapHelper.Map<EventListResponse, EditEventViewModel>(eventResult);
            }

            var eventDateFilterViewModel = new EventFilterViewModel
            {
                EventId = id,
                PageNumber = 1,
                PageSize = 10
            };

            var eventDateList = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventDateListResponse>>>(eventDateFilterViewModel, "/api/v1/event/date-get-by-filter");
            if (eventDateList.Data != null && eventDateList.Data.Count > 0)
            {
                editEventViewModel.EventDates = AutoMapHelper.MapList<EventDateListResponse, EventDateListResponse>(eventDateList.Data);
            }

            var eventAttachmentList = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventAttachmentListResponse>>>(eventDateFilterViewModel, "/api/v1/event/attachment-get-by-filter");
            if (eventAttachmentList.Data != null && eventAttachmentList.Data.Count > 0)
            {
                editEventViewModel.EventAttachments = AutoMapHelper.MapList<EventAttachmentListResponse, EventAttachmentViewModel>(eventAttachmentList.Data);
                editEventViewModel.EventAttachments.ForEach(x => x.EventAttachmentFullName = x.EventAttachmentPath + x.EventAttachmentName);
            }

            if (Request["f"] != null)
            {
                editEventViewModel.IsFromCPD = Request["f"].ToString() == "cpd" ? true : false;
            }

            return View(editEventViewModel);
        }

        public async Task<ActionResult> UpcomingEvent()
        {
            var eventListViewModel = new EventListViewModel
            {
                EventHostList = await _masterService.GetEventHostAsync(),
                EventTypeList = await _masterService.GetEventTypeAsync()
            };
            var user = UserHelper.GetLoginUserViewModel();
            var eventFilterViewModel = new EventFilterViewModel
            {
                PageNumber = 1,
                PageSize = 10,
                UserEventUserId = user.UserId,
               
                RoleId= user.RoleId
            };

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/upcoming-get-by-filter");
            eventListViewModel.EventList.EventList = responseData.Data;
            eventListViewModel.EventList.Paging.ItemCount = responseData.ItemCount;
            eventListViewModel.EventList.Paging.PageSize = eventFilterViewModel.PageSize;
            eventListViewModel.EventList.Paging.CurrentPage = eventFilterViewModel.PageNumber;

            return View(eventListViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpcomingEvent(EventFilterViewModel eventFilterViewModel)
        {
            eventFilterViewModel.UserEventUserId = UserHelper.GetLoginUserViewModel().UserId;

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/upcoming-get-by-filter");
            var eventListDataViewModel = new EventListDataViewModel();
            eventListDataViewModel.EventList = responseData.Data;
            eventListDataViewModel.Paging.ItemCount = responseData.ItemCount;
            eventListDataViewModel.Paging.PageSize = eventFilterViewModel.PageSize;
            eventListDataViewModel.Paging.CurrentPage = eventFilterViewModel.PageNumber;
            return PartialView("_UpcomingEventListTable", eventListDataViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateAttendance(int eventDateId, int eventId, int statusId)
        {
            int userEventId = 0;
            var user = UserHelper.GetLoginUserViewModel();
            var eventFilterViewModel = new EventFilterViewModel
            {
                EventId = eventId,
                PageNumber = 1,
                PageSize = 10,
                UserEventUserId = user.UserId,
                RoleId = user.RoleId
            };

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/upcoming-get-by-filter");

            if (responseData.Data != null && responseData.Data.Count > 0)
            {
                var joinEvent = responseData.Data.Where(x => x.EventDateId == eventDateId).FirstOrDefault();

                if (joinEvent != null)
                {
                    if (joinEvent.UserEventId == null)
                    {
                        var model = new AddEventUserRequest()
                        {
                            //default attendance not going & quiz failed
                            UserId = UserHelper.GetLoginUserViewModel().UserId,
                            EventDateId = eventDateId,
                            AttendanceId = 158,
                            QuizScoreId = 163,
                            CPDPoint = joinEvent.CPDPoint,
                            IsEmailSent = false,
                            Remarks = "",
                            CreatedBy = UserHelper.GetLoginUser()
                        };

                        var response = await _caliphAPIHelper.PostAsync<AddEventUserRequest, ResponseData<string>>(model, "/api/v1/event/user-add");
                        userEventId = Convert.ToInt32(response.Data);
                    }
                    else
                    {
                        userEventId = (int)joinEvent.UserEventId;
                    }

                    // if status is going
                    if (joinEvent.EventFees > 0 && statusId == 168)
                    {
                        statusId = 174;// pending payment
                    }

                    var updateUserStatus = new AddEventUserRequest()
                    {
                        UserEventId = userEventId,
                        StatusId = statusId,
                        UpdatedBy = UserHelper.GetLoginUser()
                    };

                    await _caliphAPIHelper.PostAsync<AddEventUserRequest, ResponseData<string>>(updateUserStatus, "/api/v1/event/user-update-status");
                }
            }

            return Json(new { statusId = statusId, userEventId = userEventId });
        }

        #endregion

        #region Payment List

        public async Task<ActionResult> PaymentList()
        {
            var eventPaymentListViewModel = new EventPaymentListViewModel
            {
                EventPaymentStatusList = await _masterService.GetEventPaymentStatusAsync()
            };

            var eventFilterViewModel = new EventFilterViewModel
            {
                PageNumber = 1,
                PageSize = 10,
            };

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventPaymentListResponse>>>(eventFilterViewModel, "/api/v1/event/user-payment-get-by-filter");
            eventPaymentListViewModel.EventPaymentList.EventPaymentList = responseData.Data;
            eventPaymentListViewModel.EventPaymentList.Paging.ItemCount = responseData.ItemCount;
            eventPaymentListViewModel.EventPaymentList.Paging.PageSize = eventFilterViewModel.PageSize;
            eventPaymentListViewModel.EventPaymentList.Paging.CurrentPage = eventFilterViewModel.PageNumber;

            return View(eventPaymentListViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> PaymentList(EventFilterViewModel eventFilterViewModel)
        {
            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventPaymentListResponse>>>(eventFilterViewModel, "/api/v1/event/user-payment-get-by-filter");
            var eventPaymentListDataViewModel = new EventPaymentListDataViewModel();
            eventPaymentListDataViewModel.EventPaymentList = responseData.Data;
            eventPaymentListDataViewModel.Paging.ItemCount = responseData.ItemCount;
            eventPaymentListDataViewModel.Paging.PageSize = eventFilterViewModel.PageSize;
            eventPaymentListDataViewModel.Paging.CurrentPage = eventFilterViewModel.PageNumber;
            return PartialView("_PaymentListTable", eventPaymentListDataViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePayment(int eventPaymentId, int status, int usereventId)
        {
            EventPaymentRequest eventPaymentRequest = new EventPaymentRequest()
            {
                UserEventPaymentId = eventPaymentId,
                PayementStatusId = status,
                Remarks = "",
                UpdatedBy = UserHelper.GetLoginUser()
            };

            var response = await _caliphAPIHelper.PostAsync<EventPaymentRequest, ResponseData<string>>(eventPaymentRequest, "/api/v1/event/user-payment-update");

            if (status == 181)// payment success
            {
                var updateUserStatus = new AddEventUserRequest()
                {
                    UserEventId = usereventId,
                    StatusId = 168, // going
                    UpdatedBy = UserHelper.GetLoginUser()
                };

                await _caliphAPIHelper.PostAsync<AddEventUserRequest, ResponseData<string>>(updateUserStatus, "/api/v1/event/user-update-status");
            }

            return Json(response);
        }

        #endregion

        #region CPD Earned

        public async Task<ActionResult> CPDEarned()
        {
            var eventListViewModel = new EventListViewModel
            {
                EventHostList = await _masterService.GetEventHostAsync(),
                EventTypeList = await _masterService.GetEventTypeAsync()
            };

            var dateNow = DateTime.Now;
            var startDate = new DateTime(dateNow.Year, dateNow.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var eventFilterViewModel = new EventFilterViewModel
            {
                PageNumber = 1,
                PageSize = 10,
                UserId = UserHelper.GetLoginUserViewModel().UserId,
                StatusId= (int)MasterDataEnum.Status.Going,
                EventDateFrom = startDate,
                EventDateTo = endDate,

            };

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/upcoming-get-by-filter");
            eventListViewModel.EventList.Paging.ItemCount = eventListViewModel.EventList.EventList.Count;
            eventListViewModel.EventList.Paging.PageSize = eventFilterViewModel.PageSize;
            eventListViewModel.EventList.Paging.CurrentPage = eventFilterViewModel.PageNumber;

            return View(eventListViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CPDEarned(EventFilterViewModel eventFilterViewModel)
        {
            eventFilterViewModel.UserId = UserHelper.GetLoginUserViewModel().UserId;
            eventFilterViewModel.StatusId = (int)MasterDataEnum.Status.Going;
            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/upcoming-get-by-filter");
            var eventListDataViewModel = new EventListDataViewModel();
            eventListDataViewModel.Paging.ItemCount = eventListDataViewModel.EventList.Count;
            eventListDataViewModel.Paging.PageSize = eventFilterViewModel.PageSize;
            eventListDataViewModel.Paging.CurrentPage = eventFilterViewModel.PageNumber;
            return PartialView("_CPDEarnedListTable", eventListDataViewModel);
        }

        #endregion


        public async Task<ActionResult> ManualAttendance()
        {
            var vm = new ManualAttendanceViewModel
            {
                QuizScores = await _masterService.GetEventQuizScoreAsync(),
            };

            var req = new GetAgentRequest { RoleId = (int)MasterDataEnum.RoleId.Agent };
            var response = await _caliphAPIHelper.PostAsync<GetAgentRequest, ResponseData<List<AgentUser>>>(req, "/api/v1/agent/get-by-filter");
            if (response != null && response.Data != null)
                vm.Agents.AddRange(response.Data);

            req = new GetAgentRequest { RoleId = (int)MasterDataEnum.RoleId.Leader };
            response = await _caliphAPIHelper.PostAsync<GetAgentRequest, ResponseData<List<AgentUser>>>(req, "/api/v1/agent/get-by-filter");
            if (response != null && response.Data != null)
                vm.Agents.AddRange(response.Data);


            req = new GetAgentRequest { RoleId = (int)MasterDataEnum.RoleId.PontentialAgent };
            response = await _caliphAPIHelper.PostAsync<GetAgentRequest, ResponseData<List<AgentUser>>>(req, "/api/v1/agent/get-by-filter");
            if (response != null && response.Data != null)
                vm.Agents.AddRange(response.Data);

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(new EventFilterViewModel { PageNumber=1, PageSize=99999}, "/api/v1/event/get-by-filter");
            if (responseData != null && responseData.Data != null)
                vm.Events.AddRange(responseData.Data);

            return View(vm);
        }

        [HttpPost]
        public async Task<JsonResult> ManualAttendance(FormCollection formCollection)
        {

            var model = FormCollectionMapper.FormToModel<AddEventUserRequest>(formCollection);
            model.CreatedBy = UserHelper.GetLoginUser();

            var user = UserHelper.GetLoginUserViewModel();


            // find event exists or not 
            var eventFilterViewModel = new EventFilterViewModel
            {
                EventDateId = model.EventDateId,
                EventId = model.EventId,
                PageNumber = 1,
                PageSize = 10,
                UserId = model.UserId,
            };
            //
            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/get-by-filter");
            if (responseData.Data == null || responseData.Data.Count == 0)
            {
                return Json(new ResponseData<string> { Data = "event not found, please contact admin" });
            }


            // find user event 
            var userEventData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventAttendanceListResponse>>>(eventFilterViewModel, "/api/v1/event/user-get-by-filter");
            if (userEventData.Data != null && userEventData.Data.Count > 0)
            {
                return Json(new ResponseData<string> { Data = "duplicate record , agent already joined this training event" });
            }

            var joinEvent = responseData.Data.FirstOrDefault();
            if (joinEvent.UserEventId == null)
            {
                model.CPDPoint = joinEvent.CPDPoint;
                model.AttendanceId = (int)MasterDataEnum.Status.Attended;
                model.StatusId = (int)MasterDataEnum.Status.Going;
                var response = await _caliphAPIHelper.PostAsync<AddEventUserRequest, ResponseData<string>>(model, "/api/v1/event/user-add");
                return Json(response);
            }
            else
            {
                return Json(new ResponseData<string> { Data = "event not found, please contact admin" });
            }

        }


        [HttpPost]
        public async Task<JsonResult> GetEventDate(int id)
        {

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventDateListResponse>>>(new EventFilterViewModel { PageNumber = 1, PageSize = 99999, EventId= id }, "/api/v1/event/date-get-by-filter");
            if (responseData != null && responseData.Data != null)
            {
                var list = new List<EventDateSelect>();
                foreach (var d in responseData.Data)
                {
                    list.Add(new EventDateSelect
                    {
                        EventDate = d.EventDateFrom.ToString("dd-MMM-yyyy hh:mm:ss"),
                        EventDateId = d.EventDateId

                    });
                }
                return Json(list);
            }
            else
            {
                return Json(new List<EventDateSelect>());
            }

           
        }
    }
}