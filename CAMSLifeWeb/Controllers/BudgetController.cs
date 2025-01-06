using CaliphWeb.Models.API.Budget;
using CaliphWeb.Services;
using CaliphWeb.Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CaliphWeb.Core;
using CaliphWeb.Helper;
using CaliphWeb.Helper.Mapper;
using static CaliphWeb.ViewModel.Data.MasterDataEnum;
using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using CaliphWeb.Models.API.one2one;
using CaliphWeb.Models;

namespace CaliphWeb.Controllers
{
    [Authorize]
    public class BudgetController : Controller
    {
        private readonly IMasterDataService _masterService;
        private readonly ICaliphAPIHelper _caliphAPIHelper;
        private readonly IUserService _userService;
        private readonly IALCApiHelper _one2oneAPIHelper;

        public BudgetController(IMasterDataService masterService, ICaliphAPIHelper caliphAPIHelper, IUserService userService, IALCApiHelper one2OneApiHelper)
        {
            this._masterService = masterService;
            this._caliphAPIHelper = caliphAPIHelper;
            this._userService = userService;
            this._one2oneAPIHelper = one2OneApiHelper;
        }

        // GET: Budget
        public async Task<ActionResult> Index()
        {
            BudgetFilter filter = new BudgetFilter();
            var response = await _caliphAPIHelper.PostAsync<BudgetFilter, ResponseData<object>>(filter, "/api/v1/budget/simulator-get-by-filter");
            return View(response.Data);
        }

        public async Task<ActionResult> Index2()
        {
            BudgetFilter filter = new BudgetFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId };
            BudgetModel model = new BudgetModel();
            var response = await _caliphAPIHelper.PostAsync<BudgetFilter, ResponseData<List<BudgetView>>>(filter, "/api/v1/budget/simulator-get-by-filter");
            model.BudgetData.BudgetView = response.Data;


           



            model.Income.IncomeData = response.Data.Where(p => p.BudgetYear == DateTime.Now.Year).FirstOrDefault()?? new BudgetView { BudgetMonth=DateTime.Now.Month, BudgetYear=DateTime.Now.Year};


            if (!model.Income.IncomeData.BudgetYear.HasValue || model.Income.IncomeData.BudgetYear == 0)
                model.Income.IncomeData.BudgetYear = DateTime.Now.Year;
            if (!model.Income.IncomeData.BudgetMonth.HasValue || model.Income.IncomeData.BudgetMonth == 0)
                model.Income.IncomeData.BudgetMonth =12;
            if ( model.Income.IncomeData.ProductStartMonth == 0)
                model.Income.IncomeData.ProductStartMonth = DateTime.Now.Month;


            var startDate = new DateTime(model.Income.IncomeData.BudgetYear.Value, model.Income.IncomeData.ProductStartMonth, 1);
            var endDate = startDate.AddMonths(12);

            model.Income.IncomeData.PropotionACEs = await GetACe(MasterDataEnum.one2oneRelationType.PERSONAL,startDate, endDate);

            StrategyFilter strategyfilter = new StrategyFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId, BudgetStrategyYear= DateTime.Now.Year };
            var strategyresponse = await _caliphAPIHelper.PostAsync<StrategyFilter, ResponseData<List<BudgetStrategy>>>(strategyfilter, "/api/v1/budget/strategy-get-by-filter");
            model.Strategy.BudgetStrategy = strategyresponse.Data.FirstOrDefault() ?? new BudgetStrategy(); ;

            // var clientData = await _caliphAPIHelper.PostAsync<string, ResponseData<List<Client>>>("", "/api/v1/client/get-all");
            var clientFilter = new ClientFilterRequest { CreatedBy = UserHelper.GetLoginUserViewModel().Username, PageSize = 999, PageNumber = 1 };
            var clientData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(clientFilter, "/api/v1/client/get-by-client-filter");

            model.Monthly.Clients = clientData.Data;
            model = await AddDefaultIfEmpty(model, DateTime.Now.Year);

