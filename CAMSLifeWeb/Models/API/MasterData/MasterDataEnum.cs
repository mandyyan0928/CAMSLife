namespace CaliphWeb.ViewModel.Data
{
    public class MasterDataEnum
    {
        public enum MasterData
        {
            LengthOfTimeKnown = 2,
            AnnualIncome,
            Occupation,
            MaritalStatus,
            HowWellKnown,
            HowOftenSeenInAYear,
            CouldApproachOnBusiness,
            AbilityToProvideReferrals,
            Source,
            ClientContact,
            AgeRange,
            Gender,
            DealTitle,
            ClientRelationship,
            Activity,
            EducationBackground=20,
            AgentRecruitType=31,
            PaymentChannel= 29, 
            AttendanceStatus =26, 
            UserEventStatus=28
        }


        public enum Status
        {
            Active=1, 
            Inactive=2,
            Confirm,
            Potential,
            Closed =5,
            KIV,
            Lost =7,
            Missed,
            Done,
            Leads=10,
            ConvertedToClient=11,
            Archive = 12,
            Going= 168,
            NotGoing=169,
            PendingPayment= 174,
            Attended= 157,
          //  Missed= 158
        }

        public enum PaymentStatus
        {
            
            Pending = 180,
            Success = 181,
            Failed = 182,
            //  Missed= 158
        }

 
        public enum RoleId
        {
            Admin = 1,
            Agent = 2,
            Leader =3,
            PontentialAgent=4,
            LeaderStaff = 5,
        }


        public enum ActivityPoint
        {
            SalesCall=1,
            ApptSecured,
            Survey,
            ApproachInPerson,
            ClosingInterview,
            Servicing_Followup,
            ReferralsLead,
            Salesappointments,
            Sales,
            AgentContracted,
            Training,
            JointFieldWork,
            Coaching_OneToOne,
            MiniBOP,
            ACE,
        }

        public enum PointSummaryOrdering
        {
            SalesCall        = 1,
            ApptSecured      = 2,
            Survey           = 3,
            ApprochInPerson  = 4,
            RefLeads         = 5,
            ClosingInterview = 6,
            Sales            = 7,
            Servicing        = 8,
            AgentContracted  = 9,
            Training         = 10,
            JoinFieldWork    = 11,
            Coaching         = 12,
            MiniBop          = 13,
            ACE              = 14
        }
        public enum SalesActivityPoint
        {
            SalesCall = 1,
            ApptSecured=2,
            Survey=3,
            ApproachInPerson=4,
            ClosingInterview=5,
            Servicing_Followup=6,
            RefLeads = 7,
            Salesappointments =8,
            Sales = 9,
            AgentContracted=10,
            Training = 11,
            JoinWorkField = 12,
            Coaching_One2One = 13,
            MiniBOP = 14,
           ACE=15,
            RecruitmentCall = 16,
            RecruitmentApproach = 17,
            InitialInterview = 18,
            CareerPresentation = 19,
            VIPInterview = 20,
            JobSampling = 21,
            FinalInterview =22,

        }

        public enum BudgetTypeId { 
        
            HighEnd = 115,
            LowEnd=116
        }

        public enum ActivityReviewType
        {

            PlannedActivity = 113,
            ActualActivity = 114
        }


        public enum PaymentChannel
        {

            MobiPay = 175,
            PayLater = 176,
            Atome = 177,
                Stripe = 178,
            CommissionDeduct = 179,
        }


        public enum AnnouncementType
        {

            All = 111,
            Specific = 112,
        }

        public enum AgentStimulatorType
        {
 WholeGroup = 194, 
 DirectGroup = 128
        }

        public class one2oneRelationType
        {
            public const string DIRECT_GROUP = "u";
            public const string PERSONAL = "p";
            public const string G1 = "g1";
            public const string G2 = "g2";
            public const string G3 = "g3";
            public const string G4 = "g4";
            public const string G5 = "g5";
            public const string G6 = "g6";

        }



        public class One2OneConfig
        {
            public const string COMPANY_ACCOUNT = "CAL888";
            public const string TOP_LEADER = "A00800";

        }


      
     

       
    }
}