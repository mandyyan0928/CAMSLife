public class SunlifeHierarchyResponse
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
    public string race { get; set; }
    public string religion { get; set; }
    public string otherIc { get; set; }
    public string otherIcType { get; set; }
    public string gender { get; set; }
    public string birthDate { get; set; }
    public string maritalStatus { get; set; }
    public string bankName { get; set; }
    public string bankAccountNumber { get; set; }
    public string recruitDate { get; set; }
    public string costCenterCode { get; set; }

}

public class SunlifeHierarcyRequest {
    public string advisorCode { get; set; }
    public string level { get; set; }
}


