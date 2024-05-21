using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.Mapper;
using CaliphWeb.Models.API.AgentRecruit;
using CaliphWeb.Models.API.Agent;
using CaliphWeb.Models.ViewModel;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CaliphWeb.Models.API;
using AutoMapper;
using CaliphWeb.Models;
using System.Web.Script.Serialization;
using System.Text;

namespace CaliphWeb.Controllers
{
    public class AgentCommissionController : Controller
    {
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IMasterDataService _masterDataService;
        private readonly IUserService _userService;

        public AgentCommissionController(ICaliphAPIHelper caliphAPIHelper, IMasterDataService masterDataService, IUserService userService)
        {
            this._caliphAPIHelper = caliphAPIHelper;
            this._masterDataService = masterDataService;
            this._userService = userService;
        }

        public async Task<ActionResult> Summary()
        {
            string userName = UserHelper.GetLoginUser();
            string currentYear = DateTime.Now.Year.ToString();
            DateTime firstDayOfCurrentYear = DateTime.Parse(currentYear + "-" + "01-01");
            DateTime lastDayOfCurrentYear = DateTime.Parse(currentYear + "-" + "12-31");
            string lastYear = DateTime.Now.AddYears(-1).Year.ToString();
            DateTime firstDayOfLastYear = DateTime.Parse(lastYear + "-" + "01-01");
            DateTime lastDayOflastYear = DateTime.Parse(lastYear + "-" + "12-31");

          
        
            var commisiionTable = new AgentCommissionTable()
            {
                CurrentYear = currentYear,
                LastYear = lastYear,
                Agents = await _userService.GetAgentUserAsync()
            };
            return View(commisiionTable);
        }

