namespace CaliphWeb.Models.API
{
    public class UpdateActivityReviewRequest
    {
        public int ActivityReviewId { get; set; }
        public int ActivityReviewTypeId { get; set; }
        public int UserId { get; set; }
        public string DateInWeek { get; set; }
        public string ReviewText1 { get; set; }
        public string ReviewText2 { get; set; }
        public string ReviewText3 { get; set; }
        public string ReviewText4 { get; set; }
        public string ReviewText5 { get; set; }
        public string ReviewText6 { get; set; }
        public string ReviewText7 { get; set; }
        public string ReviewText8 { get; set; }
        public string ReviewText9 { get; set; }
        public string ReviewText10 { get; set; }
        public string ReviewText11 { get; set; }
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }
    }

}