            MonthlyFilter monthlyfilter = new MonthlyFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId, BudgetYear = model.Income.IncomeData.BudgetYear ?? DateTime.Now.Year, BudgetMonth = model.Income.IncomeData.ProductStartMonth };
            var monthlyResponse = await _caliphAPIHelper.PostAsync<MonthlyFilter, ResponseData<List<BudgetMonthly>>>(monthlyfilter, "/api/v1/budget/monthly-get-by-filter");
            model.Monthly.BudgetMonthly = monthlyResponse.Data;
            model.Monthly.Year = model.Income.IncomeData.BudgetYear ?? DateTime.Now.Year;
            model.Monthly.Month = model.Income.IncomeData.ProductStartMonth;
            ;
            return View(model);
        }

        private async Task<BudgetModel> AddDefaultIfEmpty(BudgetModel model,int year)
        {
            int userid = UserHelper.GetLoginUserViewModel().UserId;
            string userName = UserHelper.GetLoginUser();
            if (model.Income.IncomeData == null)
            {
                SimulatorAdd simulatorAdd = new SimulatorAdd { BudgetYear = year, BudgetMonth = 6, ProductStartMonth=1, CreatedBy = userName, UserId = userid };
                var simulatorAddResponse = await _caliphAPIHelper.PostAsync<SimulatorAdd, ResponseData<string>>(simulatorAdd, "/api/v1/budget/simulator-add");
                model.Income.IncomeData = new BudgetView { BudgetYear = year, BudgetMonth = 6 };
                model.Income.IncomeData.BudgetId = ConvertHelper.ConvertInt(simulatorAddResponse.Data);

            }


            if (model.Income.IncomeData.BudgetGroupList == null)
                model.Income.IncomeData.BudgetGroupList = new List<BudgetGroup>();


            var personalProducerComm = model.Income.IncomeData.BudgetGroupList.Where(x => x.BudgetTitle == "Personal Producer").FirstOrDefault();
            var directGroupComm = model.Income.IncomeData.BudgetGroupList.Where(x => x.BudgetTitle == "Direct Group").FirstOrDefault();
            if (personalProducerComm == null)
            {
                GroupAdd groupAdd = new GroupAdd()
                {
                    BudgetId = Convert.ToInt32(model.Income.IncomeData.BudgetId),
                    BudgetTitle = "Personal Producer",
                    TargetCount="1",
                    CreatedBy = userName,
                    UserId = userid,
                    Remarks = "No Repeat",
                };
                var groupAddResponse = await _caliphAPIHelper.PostAsync<GroupAdd, ResponseData<string>>(groupAdd, "/api/v1/budget/group-add");
                personalProducerComm = new BudgetGroup { BudgetTitle = groupAdd.BudgetTitle, BudgetId = groupAdd.BudgetId.Value, Remarks = groupAdd.Remarks, TargetCount=1};
                model.Income.IncomeData.BudgetGroupList.Add(personalProducerComm);

            }

            //if (directGroupComm == null)
            //{
            //    GroupAdd groupAdd = new GroupAdd()
            //    {
            //        BudgetId = Convert.ToInt32(model.Income.IncomeData.BudgetId),
            //        BudgetTitle = "Direct Group",
            //        CreatedBy = userName,
            //        UserId = userid,
            //        Remarks = "No Repeat",
            //    };
            //    var groupAddResponse = await _caliphAPIHelper.PostAsync<GroupAdd, ResponseData<string>>(groupAdd, "/api/v1/budget/group-add");
            //    directGroupComm = new BudgetGroup { BudgetTitle = groupAdd.BudgetTitle, BudgetId = groupAdd.BudgetId.Value, Remarks = groupAdd.Remarks };
            //    model.Income.IncomeData.BudgetGroupList.Add(directGroupComm);

            //}

            return model;
        }

        [HttpPost]
        public async void DefaultLoad()
        {
            int userid = UserHelper.GetLoginUserViewModel().UserId;
            string userName = UserHelper.GetLoginUser();
            // Income
            SimulatorAdd simulatorAdd = PresetSimulator(userName, userid);
            var simulatorAddResponse = await _caliphAPIHelper.PostAsync<SimulatorAdd, ResponseData<string>>(simulatorAdd, "/api/v1/budget/simulator-add");
            // Group
            int defaultGroupcount = 6;
            for (int i = 1; i <= defaultGroupcount; i++)
            {
                GroupAdd groupAdd = PresetGroup(userName, userid, simulatorAddResponse.Data, i);
                var response = await _caliphAPIHelper.PostAsync<GroupAdd, ResponseData<string>>(groupAdd, "/api/v1/budget/group-add");
            }
            //Monthly
            //strategy

        }

        //income
        [HttpPost]
        public async Task<ActionResult> GetIncome(int incomeYear)
        {
            BudgetFilter filter = new BudgetFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId, BudgetYear = incomeYear };
            BudgetModel model = new BudgetModel();
            var response = await _caliphAPIHelper.PostAsync<BudgetFilter, ResponseData<List<BudgetView>>>(filter, "/api/v1/budget/simulator-get-by-filter");
            model.BudgetData.BudgetView = response.Data;
            model.Income.IncomeData = response.Data.FirstOrDefault();
            model= await AddDefaultIfEmpty(model, incomeYear);
            return PartialView("_IncomeSimulator2", model.Income);
        }




        //refresh propotion
        [HttpPost]
        public async Task<ActionResult> RefreshPropotionTab(int incomeYear, int startMonth)
        {
            BudgetFilter filter = new BudgetFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId, BudgetYear = incomeYear };
            BudgetModel model = new BudgetModel();
            var response = await _caliphAPIHelper.PostAsync<BudgetFilter, ResponseData<List<BudgetView>>>(filter, "/api/v1/budget/simulator-get-by-filter");
            model.BudgetData.BudgetView = response.Data;
            model.Income.IncomeData = response.Data.FirstOrDefault();
            model = await AddDefaultIfEmpty(model, incomeYear);

            var startDate = new DateTime(incomeYear, startMonth, 1);
            var endDate = startDate.AddMonths(12);
            model.Income.IncomeData.PropotionACEs = await GetACe(MasterDataEnum.one2oneRelationType.PERSONAL, startDate, endDate);
            return PartialView("_Propotion", model);
        }

        public async Task<List<PropotionACE>> GetACe(string type,DateTime startDate , DateTime endDate)
        {

            var request = new AgentACERequest
            {
                date_from = startDate,
                date_to = endDate,
                agent_id = UserHelper.GetLoginUserViewModel().Username,
                type = type,
               
            };

            var response = await _one2oneAPIHelper.GetDataAsync<AgentACERequest, One2OneResponse<AgentACEResponse>>(request, "/edfwebapi/alc/agentace",  new One2OneResponse<AgentACEResponse>());
            var result = new List<PropotionACE>();
            response.data = (response == null || response.data == null)? new List<AgentACEResponse>():response.data ;

            // map from api to   modal 
            for (int i = 0; i <12; i++)
            {
                var month = startDate.AddMonths(i);
                var currentMonthAce = response.data.Where(x => x.date.Month == month.Month&& x.date.Year== month.Year).Sum(x => x.ace);
                result.Add(new PropotionACE { Month = month.Month, Year=month.Year, ACE = currentMonthAce });

            }

            return result;
        }
        //refresh strategy
        [HttpPost]
        public async Task<ActionResult> RefreshStrategyTab(int incomeYear)
        {
            BudgetFilter filter = new BudgetFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId, BudgetYear = incomeYear };
            BudgetModel model = new BudgetModel();
            var response = await _caliphAPIHelper.PostAsync<BudgetFilter, ResponseData<List<BudgetView>>>(filter, "/api/v1/budget/simulator-get-by-filter");
            model.BudgetData.BudgetView = response.Data;
            model.Income.IncomeData = response.Data.FirstOrDefault();
            model = await AddDefaultIfEmpty(model, incomeYear);

            StrategyFilter strategyfilter = new StrategyFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId, BudgetStrategyYear = incomeYear};
            var strategyresponse = await _caliphAPIHelper.PostAsync<StrategyFilter, ResponseData<List<BudgetStrategy>>>(strategyfilter, "/api/v1/budget/strategy-get-by-filter");
            model.Strategy.BudgetStrategy = strategyresponse.Data.FirstOrDefault() ?? new BudgetStrategy(); ;
            return PartialView("_Strategies", model);
        }


        //refresh MOnthly Budget
        [HttpPost]
        public async Task<ActionResult> RefreshMonthlyBudgetTab(int incomeYear)
        {
            BudgetFilter filter = new BudgetFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId, BudgetYear = incomeYear };
            BudgetModel model = new BudgetModel();
            var response = await _caliphAPIHelper.PostAsync<BudgetFilter, ResponseData<List<BudgetView>>>(filter, "/api/v1/budget/simulator-get-by-filter");
            model.BudgetData.BudgetView = response.Data;
            model.Income.IncomeData = response.Data.FirstOrDefault();
            model = await AddDefaultIfEmpty(model, incomeYear);


            MonthlyFilter monthlyfilter = new MonthlyFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId, BudgetYear =incomeYear, BudgetMonth = model.Income.IncomeData.ProductStartMonth };

            var monthlyResponse = await _caliphAPIHelper.PostAsync<MonthlyFilter, ResponseData<List<BudgetMonthly>>>(monthlyfilter, "/api/v1/budget/monthly-get-by-filter");
            model.Monthly.BudgetMonthly = monthlyResponse.Data;
            model.Monthly.Year = DateTime.Now.Year;
            model.Monthly.Month = DateTime.Now.Month;
            
            return PartialView("_MonthlyBudget", model);
        }

        [HttpPost]
        public async Task<ActionResult> RefreshGroupCommission(int incomeYear)
        {
            BudgetFilter filter = new BudgetFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId , BudgetYear= incomeYear };
            BudgetModel model = new BudgetModel();
            var response = await _caliphAPIHelper.PostAsync<BudgetFilter, ResponseData<List<BudgetView>>>(filter, "/api/v1/budget/simulator-get-by-filter");
            model.Income.IncomeData = response.Data.FirstOrDefault();

            return PartialView("_IncomeStimulatorCommissionTable", model.Income);
        }



        [HttpPost]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            var response = new ResponseData<string>();
            var deleteRequest = new BudgetGroupDeleteRequest { BudgetGroupId = id, UpdatedBy = UserHelper.GetLoginUserViewModel().Username };
                response = await _caliphAPIHelper.PostAsync<BudgetGroupDeleteRequest, ResponseData<string>>(deleteRequest, "/api/v1/budget/group-delete");
           
            return Json(response);
        }


        [HttpPost]
        public async Task<ActionResult> DeleteMonthlyBudget(int id)
        {
            var response = new ResponseData<string>();
            var deleteRequest = new BudgetMonthlyDeleteRequest { BudgetMonthlyId = id, UpdatedBy = UserHelper.GetLoginUserViewModel().Username };
            response = await _caliphAPIHelper.PostAsync<BudgetMonthlyDeleteRequest, ResponseData<string>>(deleteRequest, "/api/v1/budget/monthly-delete");

            return Json(response);
        }

        public async Task<ActionResult> RefreshMonthlyBudgetClientTable(MonthlyFilter filter)
        {
            filter.UserId = UserHelper.GetLoginUserViewModel().UserId;
            BudgetModel model = new BudgetModel();

            var monthlyResponse = await _caliphAPIHelper.PostAsync<MonthlyFilter, ResponseData<List<BudgetMonthly>>>(filter, "/api/v1/budget/monthly-get-by-filter");
            model.Monthly.BudgetMonthly = monthlyResponse.Data;
            model.Monthly.Month = filter.BudgetMonth??DateTime.Now.Month;
            model.Monthly.Year = filter.BudgetYear?? DateTime.Now.Year;


            var clientFilter = new ClientFilterRequest { CreatedBy = UserHelper.GetLoginUserViewModel().Username, PageSize=999, PageNumber=1};
            var clientData = await _caliphAPIHelper.PostAsync<ClientFilterRequest, ResponseData<List<Client>>>(clientFilter, "/api/v1/client/get-by-client-filter");

            model.Monthly.Clients = clientData.Data;

            return PartialView("_MonthlyBudgetTable", model.Monthly);
        }

        [HttpPost]
        public async Task<JsonResult> Simulator(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<SimulatorAdd>(formCollection);

            int id = 0;
            int.TryParse(formCollection["StimulatorBudgetId"], out id);
            model.BudgetId = id;
            model.UserId = UserHelper.GetLoginUserViewModel().UserId;
            model.CreatedBy = UserHelper.GetLoginUser();
            model.ProductPricePlan = Convert.ToDecimal(model.PricePlan);
            var response = await _caliphAPIHelper.PostAsync<SimulatorAdd, ResponseData<string>>(model, "/api/v1/budget/simulator-add");
            if (response.StatusCode != "0000") //if cant create then update
            {
                var updateRespone = await _caliphAPIHelper.PostAsync<SimulatorAdd, ResponseData<string>>(model, "/api/v1/budget/simulator-update");
                return Json(updateRespone);

            }
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateTargetAppt(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<UpdateBudget>(formCollection);
            model.UpdatedBy = UserHelper.GetLoginUser();

            var updateRespone = await _caliphAPIHelper.PostAsync<UpdateBudget, ResponseData<string>>(model, "/api/v1/budget/target-appt-update");
            return Json(updateRespone);

        }

        [HttpPost]
        public async Task<ActionResult> RefreshTargetAppt(int incomeYear)
        {
            BudgetFilter filter = new BudgetFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId, BudgetYear = incomeYear };
            BudgetModel model = new BudgetModel();
            var response = await _caliphAPIHelper.PostAsync<BudgetFilter, ResponseData<List<BudgetView>>>(filter, "/api/v1/budget/simulator-get-by-filter");
            model.Income.IncomeData = response.Data.FirstOrDefault();

            return PartialView("_SalesApptEstimate", model.Income);
        }


        [HttpPost]
        public async Task<JsonResult> UpdateStrategy(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<StrategyAdd>(formCollection);
            model.UserId = UserHelper.GetLoginUserViewModel().UserId;
            model.CreatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<StrategyAdd, ResponseData<string>>(model, "/api/v1/budget/strategy-add");
            if (response.StatusCode != "0000") //if cant create then update
            {
                var updateRespone = await _caliphAPIHelper.PostAsync<StrategyAdd, ResponseData<string>>(model, "/api/v1/budget/strategy-update");
                return Json(updateRespone);

            }
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> AddGroup(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<GroupAdd>(formCollection);



           
            model.UserId = UserHelper.GetLoginUserViewModel().UserId;
            model.CreatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<GroupAdd, ResponseData<string>>(model, "/api/v1/budget/group-add");
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateGroup(List<BudgetGroup> list)
        { 
            var response = new ResponseData<string>();
            foreach (var item in list)
            {  
                response = await _caliphAPIHelper.PostAsync<BudgetGroup, ResponseData<string>>(item, "/api/v1/budget/group-update");
            }
            return Json(response);
        }


        [HttpPost]
        public async Task<JsonResult> UpdatePropotion(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<PropotionUpdate>(formCollection);
            model.UpdatedBy = UserHelper.GetLoginUser();
            var response = await _caliphAPIHelper.PostAsync<PropotionUpdate, ResponseData<string>>(model, "/api/v1/budget/proportion-update");
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> AddMonthlyDetail(FormCollection formCollection)
        {
            var response = new ResponseData<string>();
            var model = FormCollectionMapper.FormToModel<MonthlyAdd>(formCollection);
            model.UserId = UserHelper.GetLoginUserViewModel().UserId;
            model.CreatedBy = UserHelper.GetLoginUser();

            //foreach (var item in (BudgetTypeId[])Enum.GetValues(typeof(BudgetTypeId)))
            //{
            //    model.MonthlyBudgetTypeId = (int)item;
                response = await _caliphAPIHelper.PostAsync<MonthlyAdd, ResponseData<string>>(model, "/api/v1/budget/monthly-add");
            //}
            
            return Json(response);
        }


        //[HttpPost]
        //public async Task<JsonResult> UpdateMonthlyBudget(List<MonthlyUpdate> list)
        //{

        //    var response = new ResponseData<string>();
        //    foreach (var item in list)
        //    {
        //      response = await _caliphAPIHelper.PostAsync<MonthlyUpdate, ResponseData<string>>(item, "/api/v1/budget/monthly-update");
        //    }
        //    return Json(response);
             
        //}


        [HttpPost]
        public async Task<JsonResult> UpdateMonthlyBudget(MonthlyUpdate update)
        {
            var response =   await _caliphAPIHelper.PostAsync<MonthlyUpdate, ResponseData<string>>(update, "/api/v1/budget/monthly-update");
            return Json(response);
        }

        [HttpPost]
        public async Task<JsonResult> AddStrategy(FormCollection formCollection)
        {
            var model = FormCollectionMapper.FormToModel<StrategyAdd>(formCollection);
            model.UserId = UserHelper.GetLoginUserViewModel().UserId;
            model.CreatedBy = UserHelper.GetLoginUser();

            var response = await _caliphAPIHelper.PostAsync<StrategyAdd, ResponseData<string>>(model, "/api/v1/budget/strategy-add");

            return Json(response);
        }


        private SimulatorAdd PresetSimulator(string userName, int userId)
        {
            SimulatorAdd modelSimulator = new SimulatorAdd()
            {
                BudgetMonth = 6,
                BudgetYear = DateTime.Now.Year,
                CreatedBy = userName,
                PricePlan = "300.00",
                ProductPricePlan = 300,
                ProductQtyMonth1 = 4,
                ProductQtyMonth2 = 4,
                ProductQtyMonth3 = 4,
                ProductQtyMonth4 = 4,
                ProductQtyMonth5 = 4,
                ProductQtyMonth6 = 4,
                ProductQtyMonth7 = 4,
                ProductQtyMonth8 = 4,
                ProductQtyMonth9 = 4,
                ProductQtyMonth10 = 4,
                ProductQtyMonth11 = 4,
                ProductQtyMonth12 = 4,
                RecruitmentCount1 = 1,
                RecruitmentCount2 = 1,
                RecruitmentCount3 = 1,
                RecruitmentCount4 = 1,
                RecruitmentCount5 = 1,
                RecruitmentCount6 = 1,
                RecruitmentCount7 = 1,
                RecruitmentCount8 = 1,
                RecruitmentCount9 = 1,
                RecruitmentCount10 = 1,
                RecruitmentCount11 = 1,
                RecruitmentCount12 = 1,
                UserId = userId
            };
            return modelSimulator;
        }

        private GroupAdd PresetGroup(string userName, int userId, string budgetId, int count)
        {
            string title = string.Empty;
            decimal targetComm;
            int targetCount;
            string remark;
            if (count == 1)
            {
                title = "Personal Producer";
                remark = "No Repeat";
                targetComm = 30M;
                targetCount = 5;

            }
            else if (count == 2)
            {
                title = "Direct Group";
                remark = "No Repeat";
                targetComm = 6M;
                targetCount = 25;
            }
            else
            {
                remark = count.ToString();
                targetComm = 4M;
                targetCount = 125;
            }


            GroupAdd groupAdd = new GroupAdd()
            {
                BudgetId = Convert.ToInt32(budgetId),
                BudgetTitle = title,
                CreatedBy = userName,
                UserId = userId,
                Remarks = remark,
                TargetComm = targetComm.ToString("N2"),
                TargetCount = targetCount.ToString()

            };
            return groupAdd;
        }



        //refresh propotion
        [HttpPost]
        public async Task<ActionResult> GetMonthlyBudget(int year, int month)
        {
            MonthlyFilter monthlyfilter = new MonthlyFilter() { UserId = UserHelper.GetLoginUserViewModel().UserId, BudgetYear =year, BudgetMonth = month };
            var monthlyResponse = await _caliphAPIHelper.PostAsync<MonthlyFilter, ResponseData<List<BudgetMonthly>>>(monthlyfilter, "/api/v1/budget/monthly-get-by-filter");
           var data = monthlyResponse.Data;
            ;
            return Json( data);
        }
    }
}