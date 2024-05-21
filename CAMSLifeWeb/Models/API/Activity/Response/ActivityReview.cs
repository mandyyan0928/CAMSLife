using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.API
{
    public class ActivityReview
    {

        public int ActivityReviewId { get; set; }
        public int ActivityReviewTypeId { get; set; }
        public string ActivityReviewTypeDesc { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
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
        public int StatusId { get; set; }
        public string StatusDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}