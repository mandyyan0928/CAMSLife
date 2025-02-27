using System;

public class SunlifePolicyResponse
{
    public string leaderAdvisorCode { get; set; }
    public string leaderAdvisorRoleCode { get; set; }
    public string spinOffAdvisorCode { get; set; }
    public string spinOffAdvisorFullName { get; set; }
    public string sellingAdvisorCode { get; set; }
    public string sellingAdvisorName { get; set; }
    public string serviceAdvisorCode { get; set; }
    public string serviceAdvisorName { get; set; }
    public string policyNumber { get; set; }
    public string policyStatus { get; set; }
    public DateTime issueDate { get; set; }
    public DateTime commencementDate { get; set; }
  
    public DateTime? dueDate { get; set; }
    public DateTime? lastInstalmentDate { get; set; }
    public DateTime? inceptionDate { get; set; }
    public DateTime? proposalReceivedDate { get; set; }
    public decimal instalmentPremiumAmount { get; set; }
    public DateTime? lastStatusChangeDate { get; set; }
    public string productCode { get; set; }
    public string balancedScoreCard { get; set; }
   
}

public class SunlifePolicyRequest {
    public string advisorCode { get; set; }
    public int? level { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
}