        [HttpPost]
        public async Task<ActionResult> Summary(AgentCommissionFilter userFilterViewModel)
        {
            string userName = userFilterViewModel.AgentId;
            string currentYear = userFilterViewModel.Year2;
            DateTime firstDayOfCurrentYear = DateTime.Parse(currentYear + "-" + "01-01");
            DateTime lastDayOfCurrentYear = DateTime.Parse(currentYear + "-" + "12-31");
            string lastYear = userFilterViewModel.Year1;
            DateTime firstDayOfLastYear = DateTime.Parse(lastYear + "-" + "01-01");
            DateTime lastDayOflastYear = DateTime.Parse(lastYear + "-" + "12-31");

            #region CurrentYearGeneration
            DateTime jan1 = DateTime.Parse(currentYear + "-" + "01-01");
            DateTime jan2 = DateTime.Parse(currentYear + "-" + "01-13");
            DateTime jan3 = DateTime.Parse(currentYear + "-" + "01-14");
            DateTime jan4 = DateTime.Parse(currentYear + "-" + "01-31");
            DateTime feb1 = DateTime.Parse(currentYear + "-" + "02-01");
            DateTime feb2 = DateTime.Parse(currentYear + "-" + "02-08");
            DateTime feb3 = DateTime.Parse(currentYear + "-" + "02-09");
            DateTime feb4 = DateTime.Parse(currentYear + "-" + "02-28");
            DateTime mar1 = DateTime.Parse(currentYear + "-" + "03-01");
            DateTime mar2 = DateTime.Parse(currentYear + "-" + "03-13");
            DateTime mar3 = DateTime.Parse(currentYear + "-" + "03-14");
            DateTime mar4 = DateTime.Parse(currentYear + "-" + "03-31");
            DateTime apr1 = DateTime.Parse(currentYear + "-" + "04-01");
            DateTime apr2 = DateTime.Parse(currentYear + "-" + "04-13");
            DateTime apr3 = DateTime.Parse(currentYear + "-" + "04-14");
            DateTime apr4 = DateTime.Parse(currentYear + "-" + "04-30");
            DateTime may1 = DateTime.Parse(currentYear + "-" + "05-01");
            DateTime may2 = DateTime.Parse(currentYear + "-" + "05-13");
            DateTime may3 = DateTime.Parse(currentYear + "-" + "05-14");
            DateTime may4 = DateTime.Parse(currentYear + "-" + "05-31");
            DateTime jun1 = DateTime.Parse(currentYear + "-" + "06-01");
            DateTime jun2 = DateTime.Parse(currentYear + "-" + "06-13");
            DateTime jun3 = DateTime.Parse(currentYear + "-" + "06-14");
            DateTime jun4 = DateTime.Parse(currentYear + "-" + "06-30");
            DateTime jul1 = DateTime.Parse(currentYear + "-" + "07-01");
            DateTime jul2 = DateTime.Parse(currentYear + "-" + "07-13");
            DateTime jul3 = DateTime.Parse(currentYear + "-" + "07-14");
            DateTime jul4 = DateTime.Parse(currentYear + "-" + "07-31");
            DateTime aug1 = DateTime.Parse(currentYear + "-" + "08-01");
            DateTime aug2 = DateTime.Parse(currentYear + "-" + "08-13");
            DateTime aug3 = DateTime.Parse(currentYear + "-" + "08-14");
            DateTime aug4 = DateTime.Parse(currentYear + "-" + "08-31");
            DateTime sep1 = DateTime.Parse(currentYear + "-" + "09-01");
            DateTime sep2 = DateTime.Parse(currentYear + "-" + "09-13");
            DateTime sep3 = DateTime.Parse(currentYear + "-" + "09-14");
            DateTime sep4 = DateTime.Parse(currentYear + "-" + "09-30");
            DateTime oct1 = DateTime.Parse(currentYear + "-" + "10-01");
            DateTime oct2 = DateTime.Parse(currentYear + "-" + "10-13");
            DateTime oct3 = DateTime.Parse(currentYear + "-" + "10-14");
            DateTime oct4 = DateTime.Parse(currentYear + "-" + "10-31");
            DateTime nov1 = DateTime.Parse(currentYear + "-" + "11-01");
            DateTime nov2 = DateTime.Parse(currentYear + "-" + "11-13");
            DateTime nov3 = DateTime.Parse(currentYear + "-" + "11-14");
            DateTime nov4 = DateTime.Parse(currentYear + "-" + "11-30");
            DateTime dec1 = DateTime.Parse(currentYear + "-" + "12-01");
            DateTime dec2 = DateTime.Parse(currentYear + "-" + "12-13");
            DateTime dec3 = DateTime.Parse(currentYear + "-" + "12-14");
            DateTime dec4 = DateTime.Parse(currentYear + "-" + "12-31");
            #endregion

            #region LastYearGeneration
            DateTime jan1_last = DateTime.Parse(lastYear + "-" + "01-01");
            DateTime jan2_last = DateTime.Parse(lastYear + "-" + "01-13");
            DateTime jan3_last = DateTime.Parse(lastYear + "-" + "01-14");
            DateTime jan4_last = DateTime.Parse(lastYear + "-" + "01-31");
            DateTime feb1_last = DateTime.Parse(lastYear + "-" + "02-01");
            DateTime feb2_last = DateTime.Parse(lastYear + "-" + "02-08");
            DateTime feb3_last = DateTime.Parse(lastYear + "-" + "02-09");
            DateTime feb4_last = DateTime.Parse(lastYear + "-" + "02-28");
            DateTime mar1_last = DateTime.Parse(lastYear + "-" + "03-01");
            DateTime mar2_last = DateTime.Parse(lastYear + "-" + "03-13");
            DateTime mar3_last = DateTime.Parse(lastYear + "-" + "03-14");
            DateTime mar4_last = DateTime.Parse(lastYear + "-" + "03-31");
            DateTime apr1_last = DateTime.Parse(lastYear + "-" + "04-01");
            DateTime apr2_last = DateTime.Parse(lastYear + "-" + "04-13");
            DateTime apr3_last = DateTime.Parse(lastYear + "-" + "04-14");
            DateTime apr4_last = DateTime.Parse(lastYear + "-" + "04-30");
            DateTime may1_last = DateTime.Parse(lastYear + "-" + "05-01");
            DateTime may2_last = DateTime.Parse(lastYear + "-" + "05-13");
            DateTime may3_last = DateTime.Parse(lastYear + "-" + "05-14");
            DateTime may4_last = DateTime.Parse(lastYear + "-" + "05-31");
            DateTime jun1_last = DateTime.Parse(lastYear + "-" + "06-01");
            DateTime jun2_last = DateTime.Parse(lastYear + "-" + "06-13");
            DateTime jun3_last = DateTime.Parse(lastYear + "-" + "06-14");
            DateTime jun4_last = DateTime.Parse(lastYear + "-" + "06-30");
            DateTime jul1_last = DateTime.Parse(lastYear + "-" + "07-01");
            DateTime jul2_last = DateTime.Parse(lastYear + "-" + "07-13");
            DateTime jul3_last = DateTime.Parse(lastYear + "-" + "07-14");
            DateTime jul4_last = DateTime.Parse(lastYear + "-" + "07-31");
            DateTime aug1_last = DateTime.Parse(lastYear + "-" + "08-01");
            DateTime aug2_last = DateTime.Parse(lastYear + "-" + "08-13");
            DateTime aug3_last = DateTime.Parse(lastYear + "-" + "08-14");
            DateTime aug4_last = DateTime.Parse(lastYear + "-" + "08-31");
            DateTime sep1_last = DateTime.Parse(lastYear + "-" + "09-01");
            DateTime sep2_last = DateTime.Parse(lastYear + "-" + "09-13");
            DateTime sep3_last = DateTime.Parse(lastYear + "-" + "09-14");
            DateTime sep4_last = DateTime.Parse(lastYear + "-" + "09-30");
            DateTime oct1_last = DateTime.Parse(lastYear + "-" + "10-01");
            DateTime oct2_last = DateTime.Parse(lastYear + "-" + "10-13");
            DateTime oct3_last = DateTime.Parse(lastYear + "-" + "10-14");
            DateTime oct4_last = DateTime.Parse(lastYear + "-" + "10-31");
            DateTime nov1_last = DateTime.Parse(lastYear + "-" + "11-01");
            DateTime nov2_last = DateTime.Parse(lastYear + "-" + "11-13");
            DateTime nov3_last = DateTime.Parse(lastYear + "-" + "11-14");
            DateTime nov4_last = DateTime.Parse(lastYear + "-" + "11-30");
            DateTime dec1_last = DateTime.Parse(lastYear + "-" + "12-01");
            DateTime dec2_last = DateTime.Parse(lastYear + "-" + "12-13");
            DateTime dec3_last = DateTime.Parse(lastYear + "-" + "12-14");
            DateTime dec4_last = DateTime.Parse(lastYear + "-" + "12-31");
            #endregion

            #region date String Generation
            string dateString1 = "1/1-13/1";
            string dateString2 = "14/1-31/1";
            string dateString3 = "1/2-8/2";
            string dateString4 = "9/2-28/2";
            string dateString5 = "1/3-13/3";
            string dateString6 = "14/3-31/3";
            string dateString7 = "1/4-13/4";
            string dateString8 = "14/4-30/4";
            string dateString9 = "1/5-13/5";
            string dateString10 = "14/5-31/5";
            string dateString11 = "1/6-13/6";
            string dateString12 = "14/6-30/6";
            string dateString13 = "1/7-13/7";
            string dateString14 = "14/7-30/7";
            string dateString15 = "1/8-13/8";
            string dateString16 = "14/8-31/8";
            string dateString17 = "1/9-13/9";
            string dateString18 = "14/9-30/9";
            string dateString19 = "1/10-13/10";
            string dateString20 = "14/10-31/10";
            string dateString21 = "1/11-13/11";
            string dateString22 = "14/11-30/11";
            string dateString23 = "1/12-13/12";
            string dateString24 = "14/12- 31/12";
            #endregion

            var filter = new AgentCommissionGet
            {
                Username = userName,
                PageSize = 9000,
                PageNumber = 1,
            };

            var response = await _caliphAPIHelper.PostAsync<AgentCommissionGet, ResponseData<List<AgentCommission>>>(filter, "/api/v1/agent-commission/get-by-filter");

            var currentYeardata = response.Data.Where(x => x.CycleStartDate >= firstDayOfCurrentYear && x.CycleEndDate <= lastDayOfCurrentYear).ToList();
            var lastYeardata = response.Data.Where(x => x.CycleStartDate >= firstDayOfLastYear && x.CycleEndDate <= lastDayOflastYear).ToList();

            string additionalRow = "    <tr style=\"height: 19pt\"><td style = \"width:87pt;border-top-style:solid;border-top-width:1pt;border-top-color:#878787;border-left-style:solid;border-left-width:1pt;border-left-color:#878787;border-bottom-style:solid;border-bottom-width:1pt;border-bottom-color:#878787;border-right-style:solid;border-right-width:1pt;border-right-color:#878787\" ><p class=\"s4\" style=\"padding-top: 3pt; text-indent: 0pt; text-align: center;\">#string1</p></td><td style = \"width:88pt;border-top-style:solid;border-top-width:1pt;border-top-color:#878787;border-left-style:solid;border-left-width:1pt;border-left-color:#878787;border-bottom-style:solid;border-bottom-width:1pt;border-bottom-color:#878787;border-right-style:solid;border-right-width:1pt;border-right-color:#878787\" ><p class=\"s4\" style=\"padding-top: 3pt;padding-right: 2pt;text-indent: 0pt;text-align: right;\">#string2</p></td><td style = \"width:90pt;border-top-style:solid;border-top-width:1pt;border-top-color:#878787;border-left-style:solid;border-left-width:1pt;border-left-color:#878787;border-bottom-style:solid;border-bottom-width:1pt;border-bottom-color:#878787;border-right-style:solid;border-right-width:1pt;border-right-color:#878787\" ><p class=\"s4\" style=\"padding-top: 3pt;padding-right: 2pt;text-indent: 0pt;text-align: right;\">#string3</p></td></tr>";

            var additionalRowList = new List<AdditionalRowTable>();

            #region MonthCount
            var month1count = currentYeardata.Where(x => x.CycleStartDate.Month == 1 && x.CycleEndDate.Month == 1).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month2count = currentYeardata.Where(x => x.CycleStartDate.Month == 2 && x.CycleEndDate.Month == 2).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month3count = currentYeardata.Where(x => x.CycleStartDate.Month == 3 && x.CycleEndDate.Month == 3).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month4count = currentYeardata.Where(x => x.CycleStartDate.Month == 4 && x.CycleEndDate.Month == 4).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month5count = currentYeardata.Where(x => x.CycleStartDate.Month == 5 && x.CycleEndDate.Month == 5).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month6count = currentYeardata.Where(x => x.CycleStartDate.Month == 6 && x.CycleEndDate.Month == 6).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month7count = currentYeardata.Where(x => x.CycleStartDate.Month == 7 && x.CycleEndDate.Month == 7).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month8count = currentYeardata.Where(x => x.CycleStartDate.Month == 8 && x.CycleEndDate.Month == 8).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month9count = currentYeardata.Where(x => x.CycleStartDate.Month == 9 && x.CycleEndDate.Month == 9).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month10count = currentYeardata.Where(x => x.CycleStartDate.Month == 10 && x.CycleEndDate.Month == 10).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month11count = currentYeardata.Where(x => x.CycleStartDate.Month == 11 && x.CycleEndDate.Month == 11).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month12count = currentYeardata.Where(x => x.CycleStartDate.Month == 12 && x.CycleEndDate.Month == 12).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();

            var month1countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 1 && x.CycleEndDate.Month == 1).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month2countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 2 && x.CycleEndDate.Month == 2).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month3countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 3 && x.CycleEndDate.Month == 3).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month4countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 4 && x.CycleEndDate.Month == 4).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month5countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 5 && x.CycleEndDate.Month == 5).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month6countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 6 && x.CycleEndDate.Month == 6).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month7countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 7 && x.CycleEndDate.Month == 7).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month8countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 8 && x.CycleEndDate.Month == 8).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month9countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 9 && x.CycleEndDate.Month == 9).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month10countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 10 && x.CycleEndDate.Month == 10).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month11countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 11 && x.CycleEndDate.Month == 11).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();
            var month12countPast = lastYeardata.Where(x => x.CycleStartDate.Month == 12 && x.CycleEndDate.Month == 12).GroupBy(x => new { x.CycleStartDate, x.CycleEndDate }).ToList().Select(x => new StartdatEnddate() { CycleStartDate = x.Key.CycleStartDate, CycleEndDate = x.Key.CycleEndDate }).ToList();


            if (month1count.Count > 2|| month1countPast.Count>2)
            {
                var result = CalculateAdditional(month1count, month1countPast, currentYeardata, lastYeardata, additionalRow);

                if (month1countPast.Count > 2)
                    result = CalculateAdditional(month1countPast, month1count, lastYeardata, currentYeardata, additionalRow);

                dateString1 = result.DateString1;
                dateString2 = result.DateString2;
                jan1 = result.DateDateTime1;
                jan2 = result.DateDateTime2;
                jan1_last = result.DateDateTime3;
                jan2_last = result.DateDateTime4;
                jan3 = result.DateDateTime5;
                jan4 = result.DateDateTime6;
                jan3_last = result.DateDateTime7;
                jan4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

            if (month2count.Count > 2 || month2countPast.Count > 2)
            {

                var result = CalculateAdditional(month2count, month2countPast, currentYeardata, lastYeardata, additionalRow);

                if (month2countPast.Count > 2)
                    result = CalculateAdditional(month2countPast, month2count, lastYeardata, currentYeardata, additionalRow);

                dateString3 = result.DateString1;
                dateString4 = result.DateString2;
                feb1 = result.DateDateTime1;
                feb2 = result.DateDateTime2;
                feb1_last = result.DateDateTime3;
                feb2_last = result.DateDateTime4;
                feb3 = result.DateDateTime5;
                feb4 = result.DateDateTime6;
                feb3_last = result.DateDateTime7;
                feb4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

            if (month3count.Count > 2|| month3countPast.Count > 2)
            {
                var result = CalculateAdditional(month3count, month3countPast, currentYeardata, lastYeardata, additionalRow);

                if (month3countPast.Count > 2)
                    result = CalculateAdditional(month3countPast, month3count, lastYeardata, currentYeardata, additionalRow);

                dateString5 = result.DateString1;
                dateString6 = result.DateString2;
                mar1 = result.DateDateTime1;
                mar2 = result.DateDateTime2;
                mar1_last = result.DateDateTime3;
                mar2_last = result.DateDateTime4;
                mar3 = result.DateDateTime5;
                mar4 = result.DateDateTime6;
                mar3_last = result.DateDateTime7;
                mar4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

            if (month4count.Count > 2 || month4countPast.Count>2)
            {
                var result = CalculateAdditional(month4count, month4countPast, currentYeardata, lastYeardata, additionalRow);

                if (month4countPast.Count > 2)
                    result = CalculateAdditional(month4countPast, month4count, lastYeardata, currentYeardata, additionalRow);

                dateString7 = result.DateString1;
                dateString8 = result.DateString2;
                apr1 = result.DateDateTime1;
                apr2 = result.DateDateTime2;
                apr1_last = result.DateDateTime3;
                apr2_last = result.DateDateTime4;
                apr3 = result.DateDateTime5;
                apr4 = result.DateDateTime6;
                apr3_last = result.DateDateTime7;
                apr4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }


            if (month5count.Count > 2|| month5countPast.Count > 2)
            {
                var result = CalculateAdditional(month5count, month5countPast, currentYeardata, lastYeardata, additionalRow);

                if (month5countPast.Count > 2)
                    result = CalculateAdditional(month5countPast, month5count, lastYeardata, currentYeardata, additionalRow);

                dateString9 = result.DateString1;
                dateString10 = result.DateString2;
                may1 = result.DateDateTime1;
                may2 = result.DateDateTime2;
                may1_last = result.DateDateTime3;
                may2_last = result.DateDateTime4;
                may3 = result.DateDateTime5;
                may4 = result.DateDateTime6;
                may3_last = result.DateDateTime7;
                may4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

             if (month6count.Count > 2|| month6countPast.Count > 2)
            {
                var result = CalculateAdditional(month6count, month6countPast, currentYeardata, lastYeardata, additionalRow);


                if (month6countPast.Count > 2)
                    result = CalculateAdditional(month6countPast, month6count, lastYeardata, currentYeardata, additionalRow);

                dateString11 = result.DateString1;
                dateString12 = result.DateString2;
                jun1 = result.DateDateTime1;
                jun2 = result.DateDateTime2;
                jun1_last = result.DateDateTime3;
                jun2_last = result.DateDateTime4;
                jun3 = result.DateDateTime5;
                jun4 = result.DateDateTime6;
                jun3_last = result.DateDateTime7;
                jun4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

            if (month7count.Count > 2|| month7countPast.Count > 2)
            {
                var result = CalculateAdditional(month7count, month7countPast, currentYeardata, lastYeardata, additionalRow);


                if (month7countPast.Count > 2)
                    result = CalculateAdditional(month7countPast, month7count, lastYeardata, currentYeardata, additionalRow);

                dateString13 = result.DateString1;
                dateString14 = result.DateString2;
                jul1 = result.DateDateTime1;
                jul2 = result.DateDateTime2;
                jul1_last = result.DateDateTime3;
                jul2_last = result.DateDateTime4;
                jul3 = result.DateDateTime5;
                jul4 = result.DateDateTime6;
                jul3_last = result.DateDateTime7;
                jul4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

            if (month8count.Count > 2|| month8countPast.Count > 2)
            {
                var result = CalculateAdditional(month8count, month8countPast, currentYeardata, lastYeardata, additionalRow);

                if (month8countPast.Count > 2)
                    result = CalculateAdditional(month8countPast, month8count, lastYeardata, currentYeardata, additionalRow);

                dateString15 = result.DateString1;
                dateString16 = result.DateString2;
                aug1 = result.DateDateTime1;
                aug2 = result.DateDateTime2;
                aug1_last = result.DateDateTime3;
                aug2_last = result.DateDateTime4;
                aug3 = result.DateDateTime5;
                aug4 = result.DateDateTime6;
                aug3_last = result.DateDateTime7;
                aug4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

            if (month9count.Count > 2|| month9countPast.Count > 2)
            {
                var result = CalculateAdditional(month9count, month9countPast, currentYeardata, lastYeardata, additionalRow);

                if (month9countPast.Count > 2)
                    result = CalculateAdditional(month9countPast, month9count, lastYeardata, currentYeardata, additionalRow);

                dateString17 = result.DateString1;
                dateString18 = result.DateString2;
                sep1 = result.DateDateTime1;
                sep2 = result.DateDateTime2;
                sep1_last = result.DateDateTime3;
                sep2_last = result.DateDateTime4;
                sep3 = result.DateDateTime5;
                sep4 = result.DateDateTime6;
                sep3_last = result.DateDateTime7;
                sep4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

            if (month10count.Count > 2|| month10countPast.Count>2)
            {
                var result = CalculateAdditional(month10count, month10countPast, currentYeardata, lastYeardata, additionalRow);

                if (month10countPast.Count > 2)
                    result = CalculateAdditional(month10countPast, month10count, lastYeardata, currentYeardata, additionalRow);

                dateString19 = result.DateString1;
                dateString20 = result.DateString2;
                oct1 = result.DateDateTime1;
                oct2 = result.DateDateTime2;
                oct1_last = result.DateDateTime3;
                oct2_last = result.DateDateTime4;
                oct3 = result.DateDateTime5;
                oct4 = result.DateDateTime6;
                oct3_last = result.DateDateTime7;
                oct4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

            if (month11count.Count > 2|| month11countPast.Count>2)
            {
                var result = CalculateAdditional(month11count, month11countPast, currentYeardata, lastYeardata, additionalRow);

                if (month11countPast.Count > 2)
                    result = CalculateAdditional(month11countPast, month11count, lastYeardata, currentYeardata, additionalRow);

                dateString21 = result.DateString1;
                dateString22 = result.DateString2;
                nov1 = result.DateDateTime1;
                nov2 = result.DateDateTime2;
                nov1_last = result.DateDateTime3;
                nov2_last = result.DateDateTime4;
                nov3 = result.DateDateTime5;
                nov4 = result.DateDateTime6;
                nov3_last = result.DateDateTime7;
                nov4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

            if (month12count.Count > 2|| month12countPast.Count>2)
            {
                var result = CalculateAdditional(month12count, month12countPast, currentYeardata, lastYeardata, additionalRow);


                if (month12countPast.Count > 2)
                    result = CalculateAdditional(month12countPast, month12count, lastYeardata, currentYeardata, additionalRow);

                dateString23 = result.DateString1;
                dateString24 = result.DateString2;
                dec1 = result.DateDateTime1;
                dec2 = result.DateDateTime2;
                dec1_last = result.DateDateTime3;
                dec2_last = result.DateDateTime4;
                dec3 = result.DateDateTime5;
                dec4 = result.DateDateTime6;
                dec3_last = result.DateDateTime7;
                dec4_last = result.DateDateTime8;
                foreach (var table in result.AdditionalRowTableList)
                {
                    additionalRowList.Add(table);
                }
            }

            #endregion

            #region CurrentYearAmtGeneration
            var currentYearjan1Amt = currentYeardata.Where(x => x.CycleStartDate >= jan1 && x.CycleEndDate <= jan2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearjan2Amt = currentYeardata.Where(x => x.CycleStartDate >= jan3 && x.CycleEndDate <= jan4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearjan3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 1 && x.CycleEndDate.Month == 1).Sum(x => x.CommAmt).ToString("n2");
            var currentYearfeb1Amt = currentYeardata.Where(x => x.CycleStartDate >= feb1 && x.CycleEndDate <= feb2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearfeb2Amt = currentYeardata.Where(x => x.CycleStartDate >= feb3 && x.CycleEndDate <= feb4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearfeb3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 2 && x.CycleEndDate.Month == 2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearmar1Amt = currentYeardata.Where(x => x.CycleStartDate >= mar1 && x.CycleEndDate <= mar2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearmar2Amt = currentYeardata.Where(x => x.CycleStartDate >= mar3 && x.CycleEndDate <= mar4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearmar3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 3 && x.CycleEndDate.Month == 3).Sum(x => x.CommAmt).ToString("n2");
            var currentYearapr1Amt = currentYeardata.Where(x => x.CycleStartDate >= apr1 && x.CycleEndDate <= apr2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearapr2Amt = currentYeardata.Where(x => x.CycleStartDate >= apr3 && x.CycleEndDate <= apr4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearapr3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 4 && x.CycleEndDate.Month == 4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearmay1Amt = currentYeardata.Where(x => x.CycleStartDate >= may1 && x.CycleEndDate <= may2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearmay2Amt = currentYeardata.Where(x => x.CycleStartDate >= may3 && x.CycleEndDate <= may4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearmay3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 5 && x.CycleEndDate.Month == 5).Sum(x => x.CommAmt).ToString("n2");
            var currentYearjun1Amt = currentYeardata.Where(x => x.CycleStartDate >= jun1 && x.CycleEndDate <= jun2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearjun2Amt = currentYeardata.Where(x => x.CycleStartDate >= jun3 && x.CycleEndDate <= jun4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearjun3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 6 && x.CycleEndDate.Month == 6).Sum(x => x.CommAmt).ToString("n2");
            var currentYearjul1Amt = currentYeardata.Where(x => x.CycleStartDate >= jul1 && x.CycleEndDate <= jul2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearjul2Amt = currentYeardata.Where(x => x.CycleStartDate >= jul3 && x.CycleEndDate <= jul4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearjul3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 7 && x.CycleEndDate.Month == 7).Sum(x => x.CommAmt).ToString("n2");
            var currentYearaug1Amt = currentYeardata.Where(x => x.CycleStartDate >= aug1 && x.CycleEndDate <= aug2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearaug2Amt = currentYeardata.Where(x => x.CycleStartDate >= aug3 && x.CycleEndDate <= aug4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearaug3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 8 && x.CycleEndDate.Month == 8).Sum(x => x.CommAmt).ToString("n2");
            var currentYearsep1Amt = currentYeardata.Where(x => x.CycleStartDate >= sep1 && x.CycleEndDate <= sep2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearsep2Amt = currentYeardata.Where(x => x.CycleStartDate >= sep3 && x.CycleEndDate <= sep4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearsep3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 9 && x.CycleEndDate.Month == 9).Sum(x => x.CommAmt).ToString("n2");
            var currentYearoct1Amt = currentYeardata.Where(x => x.CycleStartDate >= oct1 && x.CycleEndDate <= oct2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearoct2Amt = currentYeardata.Where(x => x.CycleStartDate >= oct3 && x.CycleEndDate <= oct4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearoct3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 10 && x.CycleEndDate.Month == 10).Sum(x => x.CommAmt).ToString("n2");
            var currentYearnov1Amt = currentYeardata.Where(x => x.CycleStartDate >= nov1 && x.CycleEndDate <= nov2).Sum(x => x.CommAmt).ToString("n2");
            var currentYearnov2Amt = currentYeardata.Where(x => x.CycleStartDate >= nov3 && x.CycleEndDate <= nov4).Sum(x => x.CommAmt).ToString("n2");
            var currentYearnov3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 11 && x.CycleEndDate.Month == 11).Sum(x => x.CommAmt).ToString("n2");
            var currentYeardec1Amt = currentYeardata.Where(x => x.CycleStartDate >= dec1 && x.CycleEndDate <= dec2).Sum(x => x.CommAmt).ToString("n2");
            var currentYeardec2Amt = currentYeardata.Where(x => x.CycleStartDate >= dec3 && x.CycleEndDate <= dec4).Sum(x => x.CommAmt).ToString("n2");
            var currentYeardec3Amt = currentYeardata.Where(x => x.CycleStartDate.Month == 12 && x.CycleEndDate.Month == 12).Sum(x => x.CommAmt).ToString("n2");
            #endregion

            #region LastYearAmtGeneration
            var lastYearjan1Amt = lastYeardata.Where(x => x.CycleStartDate >= jan1_last && x.CycleEndDate <= jan2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearjan2Amt = lastYeardata.Where(x => x.CycleStartDate >= jan3_last && x.CycleEndDate <= jan4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearjan3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 1 && x.CycleEndDate.Month == 1).Sum(x => x.CommAmt).ToString("n2");
            var lastYearfeb1Amt = lastYeardata.Where(x => x.CycleStartDate >= feb1_last && x.CycleEndDate <= feb2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearfeb2Amt = lastYeardata.Where(x => x.CycleStartDate >= feb3_last && x.CycleEndDate <= feb4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearfeb3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 2 && x.CycleEndDate.Month == 2).Sum(x => x.CommAmt).ToString("n2");
            var lastYearmar1Amt = lastYeardata.Where(x => x.CycleStartDate >= mar1_last && x.CycleEndDate <= mar2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearmar2Amt = lastYeardata.Where(x => x.CycleStartDate >= mar3_last && x.CycleEndDate <= mar4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearmar3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 3 && x.CycleEndDate.Month == 3).Sum(x => x.CommAmt).ToString("n2");
            var lastYearapr1Amt = lastYeardata.Where(x => x.CycleStartDate >= apr1_last && x.CycleEndDate <= apr2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearapr2Amt = lastYeardata.Where(x => x.CycleStartDate >= apr3_last && x.CycleEndDate <= apr4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearapr3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 4 && x.CycleEndDate.Month == 4).Sum(x => x.CommAmt).ToString("n2");
            var lastYearmay1Amt = lastYeardata.Where(x => x.CycleStartDate >= may1_last && x.CycleEndDate <= may2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearmay2Amt = lastYeardata.Where(x => x.CycleStartDate >= may3_last && x.CycleEndDate <= may4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearmay3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 5 && x.CycleEndDate.Month == 5).Sum(x => x.CommAmt).ToString("n2");
            var lastYearjun1Amt = lastYeardata.Where(x => x.CycleStartDate >= jun1_last && x.CycleEndDate <= jun2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearjun2Amt = lastYeardata.Where(x => x.CycleStartDate >= jun3_last && x.CycleEndDate <= jun4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearjun3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 6 && x.CycleEndDate.Month == 6).Sum(x => x.CommAmt).ToString("n2");
            var lastYearjul1Amt = lastYeardata.Where(x => x.CycleStartDate >= jul1_last && x.CycleEndDate <= jul2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearjul2Amt = lastYeardata.Where(x => x.CycleStartDate >= jul3_last && x.CycleEndDate <= jul4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearjul3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 7 && x.CycleEndDate.Month == 7).Sum(x => x.CommAmt).ToString("n2");
            var lastYearaug1Amt = lastYeardata.Where(x => x.CycleStartDate >= aug1_last && x.CycleEndDate <= aug2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearaug2Amt = lastYeardata.Where(x => x.CycleStartDate >= aug3_last && x.CycleEndDate <= aug4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearaug3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 8 && x.CycleEndDate.Month == 8).Sum(x => x.CommAmt).ToString("n2");
            var lastYearsep1Amt = lastYeardata.Where(x => x.CycleStartDate >= sep1_last && x.CycleEndDate <= sep2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearsep2Amt = lastYeardata.Where(x => x.CycleStartDate >= sep3_last && x.CycleEndDate <= sep4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearsep3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 9 && x.CycleEndDate.Month == 9).Sum(x => x.CommAmt).ToString("n2");
            var lastYearoct1Amt = lastYeardata.Where(x => x.CycleStartDate >= oct1_last && x.CycleEndDate <= oct2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearoct2Amt = lastYeardata.Where(x => x.CycleStartDate >= oct3_last && x.CycleEndDate <= oct4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearoct3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 10 && x.CycleEndDate.Month == 10).Sum(x => x.CommAmt).ToString("n2");
            var lastYearnov1Amt = lastYeardata.Where(x => x.CycleStartDate >= nov1_last && x.CycleEndDate <= nov2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearnov2Amt = lastYeardata.Where(x => x.CycleStartDate >= nov3_last && x.CycleEndDate <= nov4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYearnov3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 11 && x.CycleEndDate.Month == 11).Sum(x => x.CommAmt).ToString("n2");
            var lastYeardec1Amt = lastYeardata.Where(x => x.CycleStartDate >= dec1_last && x.CycleEndDate <= dec2_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYeardec2Amt = lastYeardata.Where(x => x.CycleStartDate >= dec3_last && x.CycleEndDate <= dec4_last).Sum(x => x.CommAmt).ToString("n2");
            var lastYeardec3Amt = lastYeardata.Where(x => x.CycleStartDate.Month == 12 && x.CycleEndDate.Month == 12).Sum(x => x.CommAmt).ToString("n2");
            #endregion

            #region Gain Percentage
            var gainPercent1 = decimal.Parse(lastYearjan3Amt) == 0 ? (((decimal.Parse(currentYearjan3Amt) - decimal.Parse(lastYearjan3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearjan3Amt) - decimal.Parse(lastYearjan3Amt)) / decimal.Parse(lastYearjan3Amt)) * 100).ToString("n2");
            var gainPercent2 = decimal.Parse(lastYearfeb3Amt) == 0 ? (((decimal.Parse(currentYearfeb3Amt) - decimal.Parse(lastYearfeb3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearfeb3Amt) - decimal.Parse(lastYearfeb3Amt)) / decimal.Parse(lastYearfeb3Amt)) * 100).ToString("n2");
            var gainPercent3 = decimal.Parse(lastYearmar3Amt) == 0 ? (((decimal.Parse(currentYearmar3Amt) - decimal.Parse(lastYearjan3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearmar3Amt) - decimal.Parse(lastYearjan3Amt)) / decimal.Parse(lastYearjan3Amt)) * 100).ToString("n2");
            var gainPercent4 = decimal.Parse(lastYearapr3Amt) == 0 ? (((decimal.Parse(currentYearapr3Amt) - decimal.Parse(lastYearapr3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearapr3Amt) - decimal.Parse(lastYearapr3Amt)) / decimal.Parse(lastYearapr3Amt)) * 100).ToString("n2");
            var gainPercent5 = decimal.Parse(lastYearmay3Amt) == 0 ? (((decimal.Parse(currentYearmay3Amt) - decimal.Parse(lastYearmay3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearmay3Amt) - decimal.Parse(lastYearmay3Amt)) / decimal.Parse(lastYearmay3Amt)) * 100).ToString("n2");
            var gainPercent6 = decimal.Parse(lastYearjun3Amt) == 0 ? (((decimal.Parse(currentYearjun3Amt) - decimal.Parse(lastYearjun3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearjun3Amt) - decimal.Parse(lastYearjun3Amt)) / decimal.Parse(lastYearjun3Amt)) * 100).ToString("n2");
            var gainPercent7 = decimal.Parse(lastYearjul3Amt) == 0 ? (((decimal.Parse(currentYearjul3Amt) - decimal.Parse(lastYearjul3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearjul3Amt) - decimal.Parse(lastYearjul3Amt)) / decimal.Parse(lastYearjul3Amt)) * 100).ToString("n2");
            var gainPercent8 = decimal.Parse(lastYearaug3Amt) == 0 ? (((decimal.Parse(currentYearaug3Amt) - decimal.Parse(lastYearaug3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearaug3Amt) - decimal.Parse(lastYearaug3Amt)) / decimal.Parse(lastYearaug3Amt)) * 100).ToString("n2");
            var gainPercent9 = decimal.Parse(lastYearsep3Amt) == 0 ? (((decimal.Parse(currentYearsep3Amt) - decimal.Parse(lastYearsep3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearsep3Amt) - decimal.Parse(lastYearsep3Amt)) / decimal.Parse(lastYearsep3Amt)) * 100).ToString("n2");
            var gainPercent10 = decimal.Parse(lastYearoct3Amt) == 0 ? (((decimal.Parse(currentYearoct3Amt) - decimal.Parse(lastYearoct3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearoct3Amt) - decimal.Parse(lastYearoct3Amt)) / decimal.Parse(lastYearoct3Amt)) * 100).ToString("n2");
            var gainPercent11 = decimal.Parse(lastYearnov3Amt) == 0 ? (((decimal.Parse(currentYearnov3Amt) - decimal.Parse(lastYearnov3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYearnov3Amt) - decimal.Parse(lastYearnov3Amt)) / decimal.Parse(lastYearnov3Amt)) * 100).ToString("n2");
            var gainPercent12 = decimal.Parse(lastYeardec3Amt) == 0 ? (((decimal.Parse(currentYeardec3Amt) - decimal.Parse(lastYeardec3Amt))) * 100).ToString("n2") : (((decimal.Parse(currentYeardec3Amt) - decimal.Parse(lastYeardec3Amt)) / decimal.Parse(lastYeardec3Amt)) * 100).ToString("n2");
            #endregion

            var currentYearTotal = currentYeardata.Where(x => x.CycleStartDate >= jan1 && x.CycleEndDate <= dec4).Sum(x => x.CommAmt).ToString("n2");
            var lastYearTotal = lastYeardata.Where(x => x.CycleStartDate >= jan1_last && x.CycleEndDate <= dec4_last).Sum(x => x.CommAmt).ToString("n2");
            var yearPercentTotal = decimal.Parse(lastYearTotal) == 0 ? (((decimal.Parse(currentYearTotal) - decimal.Parse(lastYearTotal))) * 100).ToString("n2") : (((decimal.Parse(currentYearTotal) - decimal.Parse(lastYearTotal)) / decimal.Parse(lastYearTotal)) * 100).ToString("n2");

            var commisiionTable = new AgentCommissionTable()
            {
                CurrentYear = currentYear,
                LastYear = lastYear,
                CurrentYearjan1Amt = currentYearjan1Amt,
                CurrentYearjan2Amt = currentYearjan2Amt,
                CurrentYearjan3Amt = currentYearjan3Amt,
                CurrentYearfeb1Amt = currentYearfeb1Amt,
                CurrentYearfeb2Amt = currentYearfeb2Amt,
                CurrentYearfeb3Amt = currentYearfeb3Amt,
                CurrentYearmar1Amt = currentYearmar1Amt,
                CurrentYearmar2Amt = currentYearmar2Amt,
                CurrentYearmar3Amt = currentYearmar3Amt,
                CurrentYearapr1Amt = currentYearapr1Amt,
                CurrentYearapr2Amt = currentYearapr2Amt,
                CurrentYearapr3Amt = currentYearapr3Amt,
                CurrentYearmay1Amt = currentYearmay1Amt,
                CurrentYearmay2Amt = currentYearmay2Amt,
                CurrentYearmay3Amt = currentYearmay3Amt,
                CurrentYearjun1Amt = currentYearjun1Amt,
                CurrentYearjun2Amt = currentYearjun2Amt,
                CurrentYearjun3Amt = currentYearjun3Amt,
                CurrentYearjul1Amt = currentYearjul1Amt,
                CurrentYearjul2Amt = currentYearjul2Amt,
                CurrentYearjul3Amt = currentYearjul3Amt,
                CurrentYearaug1Amt = currentYearaug1Amt,
                CurrentYearaug2Amt = currentYearaug2Amt,
                CurrentYearaug3Amt = currentYearaug3Amt,
                CurrentYearsep1Amt = currentYearsep1Amt,
                CurrentYearsep2Amt = currentYearsep2Amt,
                CurrentYearsep3Amt = currentYearsep3Amt,
                CurrentYearoct1Amt = currentYearoct1Amt,
                CurrentYearoct2Amt = currentYearoct2Amt,
                CurrentYearoct3Amt = currentYearoct3Amt,
                CurrentYearnov1Amt = currentYearnov1Amt,
                CurrentYearnov2Amt = currentYearnov2Amt,
                CurrentYearnov3Amt = currentYearnov3Amt,
                CurrentYeardec1Amt = currentYeardec1Amt,
                CurrentYeardec2Amt = currentYeardec2Amt,
                CurrentYeardec3Amt = currentYeardec3Amt,

                LastYearjan1Amt = lastYearjan1Amt,
                LastYearjan2Amt = lastYearjan2Amt,
                LastYearjan3Amt = lastYearjan3Amt,
                LastYearfeb1Amt = lastYearfeb1Amt,
                LastYearfeb2Amt = lastYearfeb2Amt,
                LastYearfeb3Amt = lastYearfeb3Amt,
                LastYearmar1Amt = lastYearmar1Amt,
                LastYearmar2Amt = lastYearmar2Amt,
                LastYearmar3Amt = lastYearmar3Amt,
                LastYearapr1Amt = lastYearapr1Amt,
                LastYearapr2Amt = lastYearapr2Amt,
                LastYearapr3Amt = lastYearapr3Amt,
                LastYearmay1Amt = lastYearmay1Amt,
                LastYearmay2Amt = lastYearmay2Amt,
                LastYearmay3Amt = lastYearmay3Amt,
                LastYearjun1Amt = lastYearjun1Amt,
                LastYearjun2Amt = lastYearjun2Amt,
                LastYearjun3Amt = lastYearjun3Amt,
                LastYearjul1Amt = lastYearjul1Amt,
                LastYearjul2Amt = lastYearjul2Amt,
                LastYearjul3Amt = lastYearjul3Amt,
                LastYearaug1Amt = lastYearaug1Amt,
                LastYearaug2Amt = lastYearaug2Amt,
                LastYearaug3Amt = lastYearaug3Amt,
                LastYearsep1Amt = lastYearsep1Amt,
                LastYearsep2Amt = lastYearsep2Amt,
                LastYearsep3Amt = lastYearsep3Amt,
                LastYearoct1Amt = lastYearoct1Amt,
                LastYearoct2Amt = lastYearoct2Amt,
                LastYearoct3Amt = lastYearoct3Amt,
                LastYearnov1Amt = lastYearnov1Amt,
                LastYearnov2Amt = lastYearnov2Amt,
                LastYearnov3Amt = lastYearnov3Amt,
                LastYeardec1Amt = lastYeardec1Amt,
                LastYeardec2Amt = lastYeardec2Amt,
                LastYeardec3Amt = lastYeardec3Amt,

                YearPercent1 = gainPercent1,
                YearPercent2 = gainPercent2,
                YearPercent3 = gainPercent3,
                YearPercent4 = gainPercent4,
                YearPercent5 = gainPercent5,
                YearPercent6 = gainPercent6,
                YearPercent7 = gainPercent7,
                YearPercent8 = gainPercent8,
                YearPercent9 = gainPercent9,
                YearPercent10 = gainPercent10,
                YearPercent11 = gainPercent11,
                YearPercent12 = gainPercent12,

                CurrentYearTotal = currentYearTotal,
                LastYearTotal = lastYearTotal,
                YearPercentTotal = yearPercentTotal,

                DateString1 = dateString1,
                DateString2 = dateString2,
                DateString3 = dateString3,
                DateString4 = dateString4,
                DateString5 = dateString5,
                DateString6 = dateString6,
                DateString7 = dateString7,
                DateString8 = dateString8,
                DateString9 = dateString9,
                DateString10 = dateString10,
                DateString11 = dateString11,
                DateString12 = dateString12,
                DateString13 = dateString13,
                DateString14 = dateString14,
                DateString15 = dateString15,
                DateString16 = dateString16,
                DateString17 = dateString17,
                DateString18 = dateString18,
                DateString19 = dateString19,
                DateString20 = dateString20,
                DateString21 = dateString21,
                DateString22 = dateString22,
                DateString23 = dateString23,
                DateString24 = dateString24,

                AdditionalRowTableList = additionalRowList,

                Agents = await _userService.GetAgentUserAsync()

            };

            return PartialView("_SummaryTable", commisiionTable);
        }


        [HttpPost]
        public async Task<ActionResult> NewSummary(AgentCommissionFilter userFilterViewModel)
        {
            string userName = userFilterViewModel.AgentId;
            string currentYear = userFilterViewModel.Year2;
            DateTime firstDayOfCurrentYear = DateTime.Parse(currentYear + "-" + "01-01");
            DateTime lastDayOfCurrentYear = DateTime.Parse(currentYear + "-" + "12-31");
            string lastYear = userFilterViewModel.Year1;
            DateTime firstDayOfLastYear = DateTime.Parse(lastYear + "-" + "01-01");
            DateTime lastDayOflastYear = DateTime.Parse(lastYear + "-" + "12-31");

            var filter = new AgentCommissionGet
            {
                Username = userName,
                PageSize = 9000,
                PageNumber = 1,
            };

            var response = await _caliphAPIHelper.PostAsync<AgentCommissionGet, ResponseData<List<AgentCommission>>>(filter, "/api/v1/agent-commission/get-by-filter");

            var currentYeardata = response.Data.Where(x => x.CycleStartDate >= firstDayOfCurrentYear && x.CycleEndDate <= lastDayOfCurrentYear).ToList();
            var lastYeardata = response.Data.Where(x => x.CycleStartDate >= firstDayOfLastYear && x.CycleEndDate <= lastDayOflastYear).ToList();

            var vm = new NewCommissionSummaryViewModel { CurrentYear = currentYear, LastYear = lastYear, CurrentYearData=currentYeardata, LastYearData= lastYeardata };

            return PartialView("_NewSummaryTable", vm);
        }

        public SummaryTableFunction CalculateAdditional(List<StartdatEnddate> table1, List<StartdatEnddate> table2, List<AgentCommission> currentYeardata, List<AgentCommission> lastYeardata,string additionalRow)
        {
            var result = new SummaryTableFunction();
                int i = 0;
                    foreach (var month in table1)
                    {
                        if (i == 0)
                        {
                            result.DateString1 = month.CycleStartDate.Day.ToString() + "/" + month .CycleStartDate.Month.ToString() + "-" + month .CycleEndDate.Day.ToString() + "/" + month .CycleEndDate.Month.ToString();
                            result.DateDateTime1 = month.CycleStartDate;
                            result.DateDateTime2 = month.CycleEndDate;
                            result.DateDateTime3 = month.CycleStartDate.AddYears(-1);
                            result.DateDateTime4 = month.CycleEndDate.AddYears(-1);
                        }
                        else if (i == 1)
                        {
                            result.DateString2 = month.CycleStartDate.Day.ToString() + "/" + month .CycleStartDate.Month.ToString() + "-" + month .CycleEndDate.Day.ToString() + "/" + month .CycleEndDate.Month.ToString();
                            result.DateDateTime5 = month.CycleStartDate;
                            result.DateDateTime6 = month.CycleStartDate;
                            result.DateDateTime7 = month.CycleStartDate.AddYears(-1);
                            result.DateDateTime8 = month.CycleEndDate.AddYears(-1);
                        }
                        else
                        {
                            string tableString = string.Empty;
                            string string1 = string.Empty;
                            string string2 = string.Empty;
                            string string3 = string.Empty;
                            var additionalRow1 = new AdditionalRowTable();
                            additionalRow1.Month = month.CycleStartDate.Month.ToString();
                            additionalRow1.TableString = new List<string>();

                            DateTime date1 = month.CycleStartDate;
                            DateTime date2 = month.CycleEndDate;
                            DateTime date3 = month.CycleStartDate.AddYears(-1);
                            DateTime date4 = month.CycleEndDate.AddYears(-1);

                            string1 = month.CycleStartDate.Day.ToString() + "/" + month .CycleStartDate.Month.ToString() + "-" + month .CycleEndDate.Day.ToString() + "/" + month .CycleEndDate.Month.ToString();
                            string2 = lastYeardata.Where(x => x.CycleStartDate >= date3 && x.CycleEndDate <= date4).Sum(x => x.CommAmt).ToString("n2");
                            string3 = currentYeardata.Where(x => x.CycleStartDate >= date1 && x.CycleEndDate <= date2).Sum(x => x.CommAmt).ToString("n2");
                            tableString = additionalRow.Replace("#string1", string1).Replace("#string2", string2).Replace("#string3", string3);
                            additionalRow1.TableString.Add(tableString);
                            result.AdditionalRowTableList = new List<AdditionalRowTable>();
                            result.AdditionalRowTableList.Add(additionalRow1);
                        }
                        i++;
                    }
               
            
            return result;
        }
    }

}