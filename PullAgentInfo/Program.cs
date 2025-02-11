﻿using Caliph.Library.Helper;
using CaliphWeb.Core;
using CaliphWeb.Core.Helper;
using CaliphWeb.Helper;
using CaliphWeb.Helper.ALCData;
using CaliphWeb.Models;
using CaliphWeb.Models.API;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace PullAgentInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRestHelper, HttpClientHelper>()
                .AddSingleton<ICaliphAPIHelper, CaliphAPIHelper>()
                 .AddSingleton<IALCApiHelper, SLMAPIHelper>()
                .AddSingleton<IALCDataGetter,SunlifeDataGetter>()
                .BuildServiceProvider();

            var caliphAPIHelper = serviceProvider.GetService<ICaliphAPIHelper>();
            var dataGetter = serviceProvider.GetService<IALCDataGetter>();

            GrabAgentInfo(caliphAPIHelper, dataGetter);
            //GrabAgentCommission(caliphAPIHelper, one2OneAPIHelper);
        }

        //private static void GrabAgentCommission(ICaliphAPIHelper caliphAPIHelper, IALCApiHelper one2OneAPIHelper)
        //{
        //    //insert agent commission 
        //    var today = new DateTime();
        //     today = DateTime.Now;
        //    //if (today.TimeOfDay == TimeSpan.Zero)
        //    //{

        //        var startDate = new DateTime(2023, 1, 1);
        //    var endDate = new DateTime(2023, 3, 20);
        //    while (startDate.Date <= endDate)
        //        {

        //           // var startDate = DateTime.Now;

        //            AgentListRequest commissionModel = new AgentListRequest()
        //            {
        //                Date = startDate
        //            };
        //            Console.WriteLine("getting commission data for " + startDate.ToString("yyyy-MM-dd"));
        //            var response_commission = one2OneAPIHelper.GetDataAsync<AgentListRequest, ResponseData<List<ApiResponseCommission>>>(commissionModel, "/edfwebapi/alc/agentcommission", new ResponseData<List<ApiResponseCommission>>()).Result;

        //            if (response_commission.Data != null && response_commission.Data.Count > 0)
        //            {
        //                foreach (var agent in response_commission.Data)
        //                {
        //                    AddAgentCommission commissionRequest = new AddAgentCommission()
        //                    {
        //                        Username = agent.Agent_Id,
        //                        AgentName = agent.Agent_Name,
        //                        PayoutDate = agent.Payout_Date,
        //                        CycleStartDate = agent.Cycle_Start_Date,
        //                        CycleEndDate = agent.Cycle_End_Date,
        //                        CreatedBy = "PullAgentInfo",
        //                        CommAmt = agent.Amount
        //                    };

        //                    var response_commission_data = caliphAPIHelper.PostAsync<AddAgentCommission, ResponseData<string>>(commissionRequest, "/api/v1/agent-commission/add").Result;
        //                    Console.WriteLine(response_commission_data.StatusMsg + " - " + response_commission_data.Data);
        //                }

        //            }

        //            startDate = startDate.AddDays(1);
        //        }
        //    //}
        //}

        private static void GrabAgentInfo(ICaliphAPIHelper caliphAPIHelper, IALCDataGetter dataGetter)
        {
            var startDate = new DateTime(2024, 1, 1);   //DateTime.Now.Date.AddDays(-1);
            var endDate = new DateTime(2024, 1, 1);//DateTime.Now.Date.AddDays(-1);

            var exportFile = "User_" + DateTime.Now.ToString("yyyyMMddhhmmssttt") + ".txt";
            var failedExportFile = "UserFailed_" + DateTime.Now.ToString("yyyyMMddhhmmssttt") + ".txt";
            while (startDate <= endDate)
            {
                AgentListRequest model = new AgentListRequest()
                {
                    Date = startDate
                };
                Console.WriteLine("getting data for " + startDate.ToString("yyyy-MM-dd"));
                var responseData = dataGetter.GetAgentListAsync(model).Result;


                Console.WriteLine(responseData.Count);

                if (responseData.Count > 0)
                {
                    string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    StreamWriter streamWriter = new StreamWriter(Path.Combine(docPath, exportFile), true);
                    StreamWriter failedStreamWriter = new StreamWriter(Path.Combine(docPath, failedExportFile), true);

                    foreach (var one2oneAgent in responseData.OrderBy(a => a.Join_Date).OrderBy(a=>a.Agent_Id))
                    {
                        var key = "H6&a##5";
                        var pass = GeneratePassword(8);
                        var password = HashHelper.GetHashMd5(key + pass);

                        UserRequest userRequest = new UserRequest()
                        {
                            Username = one2oneAgent.Agent_Id,
                            DisplayName = one2oneAgent.Agent_Name,
                            Fullname = one2oneAgent.Agent_Name,
                            PW = password,
                            RoleId = UserHelper.IsLeader(one2oneAgent.Role)? (int)MasterDataEnum.RoleId.Leader :(int)MasterDataEnum.RoleId.Agent,
                            StatusId = 1,
                            ContactNo = one2oneAgent.Mobile,
                            IcNo = one2oneAgent.IC,
                            Email =one2oneAgent.Email,
                            JoinDate = one2oneAgent.Join_Date,
                            UplineUsername = one2oneAgent.Upline_Agent_Id,
                            CreatedBy = "PullAgentInfo"
                        };
                       
                        if (!string.IsNullOrEmpty(one2oneAgent.IC))
                        {
                            var filterICReq = new UserFilterRequest { IcNo = one2oneAgent.IC, RoleId = 4 };
                            var filterICRes = caliphAPIHelper.PostAsync<UserFilterRequest, ResponseData<List<UserListResponse>>>(filterICReq, "/api/v1/system-user/get-by-filter").Result;

                            var agent = (filterICRes.Data != null && filterICRes.Data.Count > 0) ? filterICRes.Data.FirstOrDefault() : null;
                            if (agent != null && agent.RoleId == 4)
                            {
                                //convert
                                var convertReq = new ConvertAgentRequest { NewUsername = one2oneAgent.Agent_Id, Username = agent.Username, RoleId = userRequest.RoleId };
                                var response = caliphAPIHelper.PostAsync<ConvertAgentRequest, ResponseData<string>>(convertReq, "/api/v1/system-user/convert-one2one-agent").Result;
                                Console.WriteLine(JsonConvert.SerializeObject(response));
                                Console.WriteLine(response.StatusMsg + " - " + response.Data + "-" + one2oneAgent.Agent_Id + "-" + one2oneAgent.Join_Date);
                                if (response.IsSuccess)
                                {
                                    var helper = new EmailHelper();
                                    var email = helper.GetRegisterEmail(userRequest, pass);
                                    helper.SendEmail(email);
                                }
                                streamWriter.WriteLine(one2oneAgent.Agent_Id + "," + one2oneAgent.Agent_Name + "," + pass + "," + one2oneAgent.Upline_Agent_Id + "," + one2oneAgent.Upline_Agent_Name + "," + one2oneAgent.Join_Date + "," + one2oneAgent.Agent_Branch + "," + one2oneAgent.IC + "," + one2oneAgent.Mobile + "," + one2oneAgent.Email, "convert");
                            }
                        }
                        else
                        {
                            // add new 
                            var response = caliphAPIHelper.PostAsync<UserRequest, ResponseData<string>>(userRequest, "/api/v1/system-user/add").Result;
                            if (response.IsSuccess && !string.IsNullOrEmpty(userRequest.Email))
                            {
                                var helper = new EmailHelper();
                                
                                var email = helper.GetRegisterEmail(userRequest, pass);
                                helper.SendEmail(email);
                            }

                            if (!response.IsSuccess)
                            {
                                failedStreamWriter.WriteLine(response.StatusMsg + "," + one2oneAgent.Agent_Id + "," + one2oneAgent.Agent_Name + "," + pass + "," + one2oneAgent.Upline_Agent_Id + "," + one2oneAgent.Upline_Agent_Name + "," + one2oneAgent.Join_Date + "," + one2oneAgent.Email, "add");
                            }
                            else
                            {
                                streamWriter.WriteLine(one2oneAgent.Agent_Id + "," + one2oneAgent.Agent_Name + "," + pass + "," + one2oneAgent.Upline_Agent_Id + "," + one2oneAgent.Upline_Agent_Name + "," + one2oneAgent.Join_Date + "," + one2oneAgent.Agent_Branch + "," + one2oneAgent.IC + "," + one2oneAgent.Mobile + "," + one2oneAgent.Email, "add");
                            }
                            Console.WriteLine(response.StatusMsg + " - " + response.Data + "-" + one2oneAgent.Agent_Id + "-" + one2oneAgent.Join_Date);
                        }

                      

                        // send email notification 


                    }

                    streamWriter.Close();

                }
                startDate = startDate.AddDays(1);


            }
        }

        static string GeneratePassword(int length)
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];
                byte[] buffer = null;

                int maxRandom = byte.MaxValue - ((byte.MaxValue + 1) % chars.Length);

                crypto.GetBytes(data);

                char[] result = new char[length];

                for (int i = 0; i < length; i++)
                {
                    byte value = data[i];

                    while (value > maxRandom)
                    {
                        if (buffer == null)
                        {
                            buffer = new byte[1];
                        }

                        crypto.GetBytes(buffer);
                        value = buffer[0];
                    }

                    result[i] = chars[value % chars.Length];
                }

                return new string(result);
            }
        }
    }
}
