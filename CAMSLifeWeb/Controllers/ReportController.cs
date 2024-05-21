using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Models.API.AgentRecruit;
using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.Models.API.Report;
using CaliphWeb.Models.ViewModel;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using ClosedXML.Excel;
using OfficeOpenXml;
using Rotativa;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CaliphWeb.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {

        private readonly IMasterDataService _masterDataService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;

        public ReportController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService)
        {
            this._masterDataService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
        }
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Project100()
        {

            var view = new Project100ViewModel();
            view.Agents = await _userService.GetAgentUserAsync();

            return View(view);
        }
        public async Task<ActionResult> Project100Recruit()
        {

            var view = new Project100RecruitmentViewModel();
            view.Agents = await _userService.GetAgentUserAsync();

            return View(view);
        }
        public ActionResult ExportProject100()
        {
            return View();
        }

        public async Task<ActionResult> KIVHistory()
        {
            var vm = new RevertKIVReportViewModel();
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var filter = new ClientFilterRequest { PageNumber = 1, PageSize = 999, CreatedBy = UserHelper.GetDefaultSearchUser(), };
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-by-client-filter");
            vm.Clients = responseData.Data.Where(x => x.StatusId != (int)MasterDataEnum.Status.Inactive).ToList();

            var kivReq = new GetRevertKIVRequest { PageNumber = 1, PageSize = 10, CreatedBy = UserHelper.GetDefaultSearchUser(), KIVDateFrom = startDate, KIVDateTo = endDate };
            var kivRes = await _caliphAPIHelper.PostAsync<GetRevertKIVRequest, ResponseData<List<KIVRevertHistory>>>(kivReq, "/api/v1/report/get-kiv-revert-history");

            vm.RevertKIVData.KIVRevertHistories = kivRes.Data ?? new List<KIVRevertHistory>();
            vm.RevertKIVData.Paging.ItemCount = kivRes.ItemCount;
            vm.RevertKIVData.Paging.PageSize = kivReq.PageSize;
            vm.RevertKIVData.Paging.CurrentPage = kivReq.PageNumber;
            vm.Users = await _userService.GetAgentUserAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> KIVHistory(GetRevertKIVRequest kivReq)
        {
            var kivRes = await _caliphAPIHelper.PostAsync<GetRevertKIVRequest, ResponseData<List<KIVRevertHistory>>>(kivReq, "/api/v1/report/get-kiv-revert-history");
            var data = new RevertKIVData();
            data.KIVRevertHistories = kivRes.Data ?? new List<KIVRevertHistory>();
            data.Paging.ItemCount = kivRes.ItemCount;
            data.Paging.PageSize = kivReq.PageSize;
            data.Paging.CurrentPage = kivReq.PageNumber;
            return PartialView("_KIVRevertHistory", data);
        }


        public async Task<ActionResult> EventAttendanceReport()
        {
            var vm = new AttendanceReportViewModel();
            var attendanceReq = new EventFilterViewModel { PageNumber = 1, PageSize = 10, };
            var attendanceRes = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventAttendanceListResponse>>>(attendanceReq, "/api/v1/event/user-get-by-filter");

            vm.AttendanceReport.AttendanceList = attendanceRes.Data ?? new List<EventAttendanceListResponse>();
            vm.AttendanceReport.Paging.ItemCount = attendanceRes.ItemCount;
            vm.AttendanceReport.Paging.PageSize = attendanceReq.PageSize;
            vm.AttendanceReport.Paging.CurrentPage = attendanceReq.PageNumber;


            vm.EventStatuses = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.UserEventStatus);
            vm.AttendanceStatuses = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AttendanceStatus);
            vm.PaymentChannel = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.PaymentChannel);
            vm.Users = await _userService.GetAgentUserAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> EventAttendanceReport(EventFilterViewModel req)
        {
            var res = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventAttendanceListResponse>>>(req, "/api/v1/event/user-get-by-filter");
            var data = new AttendanceReport();
            data.AttendanceList = res.Data ?? new List<EventAttendanceListResponse>();
            data.Paging.ItemCount = res.ItemCount;
            data.Paging.PageSize = req.PageSize;
            data.Paging.CurrentPage = req.PageNumber;
            return PartialView("_EventAttendanceTable", data);
        }
        public async Task<ActionResult> ClientSummary()
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var req = new ClientSummaryRequest { CreatedDateFrom = startDate, CreatedDateTo = endDate, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var res = await _caliphAPIHelper.PostAsync<ClientSummaryRequest, ResponseData<List<ClientSummary>>>(req, "/api/v1/report/get-client-summary");
            var clientSummaryVM = new ClientSummaryViewModel();
            clientSummaryVM.ClientSummaries = res.Data ?? new List<ClientSummary>();
            clientSummaryVM.Users = await _userService.GetAgentUserAsync();
            return View(clientSummaryVM);
        }

        [HttpPost]
        public async Task<ActionResult> ClientSummary(ClientSummaryRequest req)
        {
            var res = await _caliphAPIHelper.PostAsync<ClientSummaryRequest, ResponseData<List<ClientSummary>>>(req, "/api/v1/report/get-client-summary");
            var data = res.Data ?? new List<ClientSummary>();
            return PartialView("_ClientSummaryTable", data);
        }
        public ActionResult DealHistory()
        {
            return View();
        }
        public async Task<ActionResult> PrintProject100()
        {
            var view = new Project100ViewModel();
            view.AnnualIncomes = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AnnualIncome);
            view.Ages = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AgeRange);
            view.CouldApproachBusinesses = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.CouldApproachOnBusiness);
            view.HowOftenSeenInAYears = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.HowOftenSeenInAYear);
            view.HowWellKnowns = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.HowWellKnown);
            view.LengthOfTimeKnowns = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.LengthOfTimeKnown);
            view.MaritalStatuses = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.MaritalStatus);
            view.Occupations = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.Occupation);
            view.AbilityToProvideReferrals = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AbilityToProvideReferrals);
            var filter = new ClientFilterRequest { PageNumber = 1, PageSize = 999999, };
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-by-client-filter");
            view.Agents = await _userService.GetAgentUserAsync();
            view.Clients = responseData.Data;
            var report = new PartialViewAsPdf("~/Views/Shared/_Project100Form.cshtml", view);
            return report;
        }
        [HttpPost]
        public async Task<ActionResult> GetProject100(ClientFilterRequest filter)
        {
            var view = new Project100ViewModel();
            view.AnnualIncomes = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AnnualIncome);
            view.Ages = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AgeRange);
            view.CouldApproachBusinesses = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.CouldApproachOnBusiness);
            view.HowOftenSeenInAYears = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.HowOftenSeenInAYear);
            view.HowWellKnowns = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.HowWellKnown);
            view.LengthOfTimeKnowns = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.LengthOfTimeKnown);
            view.MaritalStatuses = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.MaritalStatus);
            view.Occupations = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.Occupation);
            view.AbilityToProvideReferrals = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AbilityToProvideReferrals);
            //    var filter = new ClientFilterRequest { PageNumber = 1, PageSize = 999999, };
            var responseData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(filter, "/api/v1/client/get-by-client-filter");


            view.Clients = responseData.Data;
            TempData["Project100"] = view;
            //  return Json(view);
            return PartialView("~/Views/Shared/_Project100Form.cshtml", view);
        }

        public ActionResult PrintPartialViewToPdf()
        {
            var data = (Project100ViewModel)TempData["Project100"] ?? new Project100ViewModel();
            var report = new PartialViewAsPdf("~/Views/Shared/_Project100Form.cshtml", data);
            return report;

        }



        [HttpPost]
        public async Task<ActionResult> GetProject100Recruit(FiltertAgentRecruitRequest filter)
        {
            var view = new Project100RecruitmentViewModel();
            view.AnnualIncomes = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AnnualIncome);
            view.Ages = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.AgeRange);
            view.MaritalStatuses = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.MaritalStatus);
            view.Occupations = await _masterDataService.GetMasterDatasAsync(MasterDataEnum.MasterData.Occupation);
            var responseData = await _caliphAPIHelper.PostAsync<FiltertAgentRecruitRequest, ResponseData<List<AgentRecruiment>>>(filter, "/api/v1/agent-recruit/get-by-filter");
            view.AgentRecruiments = responseData.Data;
            TempData["Project100Recruit"] = view;
            //  return Json(view);
            return PartialView("~/Views/Shared/_Project100FormRecruit.cshtml", view);
        }

        public ActionResult PrintRecruitProject100()
        {
            var data = (Project100RecruitmentViewModel)TempData["Project100Recruit"] ?? new Project100RecruitmentViewModel();
            var report = new PartialViewAsPdf("~/Views/Shared/_Project100FormRecruit.cshtml", data);
            return report;

        }
        public ActionResult ExportToExcel()
        {
            var testdata = new List<Employee>()
         {
             new Employee(){ EmpID=101, EmpName="Johnny"},
             new Employee(){ EmpID=102, EmpName="Tom"},
             new Employee(){ EmpID=103, EmpName="Jack"},
             new Employee(){ EmpID=104, EmpName="Vivian"},
             new Employee(){ EmpID=105, EmpName="Edward"},
         };
            //using System.Data;
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("EmpID"),
                                     new DataColumn("EmpName") });

            foreach (var emp in testdata)
            {
                dt.Rows.Add(emp.EmpID, emp.EmpName);
            }
            //using ClosedXML.Excel;
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }


        [HttpPost]
        public async Task<ActionResult> ExportEventAttendanceFilter(EventFilterViewModel req)
        {
            var res = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventAttendanceListResponse>>>(req, "/api/v1/event/user-get-by-filter");
            var data = new AttendanceReport();
            data.AttendanceList = res.Data ?? new List<EventAttendanceListResponse>();
            data.Paging.ItemCount = res.ItemCount;
            data.Paging.PageSize = req.PageSize;
            data.Paging.CurrentPage = req.PageNumber;
            DataTable dt = new DataTable("AttendanceReport");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Event Name"),
                                            new DataColumn("Event Date"),
                                            new DataColumn("Agent Id"),
                                            new DataColumn("Agent Name"),
            new DataColumn("Status"),
            new DataColumn("Attendance"),
            new DataColumn("Payment")});

            TempData["ExportAttendance"] = dt;
            foreach (var column in data.AttendanceList)
            {
                dt.Rows.Add(column.EventName, column.EventDateFrom, column.Username, column.DisplayName, column.UserEventStatusDesc, 
                    column.AttendanceDesc, column.PaymentChannelDesc);
            }
            //  return Json(view);
            return Json("");
        }

        public async Task<ActionResult> ExportEventAttendanceReport(EventFilterViewModel req)
        {

            if (TempData["ExportAttendance"] == null)
            {
                return View();
            }

            var dt = (DataTable)TempData["ExportAttendance"];
            // Prepare the response
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=AttendanceReport.xlsx");
            var MyWorkBook = new XLWorkbook();
            MyWorkBook.Worksheets.Add(dt);
            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                MyWorkBook.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                memoryStream.Close();
            }

            Response.End();
            return View();
            //using (XLWorkbook wb = new XLWorkbook())
            //{
            //    wb.Worksheets.Add(dt);
            //    using (MemoryStream stream = new MemoryStream())
            //    {
            //        wb.SaveAs(stream);
            //    //    wb.SaveToFile("insertTableToExcel.xls", ExcelVersion.Version97to2003);
            //        return File(stream.ToArray(), "application/vnd.ms-excel", "Grid.xlsx");
            //    }
            //}
        }


        public async Task<ActionResult> ExportBonusReport(EventFilterViewModel req)
        {

            if (TempData[""] == null)
            {
                return View();
            }

            var dt = (DataTable)TempData["Bonus"];
            // Prepare the response
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=AttendanceReport.xlsx");
            var MyWorkBook = new XLWorkbook();
            MyWorkBook.Worksheets.Add(dt);
            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                MyWorkBook.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                memoryStream.Close();
            }

            Response.End();
            return View();
            //using (XLWorkbook wb = new XLWorkbook())
            //{
            //    wb.Worksheets.Add(dt);
            //    using (MemoryStream stream = new MemoryStream())
            //    {
            //        wb.SaveAs(stream);
            //    //    wb.SaveToFile("insertTableToExcel.xls", ExcelVersion.Version97to2003);
            //        return File(stream.ToArray(), "application/vnd.ms-excel", "Grid.xlsx");
            //    }
            //}
        }



        public async Task<ActionResult> AgentRecruitSummary()
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var req = new ClientSummaryRequest { CreatedDateFrom = startDate, CreatedDateTo = endDate, CreatedBy = UserHelper.GetDefaultSearchUser() };
            var res = await _caliphAPIHelper.PostAsync<ClientSummaryRequest, ResponseData<List<ClientSummary>>>(req, "/api/v1/report/get-agent-recruit-summary");
            var clientSummaryVM = new ClientSummaryViewModel();
            clientSummaryVM.ClientSummaries = res.Data ?? new List<ClientSummary>();
            clientSummaryVM.Users = await _userService.GetAgentUserAsync();
            return View(clientSummaryVM);
        }

        [HttpPost]
        public async Task<ActionResult> AgentRecruitSummary(ClientSummaryRequest req)
        {
            var res = await _caliphAPIHelper.PostAsync<ClientSummaryRequest, ResponseData<List<ClientSummary>>>(req, "/api/v1/report/get-agent-recruit-summary");
            var data = res.Data ?? new List<ClientSummary>();
            return PartialView("_ClientSummaryTable", data);
        }
    }


}

     