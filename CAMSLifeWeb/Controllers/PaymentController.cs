using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Models.API.Event.Request;
using CaliphWeb.Models.API.Event.Response;
using CaliphWeb.Models.ViewModel;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Security.Cryptography;

namespace CaliphWeb.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;

        public PaymentController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> MakePayment(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<EventPaymentRequest>(formCollection);
            model.CreatedBy = UserHelper.GetLoginUser();
            model.PayementStatusId = (int)MasterDataEnum.PaymentStatus.Pending;// 180;

            var response = await _caliphAPIHelper.PostAsync<EventPaymentRequest, ResponseData<EventPaymentListResponse>>(model, "/api/v1/event/user-payment-add");

            var refId = response.Data.UserEventPaymentRefNo;
            var amount = model.Price.ToString();
            string url = Request.Url.GetLeftPart(UriPartial.Authority); 

            //175=MobiPay, 176=PayLater, 177=Atome , 178 = Stripe Payment
            if (model.PaymentChannelId == (int)MasterDataEnum.PaymentChannel.MobiPay)
            {
                PaymentViewModel paymentViewModel = new PaymentViewModel()
                {
                    Amount = amount,
                    UserEventPaymentRefNo = refId,
                    MobiApiId = "mobi0064",
                    MobiApiKey = "b07ad9f31df158edb188a41f725899bc",
                    CallbackUrl = url + "/payment/result/" + refId + "/"+ model.PaymentChannelId
                };

                return View("MobiPayment", paymentViewModel);
            }
            if (model.PaymentChannelId == (int)MasterDataEnum.PaymentChannel.Stripe)
            {
                PaymentViewModel paymentViewModel = new PaymentViewModel()
                {
                    Amount = amount,
                    UserEventPaymentRefNo = refId,
                    CallbackUrl = url + "/payment/StripePaymentResult/" + refId+"/"+model.PaymentChannelId
                };

                return View("StripePayment", paymentViewModel);
            }
            if (model.PaymentChannelId == (int)MasterDataEnum.PaymentChannel.PayLater)
            {
                PaymentViewModel paymentViewModel = new PaymentViewModel()
                {
                    Amount = amount,
                    UserEventPaymentRefNo = refId,
                    CallbackUrl = url + "/payment/Result/" + refId + "/" + model.PaymentChannelId
                };

                var payLaterResult = CreatePaylaterPayment(paymentViewModel);
                if (payLaterResult.Result == string.Empty)
                    return Redirect("~/Payment/Result/" + refId);
                else
                    return Redirect(payLaterResult.Result);
            }
            else if (model.PaymentChannelId == (int)MasterDataEnum.PaymentChannel.CommissionDeduct)// deduct commission
            {
                return Redirect("~/Payment/Result/" + refId+"/"+model.PaymentChannelId);
            }
            else
            {
                return Redirect("~/Payment/Result/" + refId);
            }
        }

        public async Task<ActionResult> Result(string id, int paymentChannel)
        {
            var paymentViewModel = new PaymentViewModel { UserEventPaymentRefNo = id };
            var jsonStr = JsonConvert.SerializeObject(MapRequestFormData());
            LogResponseToFile(paymentViewModel, jsonStr);

            var paymentInfo = await GetPaymentInfo(paymentViewModel);
            paymentViewModel.UserEventId = (paymentInfo == null || paymentInfo.Data.Count == 0) ? 0 : paymentInfo.Data.FirstOrDefault().UserEventId;
            paymentViewModel=await MapEventInfo( paymentViewModel);
           
            if (paymentInfo == null || paymentInfo.Data.Count == 0)
            {
                paymentViewModel.UserEventPaymentRefNo = "Record not found.";
                return View("Result", paymentViewModel);
            }

            if (paymentChannel == (int)MasterDataEnum.PaymentChannel.MobiPay)
                MapMobiPayViewModel(paymentViewModel);
            else if (paymentChannel == (int)MasterDataEnum.PaymentChannel.Stripe)
                MapStripeViewModel(paymentViewModel);
            else if (paymentChannel == (int)MasterDataEnum.PaymentChannel.PayLater)
                MapPayLaterViewModel(paymentViewModel);
            else if (paymentChannel == (int)MasterDataEnum.PaymentChannel.CommissionDeduct)
                paymentViewModel.IsSuccess = true;

            if (paymentViewModel.IsSuccess)
                UpdateUserEvent(paymentInfo.Data.FirstOrDefault().UserEventId);

            await UpdatePaymentResponse(paymentViewModel, jsonStr, paymentInfo);
         
            return View("Result", paymentViewModel);
        }


       


        private async Task<PaymentViewModel> MapEventInfo(PaymentViewModel paymentViewModel)
        {
            
            var userEvent = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventAttendanceListResponse>>>(new EventFilterViewModel { UserEventId = paymentViewModel.UserEventId }, "/api/v1/event/user-get-by-filter");
            if (userEvent == null || userEvent.Data.Count == 0)
                return null;

            var userEventDate = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventDateListResponse>>>(new EventFilterViewModel { EventDateId = userEvent.Data.FirstOrDefault().EventDateId }, "/api/v1/event/date-get-by-filter");
            if (userEventDate == null || userEventDate.Data.Count == 0)
                return null;

            var eventResponse = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(new EventFilterViewModel { EventId = userEventDate.Data.FirstOrDefault().EventId }, "/api/v1/event/get-by-filter");
            if (eventResponse == null || eventResponse.Data.Count == 0)
                return null;

            var eventInfo = eventResponse.Data.FirstOrDefault();


            paymentViewModel.EventName = eventInfo.EventName;
            paymentViewModel.EventType = eventInfo.EventTypeDesc;
            paymentViewModel.EventHost = eventInfo.EventHostDesc;
            paymentViewModel.EventChannel = eventInfo.EventChannelDesc;
            paymentViewModel.EventLocation = eventInfo.EventChannelLocation;
            paymentViewModel.Amount = eventInfo.EventFees.ToString("f2");
            paymentViewModel.EventDateFrom = userEventDate.Data.FirstOrDefault().EventDateFrom;
            paymentViewModel.EventDateTo = userEventDate.Data.FirstOrDefault().EventDateTo;

            return paymentViewModel;
        }

        private async Task<ResponseData<List<EventPaymentListResponse>>> GetPaymentInfo(PaymentViewModel paymentViewModel)
        {
            var eventPaymentFilterViewModel = new EventFilterViewModel
            {
                PageNumber = 1,
                PageSize = 10,
                UserEventPaymentRefNo = paymentViewModel.UserEventPaymentRefNo
            };

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventPaymentListResponse>>>(eventPaymentFilterViewModel, "/api/v1/event/user-payment-get-by-filter");
            return responseData;
        }

        private async Task UpdatePaymentResponse(PaymentViewModel paymentViewModel, string jsonStr, ResponseData<List<EventPaymentListResponse>> responseData)
        {
            EventPaymentRequest eventPaymentRequest = new EventPaymentRequest()
            {
                UserEventPaymentId = responseData.Data.FirstOrDefault().UserEventPaymentId,
                PaymentRefId = paymentViewModel.PaymentRefId,
                PaymentResponse = jsonStr,
                PayementStatusId = paymentViewModel.IsSuccess ? 181 : 182,
                Remarks = "",
                UpdatedBy = "PaymentResponse"
            };

            await _caliphAPIHelper.PostAsync<EventPaymentRequest, ResponseData<string>>(eventPaymentRequest, "/api/v1/event/user-payment-update");
        }

        private static void LogResponseToFile(PaymentViewModel paymentViewModel, string jsonStr)
        {
            var path = HostingEnvironment.MapPath("~/FileUpload/PaymentResponse/");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            // Log response to file
            StreamWriter streamWriter = new StreamWriter(path + paymentViewModel.UserEventPaymentRefNo + "_" + DateTime.Now.ToString("yyyyMMddHHMMssfff") + ".txt", true);
            streamWriter.WriteLine(jsonStr);
            streamWriter.Close();
        }

        private Dictionary<string, string> MapRequestFormData()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            for (int i = 0; i < Request.Form.Keys.Count; i++)
            {
                keyValuePairs.Add(Request.Form.Keys[i].ToString(), Request[Request.Form.Keys[i]].ToString());
            }

            return keyValuePairs;
        }

        private void MapMobiPayViewModel(PaymentViewModel paymentViewModel)
        {
            if (Request["status"] != null && Request["amount"] != null)
            {
                paymentViewModel.IsSuccess = Request["status"].ToString() == "1" ? true : false;
                paymentViewModel.Amount = Request["amount"].ToString();
            }
            else if (Request["responseCode"] != null && Request["amount"] != null)
            {
                paymentViewModel.IsSuccess = Request["responseCode"].ToString() == "0000" ? true : false;
                paymentViewModel.Amount = Request["amount"].ToString();
                paymentViewModel.PaymentRefId = Request["tid"] != null ? Request["tid"].ToString() : Request["boostRefNumber"] != null ? Request["boostRefNumber"].ToString() : "";
            }
            else if (Request["txStatus"] != null && Request["amount"] != null)
            {
                paymentViewModel.IsSuccess = Request["txStatus"].ToString() == "success" ? true : false;
                paymentViewModel.Amount = Request["amount"].ToString();
                paymentViewModel.PaymentRefId = Request["txID"] != null ? Request["txID"].ToString() : "";
            }
            else if (Request["fpx_debitAuthCode"] != null && Request["fpx_txnAmount"] != null)
            {
                paymentViewModel.IsSuccess = Request["fpx_debitAuthCode"].ToString() == "00" ? true : false;
                paymentViewModel.Amount = Request["fpx_txnAmount"].ToString();
                paymentViewModel.PaymentRefId = Request["fpx_fpxTxnId"] != null ? Request["fpx_fpxTxnId"].ToString() : "";
            }
        }


        private void MapStripeViewModel(PaymentViewModel paymentViewModel)
        {
            if (Request["status"] != null )
            {
                paymentViewModel.IsSuccess = Request["status"].ToString() == "succeeded" ? true : false;
            }
           
        }


        public ActionResult StripePaymentResult(string id, int paymentChannel)
        {
            StripePaymentResult paymentViewModel = new StripePaymentResult();
            paymentViewModel.Id = id;
            paymentViewModel.PaymentChannel = paymentChannel;
             

            return View("StripePaymentResult", paymentViewModel);
        }

        private async void MapPayLaterViewModel(PaymentViewModel paymentViewModel)
        {
            string sec_code = "89bd865258eda41ef5d23219a51d9ca2";
            int mch_Id = 43;

            string sign = "mch_id" + mch_Id + "transaction_id" + paymentViewModel.UserEventPaymentRefNo + "&secret=" + sec_code;
            string encode_md5 = MD5Hash(sign).ToUpper();
            string encode_sha1;

            using (SHA1 sha1Hash = SHA1.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(encode_md5);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                encode_sha1 = hash.ToUpper();
            }

            var paylaterPaymentModel = new PayLaterItemForSeach
            {
                mch_id = mch_Id,
                order_id = paymentViewModel.UserEventPaymentRefNo,
                sign = encode_sha1
            };

            var responseData = await _caliphAPIHelper.PostAsyncPayLater<PayLaterItemForSeach, PayLaterPaymentResult<string>>(paylaterPaymentModel, "/open/pay/search");

            if (responseData.info == "Paid")
            {
                paymentViewModel.IsSuccess = true;
            }
            else
            {
                paymentViewModel.IsSuccess = false;
            }

        }


        [HttpPost]
        public async Task<ActionResult> PaymentMethod(int eventId, int userEventId)
        {
            var eventFilterViewModel = new EventFilterViewModel
            {
                EventId = eventId,
                PageNumber = 1,
                PageSize = 10,
                UserEventUserId = UserHelper.GetLoginUserViewModel().UserId,
            };

            var responseData = await _caliphAPIHelper.PostAsync<EventFilterViewModel, ResponseData<List<EventListResponse>>>(eventFilterViewModel, "/api/v1/event/upcoming-get-by-filter");
            var eventInfo = responseData.Data.FirstOrDefault();

            PaymentViewModel paymentViewModel = new PaymentViewModel()
            {
                PaymentChannels = await _masterService.GetPaymentChannelAsync(),
                EventName = eventInfo.EventName,
                EventType = eventInfo.EventTypeDesc,
                EventHost = eventInfo.EventHostDesc,
                EventChannel = eventInfo.EventChannelDesc,
                Amount = eventInfo.EventFees.ToString("f2"),
                UserEventId = userEventId
            };

            return View(paymentViewModel);
        }

        public async void UpdateUserEvent(int userEventId)
        {
            var updateUserStatus = new AddEventUserRequest()
            {
                UserEventId = userEventId,
                StatusId = 168,
                UpdatedBy = "PaymentResponse"
            };

            await _caliphAPIHelper.PostAsync<AddEventUserRequest, ResponseData<string>>(updateUserStatus, "/api/v1/event/user-update-status");
        }



        //[Authorize]
      
        //    [HttpPost]
        //    public ActionResult CreateStripePayment(StripePaymentItem items)
        //    {
        //    StripeConfiguration.ApiKey = "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";

        //    var paymentIntentService = new PaymentIntentService();
        //        var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
        //        {
        //            Amount = (long)items.Amount*100, 
        //            Currency = "myr",
        //            //                  PaymentMethodTypes = new List<string>
        //            //{
        //            //  "card",
        //            //},
        //            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
        //            {
        //                Enabled = true,
        //            },
        //        });

        //        return Json(new { clientSecret = paymentIntent.ClientSecret });
        //    }

        [HttpPost]
        public async Task<string> CreatePaylaterPayment(PaymentViewModel items)
        {
            string ipAddress = "115.135.106.239";
            decimal amountInInt = decimal.Parse(items.Amount);
            string amountInString = amountInInt.ToString("0.00").Replace(".", "");
            int amountRequested = int.Parse(amountInString);
            string sec_code = "89bd865258eda41ef5d23219a51d9ca2";
            string currency = "MYR";
            int mch_Id = 43;

            string sign = "amount" + amountRequested + "cancel_url" + items.CallbackUrl + "confirm_url" + items.CallbackUrl + "currency" + currency + "ip" + ipAddress + "mch_id" + mch_Id
            +"notify_url" + items.CallbackUrl + "order_no" + items.UserEventPaymentRefNo + "&secret=" + sec_code;
            string encode_md5 = MD5Hash(sign).ToUpper();
            string encode_sha1;

            using (SHA1 sha1Hash = SHA1.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(encode_md5);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                encode_sha1 = hash.ToUpper();
            }

            var paylaterPaymentModel = new PayLaterItem
            {
                mch_id = mch_Id,
                confirm_url = items.CallbackUrl,
                cancel_url = items.CallbackUrl,
                notify_url = items.CallbackUrl,
                currency = currency,
                order_no = items.UserEventPaymentRefNo,
                amount = amountRequested,
                ip = ipAddress,
                sign = encode_sha1
                };

            var responseData = await _caliphAPIHelper.PostAsyncPayLater<PayLaterItem, PayLaterPaymentResult<string>>(paylaterPaymentModel, "/open/pay/Getpaid");
            if (responseData.info == "ok")
                return responseData.data;
            else
                return string.Empty;
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        [HttpPost]
        public async Task<string> CheckPaylaterPayment(PaymentViewModel paymentViewModel)
        {
            string sec_code = "89bd865258eda41ef5d23219a51d9ca2";
            int mch_Id = 43;

            string sign = "mch_id" + mch_Id + "transaction_id" + paymentViewModel.UserEventPaymentRefNo + "&secret=" + sec_code;
            string encode_md5 = MD5Hash(sign).ToUpper();
            string encode_sha1;

            using (SHA1 sha1Hash = SHA1.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(encode_md5);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                encode_sha1 = hash.ToUpper();
            }
            var paylaterPaymentModel = new PayLaterItemForSeach
            {
                mch_id = mch_Id,
                order_id = paymentViewModel.UserEventPaymentRefNo,
                sign = encode_sha1
            };

            var responseData = await _caliphAPIHelper.PostAsyncPayLater<PayLaterItemForSeach, PayLaterPaymentResult<string>>(paylaterPaymentModel, "/open/pay/search");

            if (responseData.info == "ok")
                return responseData.data;
            else
                return string.Empty;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

    }
}