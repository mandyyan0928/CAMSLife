using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaliphWeb.Models.API.Agent;

namespace CaliphWeb.Models.API
{
    public class AgentCommissionTable
    {
        public AgentCommissionTable()
        {
            Agents = new List<AgentUser>();

        }
        public string CurrentYear { get; set; }
        public string LastYear { get; set; }
        public string CurrentYearjan1Amt { get; set; }
        public string CurrentYearjan2Amt { get; set; }
        public string CurrentYearjan3Amt { get; set; }
        public string CurrentYearfeb1Amt { get; set; }
        public string CurrentYearfeb2Amt { get; set; }
        public string CurrentYearfeb3Amt { get; set; }
        public string CurrentYearmar1Amt { get; set; }
        public string CurrentYearmar2Amt { get; set; }
        public string CurrentYearmar3Amt { get; set; }
        public string CurrentYearapr1Amt { get; set; }
        public string CurrentYearapr2Amt { get; set; }
        public string CurrentYearapr3Amt { get; set; }
        public string CurrentYearmay1Amt { get; set; }
        public string CurrentYearmay2Amt { get; set; }
        public string CurrentYearmay3Amt { get; set; }
        public string CurrentYearjun1Amt { get; set; }
        public string CurrentYearjun2Amt { get; set; }
        public string CurrentYearjun3Amt { get; set; }
        public string CurrentYearjul1Amt { get; set; }
        public string CurrentYearjul2Amt { get; set; }
        public string CurrentYearjul3Amt { get; set; }
        public string CurrentYearaug1Amt { get; set; }
        public string CurrentYearaug2Amt { get; set; }
        public string CurrentYearaug3Amt { get; set; }
        public string CurrentYearsep1Amt { get; set; }
        public string CurrentYearsep2Amt { get; set; }
        public string CurrentYearsep3Amt { get; set; }
        public string CurrentYearoct1Amt { get; set; }
        public string CurrentYearoct2Amt { get; set; }
        public string CurrentYearoct3Amt { get; set; }
        public string CurrentYearnov1Amt { get; set; }
        public string CurrentYearnov2Amt { get; set; }
        public string CurrentYearnov3Amt { get; set; }
        public string CurrentYeardec1Amt { get; set; }
        public string CurrentYeardec2Amt { get; set; }
        public string CurrentYeardec3Amt { get; set; }

        public string LastYearjan1Amt { get; set; }
        public string LastYearjan2Amt { get; set; }
        public string LastYearjan3Amt { get; set; }
        public string LastYearfeb1Amt { get; set; }
        public string LastYearfeb2Amt { get; set; }
        public string LastYearfeb3Amt { get; set; }
        public string LastYearmar1Amt { get; set; }
        public string LastYearmar2Amt { get; set; }
        public string LastYearmar3Amt { get; set; }
        public string LastYearapr1Amt { get; set; }
        public string LastYearapr2Amt { get; set; }
        public string LastYearapr3Amt { get; set; }
        public string LastYearmay1Amt { get; set; }
        public string LastYearmay2Amt { get; set; }
        public string LastYearmay3Amt { get; set; }
        public string LastYearjun1Amt { get; set; }
        public string LastYearjun2Amt { get; set; }
        public string LastYearjun3Amt { get; set; }
        public string LastYearjul1Amt { get; set; }
        public string LastYearjul2Amt { get; set; }
        public string LastYearjul3Amt { get; set; }
        public string LastYearaug1Amt { get; set; }
        public string LastYearaug2Amt { get; set; }
        public string LastYearaug3Amt { get; set; }
        public string LastYearsep1Amt { get; set; }
        public string LastYearsep2Amt { get; set; }
        public string LastYearsep3Amt { get; set; }
        public string LastYearoct1Amt { get; set; }
        public string LastYearoct2Amt { get; set; }
        public string LastYearoct3Amt { get; set; }
        public string LastYearnov1Amt { get; set; }
        public string LastYearnov2Amt { get; set; }
        public string LastYearnov3Amt { get; set; }
        public string LastYeardec1Amt { get; set; }
        public string LastYeardec2Amt { get; set; }
        public string LastYeardec3Amt { get; set; }

        public string YearPercent1 { get; set; }
        public string YearPercent2 { get; set; }
        public string YearPercent3 { get; set; }
        public string YearPercent4 { get; set; }
        public string YearPercent5 { get; set; }
        public string YearPercent6 { get; set; }
        public string YearPercent7 { get; set; }
        public string YearPercent8 { get; set; }
        public string YearPercent9 { get; set; }
        public string YearPercent10 { get; set; }
        public string YearPercent11 { get; set; }
        public string YearPercent12 { get; set; }

        public string CurrentYearTotal { get; set; }
        public string LastYearTotal { get; set; }
        public string YearPercentTotal { get; set; }

        public string DateString1 { get; set; }
        public string DateString2 { get; set; }
        public string DateString3 { get; set; }
        public string DateString4 { get; set; }
        public string DateString5 { get; set; }
        public string DateString6 { get; set; }
        public string DateString7 { get; set; }
        public string DateString8 { get; set; }
        public string DateString9 { get; set; }
        public string DateString10 { get; set; }
        public string DateString11 { get; set; }
        public string DateString12 { get; set; }
        public string DateString13 { get; set; }
        public string DateString14 { get; set; }
        public string DateString15 { get; set; }
        public string DateString16 { get; set; }
        public string DateString17 { get; set; }
        public string DateString18 { get; set; }
        public string DateString19 { get; set; }
        public string DateString20 { get; set; }
        public string DateString21 { get; set; }
        public string DateString22 { get; set; }
        public string DateString23 { get; set; }
        public string DateString24 { get; set; }
        public List<AdditionalRowTable> AdditionalRowTableList { get; set; }
        public List<AgentUser> Agents { get; set; }
    }
    public class AdditionalRowTable
    {
        public string Month { get; set; }
        public List<string> TableString { get; set; }
    }

    public class SummaryTableFunction
    {
        public string DateString1 { get; set; }
        public string DateString2 { get; set; }
        public DateTime DateDateTime1 { get; set; }
        public DateTime DateDateTime2 { get; set; }
        public DateTime DateDateTime3 { get; set; }
        public DateTime DateDateTime4 { get; set; }
        public DateTime DateDateTime5 { get; set; }
        public DateTime DateDateTime6 { get; set; }
        public DateTime DateDateTime7 { get; set; }
        public DateTime DateDateTime8 { get; set; }
        public List<AdditionalRowTable> AdditionalRowTableList { get; set; }
    }

    public class StartdatEnddate
    {
        public DateTime CycleStartDate { get; set; }
        public DateTime CycleEndDate { get; set; }
    }


    public class NewCommissionSummaryViewModel
    {
        public string CurrentYear { get; set; }
        public string LastYear { get; set; }

        public List<AgentCommission> CurrentYearData { get; set; } = new List<AgentCommission>();
        public List<AgentCommission> LastYearData { get; set; } = new List<AgentCommission>();

    }
}