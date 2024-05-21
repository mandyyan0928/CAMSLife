using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{


    public class MapaCalculatorViewModel {
        public MapaCalculatorViewModel()
        {
            PlannedMapa = new List<MapaPlanning>();
            CurrentYearActualMapa = new List<MapaPlanning>();
            PreviousYearActualMapa = new List<MapaPlanning>();
          //  PreviousMapaAchieve = new ActualMapaAchieve();
            Months = new List<int>();
        }
        public int ID { get; set; }
        //     public ActualMapaAchieve PreviousMapaAchieve { get; set; }
        public List<MapaPlanning> PlannedMapa { get; set; }
        public List<MapaPlanning> CurrentYearActualMapa { get; set; }
        public List<MapaPlanning> PreviousYearActualMapa { get; set; }
        public List<int> Months { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public int Type { get; set; }
    }

    public class MapaPlanning {
        public int Month { get; set; }
        public decimal Manpower_YTDRecruit { get; set; }
        public decimal ActiveAgent_YTDRecruit { get; set; }
        public decimal ActiveAgent_TotalCases { get; set; }
        public decimal ACE_TotalCases { get; set; }
        public decimal ACE { get; set; }
        public int ActiveAgent { get; set; }
        public int NewRecruit { get; set; }
        public int YtdRecruit { get; set; }
        public decimal ActiveRatio { get; set; }
        public decimal TotalCases { get; set; }
        public int Year { get; set; }
    }

    public class ActualMapaAchieve {
        public int Month { get; set; }
        public int Manpower_YTDRecruit { get; set; }
        public decimal ActiveAgent_YTDRecruit { get; set; }
        public decimal ActiveAgent_TotalCases { get; set; }
        public decimal ACE_TotalCases { get; set; }
        public decimal ACE { get; set; }
        public int ActiveAgent { get; set; }
        public int NewRecruit { get; set; }
        public int YtdRecruit { get; set; }
        public int TotalCases { get; set; }
    }

    public class DirectGroupMapaBuget
    {
        public int Month { get; set; }
        public int Manpower_YTDRecruit { get; set; }
        public decimal ActiveAgent_YTDRecruit { get; set; }
        public decimal ActiveAgent_TotalCases { get; set; }
        public decimal ACE_TotalCases { get; set; }
        public decimal ACE { get; set; }
        public decimal ACE_Percent { get; set; }
        public int ActiveAgent { get; set; }
        public int MPower { get; set; }
        public int Rec { get; set; }
        public int ActiveRatio { get; set; }
        public int TotalCases { get; set; }
    }

    public class DirectGroupMapaAchieve
    {
        public int Month { get; set; }
        public int Manpower_YTDRecruit { get; set; }
        public decimal ActiveAgent_YTDRecruit { get; set; }
        public decimal ActiveAgent_TotalCases { get; set; }
        public decimal ACE_TotalCases { get; set; }
        public decimal ACE { get; set; }
        public int ActiveAgent { get; set; }
        public int MPower { get; set; }
        public int Rec   { get; set; }
        public decimal ActiveRatio { get; set; }
        public int TotalCases { get; set; }
    }




    public class MapaSummaryViewModel
    {
        public MapaSummaryViewModel()
        {
            MapaSummaryQuarters = new List<MapaSummaryQuarter>();
        }
        public List<MapaSummaryQuarter>  MapaSummaryQuarters { get; set; }
    }

    public class MapaSummaryQuarter {
        public MapaSummaryQuarter()
        {
            MapaSummaryQuarters = new List<MapaSummaryQuarterDetails>();
        }
        public int Quarter { get; set; }
        public string QuarterName { get; set; }
        public List<MapaSummaryQuarterDetails> MapaSummaryQuarters { get; set; }
    }

    public class MapaSummaryQuarterDetails {
        public int Month { get; set; }
        public decimal Budget { get; set; }
        public decimal Achieved { get; set; }
        public decimal Percent { get; set; }
        public decimal MonthlyShortfall { get; set; }
        public decimal AccumulateShortFall { get; set; }
    }
}