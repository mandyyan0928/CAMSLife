using CaliphWeb.Models;
using System;
using System.Collections.Generic;

namespace CaliphWeb.Services
{

    public class BonusService
    {
        public BonusService()
        {

        }

        public static List<BonusContest> GetContests(DateTime joinDate )
        {

            if (joinDate >= new DateTime(2022, 6, 1))
                return GetRookieContests();
            else
                return GetNonRookieContests();

          
        }


        public static List<BonusContest> GetNonRookieContests()
        {
            if (BonusContests == null)
            {
                BonusContests = new List<BonusContest>();
                BonusContests.Add(new BonusContest { FromNetACE= 40000, ToNetACE = 60000-1, PR_D0 = 90, Cases = 12, BonusPercent = 0.04 });
                BonusContests.Add(new BonusContest { FromNetACE = 60000, ToNetACE = 90000-1, PR_D0 = 90, Cases = 12, BonusPercent = 0.06 });
                BonusContests.Add(new BonusContest { FromNetACE = 90000, ToNetACE = 120000-1, PR_D0 = 90, Cases = 12, BonusPercent =0.08 });
                BonusContests.Add(new BonusContest { FromNetACE= 120000,   ToNetACE = 240000-1, PR_D0 = 90, Cases = 14, BonusPercent =0.09 });
                BonusContests.Add(new BonusContest { FromNetACE = 240000, ToNetACE = 360000-1, PR_D0 = 90, Cases = 14, BonusPercent =0.11 });
                BonusContests.Add(new BonusContest { FromNetACE = 360000, ToNetACE = 999999999999, PR_D0 = 90, Cases = 14, BonusPercent =0.13 });
                return BonusContests;
            }
            else
                return BonusContests;
        }
        public static List<BonusContest> GetRookieContests()
        {
            if (RookieBonusContests == null)
            {
                RookieBonusContests = new List<BonusContest>();
                RookieBonusContests.Add(new BonusContest { FromNetACE = 28000, ToNetACE = 42000 - 1, PR_D0 = 90, Cases = 8, BonusPercent = 0.04 });
                RookieBonusContests.Add(new BonusContest { FromNetACE = 42000, ToNetACE = 63000 - 1, PR_D0 = 90, Cases = 8, BonusPercent = 0.06 });
                RookieBonusContests.Add(new BonusContest { FromNetACE = 63000  , ToNetACE = 84000 - 1, PR_D0 = 90, Cases = 8, BonusPercent = 0.08 });
                RookieBonusContests.Add(new BonusContest { FromNetACE = 84000, ToNetACE = 168000 - 1, PR_D0 = 90, Cases = 10, BonusPercent = 0.09 });
                RookieBonusContests.Add(new BonusContest { FromNetACE = 168000, ToNetACE = 252000 - 1, PR_D0 = 90, Cases = 10, BonusPercent = 0.11 });
                RookieBonusContests.Add(new BonusContest { FromNetACE = 252000, ToNetACE = 999999999999, PR_D0 = 90, Cases = 10, BonusPercent = 0.13 });
                return RookieBonusContests;
            }
            else
                return RookieBonusContests;
        }
        private static List<BonusContest>  BonusContests { get; set; }
        private static List<BonusContest> RookieBonusContests { get; set; }
    }


}