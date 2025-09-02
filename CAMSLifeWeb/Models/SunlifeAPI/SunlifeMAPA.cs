using System;

public class SunlifeMAPAResponse
{
    public string advisorCode { get; set; }
    public string advisorName { get; set; }
    public string roleCode { get; set; }
    public DateTime recruitmentDate { get; set; }
    public int month { get; set; }
    public int year { get; set; }
    public decimal afycMtd { get; set; }
    public decimal afycYtd { get; set; }
    public int casesMtd { get; set; }
    public int casesYtd { get; set; }
    public int manpowerMtd { get; set; }
    public int manpowerYtd { get; set; }
    public int recruitMtd { get; set; }
    public int recruitYtd { get; set; }
    public int activeAdvisorMtd { get; set; }
    public decimal activeAdvisorYtd { get; set; }
}

public class SunlifeMAPARequest
{
    public string advisorCode { get; set; }
    public int? level { get; set; }
    public int startMonth { get; set; }
    public int startYear { get; set; }
    public int endMonth { get; set; }
    public int endYear { get; set; }
}


