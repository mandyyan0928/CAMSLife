﻿using System;

public class SunlifeDailyAFYCResponse {
    public DateTime date { get; set; }
    public double afyc { get; set; }
    public string advisorCode { get; set; }
    public string advisorName { get; set; }
}

public class SunlifeDailyAFYCRequest
{

    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public string advisorCode { get; set; }
    public int level { get; set; }
}