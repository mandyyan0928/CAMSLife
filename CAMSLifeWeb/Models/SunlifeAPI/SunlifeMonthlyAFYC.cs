using System;

public class SunlifeMonthlyAFYCResponse {
    public DateTime date { get; set; }
    public double afyc { get; set; }
    public string advisorCode { get; set; }
    public string advisorName { get; set; }
}

public class SunlifeMonthyAFYCRequest
{
    public DateTime startDate { get; set; }
    public DateTime staendDatertDate { get; set; }
    public string advisorCode { get; set; }
    public int level { get; set; }
}