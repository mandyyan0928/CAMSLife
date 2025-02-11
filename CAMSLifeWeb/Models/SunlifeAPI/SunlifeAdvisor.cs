using System;

public class SunlifeAdvisorListResponse
{
    public string leaderAdvisorCode { get; set; }
    public string leaderAdvisorRoleCode { get; set; }
    public string recruiterAdvisorCode { get; set; }
    public string recruiterAdvisorRoleCode { get; set; }
    public string spinOffAdvisorCode { get; set; }
    public string spinOffAdvisorRoleCode { get; set; }
    public string advisorCode { get; set; }
    public string roleCode { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public object race { get; set; }
    public object religion { get; set; }
    public string otherIc { get; set; }
    public string otherIcType { get; set; }
    public string gender { get; set; }
    public DateTime? birthDate { get; set; }
    public string maritalStatus { get; set; }
    public string bankName { get; set; }
    public string bankAccountNumber { get; set; }
    public DateTime? recruitDate { get; set; } = DateTime.MinValue;
    public string costCenterCode { get; set; }
    public string status { get; set; }
    public DateTime? terminationDate { get; set; }
    public DateTime? reinstatementDate { get; set; }
    public string liamNumber { get; set; }
    public string mtaNumber { get; set; }
    public string email { get; set; }

}

public class SunlifeAdvisorListRequest {
    public DateTime recruitOn { get; set; }
    public int pageNumber { get; set; }
    public int itemsPerPage { get; set; }
}


