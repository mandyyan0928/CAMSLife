using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API.AgentRecruit
{


    //public class AddAgentStimulatorRequest
    //{
    //    public int UserId { get; set; }
    //    public string AgentSimulatorYear { get; set; }
    //    public string AgentSimulatorMonth { get; set; }
    //    public int Manpower_YTDRecruit1 { get; set; }
    //    public int Manpower_YTDRecruit2 { get; set; }
    //    public int Manpower_YTDRecruit3 { get; set; }
    //    public int Manpower_YTDRecruit4 { get; set; }
    //    public int Manpower_YTDRecruit5 { get; set; }
    //    public int Manpower_YTDRecruit6 { get; set; }
    //    public int Manpower_YTDRecruit7 { get; set; }
    //    public int Manpower_YTDRecruit8 { get; set; }
    //    public int Manpower_YTDRecruit9 { get; set; }
    //    public int Manpower_YTDRecruit10 { get; set; }
    //    public int Manpower_YTDRecruit11 { get; set; }
    //    public int Manpower_YTDRecruit12 { get; set; }
    //    public int ActiveAgent_YTDRecruit1 { get; set; }
    //    public int ActiveAgent_YTDRecruit2 { get; set; }
    //    public int ActiveAgent_YTDRecruit3 { get; set; }
    //    public int ActiveAgent_YTDRecruit4 { get; set; }
    //    public int ActiveAgent_YTDRecruit5 { get; set; }
    //    public int ActiveAgent_YTDRecruit6 { get; set; }
    //    public int ActiveAgent_YTDRecruit7 { get; set; }
    //    public int ActiveAgent_YTDRecruit8 { get; set; }
    //    public int ActiveAgent_YTDRecruit9 { get; set; }
    //    public int ActiveAgent_YTDRecruit10 { get; set; }
    //    public int ActiveAgent_YTDRecruit11 { get; set; }
    //    public int ActiveAgent_YTDRecruit12 { get; set; }
    //    public int ActiveAgent_TotalCases1 { get; set; }
    //    public int ActiveAgent_TotalCases2 { get; set; }
    //    public int ActiveAgent_TotalCases3 { get; set; }
    //    public int ActiveAgent_TotalCases4 { get; set; }
    //    public int ActiveAgent_TotalCases5 { get; set; }
    //    public int ActiveAgent_TotalCases6 { get; set; }
    //    public int ActiveAgent_TotalCases7 { get; set; }
    //    public int ActiveAgent_TotalCases8 { get; set; }
    //    public int ActiveAgent_TotalCases9 { get; set; }
    //    public int ActiveAgent_TotalCases10 { get; set; }
    //    public int ActiveAgent_TotalCases11 { get; set; }
    //    public int ActiveAgent_TotalCases12 { get; set; }
    //    public int ACE_TotalCases1 { get; set; }
    //    public int ACE_TotalCases2 { get; set; }
    //    public int ACE_TotalCases3 { get; set; }
    //    public int ACE_TotalCases4 { get; set; }
    //    public int ACE_TotalCases5 { get; set; }
    //    public int ACE_TotalCases6 { get; set; }
    //    public int ACE_TotalCases7 { get; set; }
    //    public int ACE_TotalCases8 { get; set; }
    //    public int ACE_TotalCases9 { get; set; }
    //    public int ACE_TotalCases10 { get; set; }
    //    public int ACE_TotalCases11 { get; set; }
    //    public int ACE_TotalCases12 { get; set; }
    //    public string Remarks { get; set; }
    //    public string CreatedBy { get; set; }
    //}


    public class AgentStimulator
    {
        public int AgentSimulatorId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int AgentSimulatorYear { get; set; }
        public int AgentSimulatorMonth { get; set; }
        public double Manpower_YTDRecruit1 { get; set; }
        public double Manpower_YTDRecruit2 { get; set; }
        public double Manpower_YTDRecruit3 { get; set; }
        public double Manpower_YTDRecruit4 { get; set; }
        public double Manpower_YTDRecruit5 { get; set; }
        public double Manpower_YTDRecruit6 { get; set; }
        public double Manpower_YTDRecruit7 { get; set; }
        public double Manpower_YTDRecruit8 { get; set; }
        public double Manpower_YTDRecruit9 { get; set; }
        public double Manpower_YTDRecruit10 { get; set; }
        public double Manpower_YTDRecruit11 { get; set; }
        public double Manpower_YTDRecruit12 { get; set; }
        public double ActiveAgent_YTDRecruit1 { get; set; }
        public double ActiveAgent_YTDRecruit2 { get; set; }
        public double ActiveAgent_YTDRecruit3 { get; set; }
        public double ActiveAgent_YTDRecruit4 { get; set; }
        public double ActiveAgent_YTDRecruit5 { get; set; }
        public double ActiveAgent_YTDRecruit6 { get; set; }
        public double ActiveAgent_YTDRecruit7 { get; set; }
        public double ActiveAgent_YTDRecruit8 { get; set; }
        public double ActiveAgent_YTDRecruit9 { get; set; }
        public double ActiveAgent_YTDRecruit10 { get; set; }
        public double ActiveAgent_YTDRecruit11 { get; set; }
        public double ActiveAgent_YTDRecruit12 { get; set; }
        public double ActiveAgent_TotalCases1 { get; set; }
        public double ActiveAgent_TotalCases2 { get; set; }
        public double ActiveAgent_TotalCases3 { get; set; }
        public double ActiveAgent_TotalCases4 { get; set; }
        public double ActiveAgent_TotalCases5 { get; set; }
        public double ActiveAgent_TotalCases6 { get; set; }
        public double ActiveAgent_TotalCases7 { get; set; }
        public double ActiveAgent_TotalCases8 { get; set; }
        public double ActiveAgent_TotalCases9 { get; set; }
        public double ActiveAgent_TotalCases10 { get; set; }
        public double ActiveAgent_TotalCases11 { get; set; }
        public double ActiveAgent_TotalCases12 { get; set; }
        public double ACE_TotalCases1 { get; set; }
        public double ACE_TotalCases2 { get; set; }
        public double ACE_TotalCases3 { get; set; }
        public double ACE_TotalCases4 { get; set; }
        public double ACE_TotalCases5 { get; set; }
        public double ACE_TotalCases6 { get; set; }
        public double ACE_TotalCases7 { get; set; }
        public double ACE_TotalCases8 { get; set; }
        public double ACE_TotalCases9 { get; set; }
        public double ACE_TotalCases10 { get; set; }
        public double ACE_TotalCases11 { get; set; }
        public double ACE_TotalCases12 { get; set; }
        public string Remarks { get; set; }
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}