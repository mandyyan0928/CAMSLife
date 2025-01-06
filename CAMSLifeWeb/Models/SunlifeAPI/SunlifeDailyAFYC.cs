﻿using System;

public class SunlifeDailyAFYCResponse {
    public int month { get; set; }
    public int year { get; set; }
    public double afyc { get; set; }
    public string advisorCode { get; set; }
    public string advisorName { get; set; }
}

public class SunlifeDailyAFYCRequest
{
    public int startMonth { get; set; }
    public int startYear { get; set; }
    public int endMonth { get; set; }
    public int endYear { get; set; }
    public string advisorCode { get; set; }
    public int level { get; set; }
}