using CaliphWeb.ViewModel;
using CaliphWeb.ViewModel.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CaliphWeb.Helper
{
    public class UserHelper
    {
        public static string GetLoginUser()
        {
            var user = (UserViewModel)HttpContext.Current.Session["user"];

            if (user == null)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return "";
            }
            return user.Username;
        }
        public static int GetLoginRole()
        {
            var user = (UserViewModel)HttpContext.Current.Session["user"];

            if (user == null)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return 0;
            }
            return user.RoleId;
        }
        public static UserViewModel GetLoginUserViewModel()
        {
            var user = (UserViewModel)HttpContext.Current.Session["user"];

            if (user == null)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return new UserViewModel();
            }
            return user;
        }

        public static string GetDefaultSearchUser()
        {
            var user = (UserViewModel)HttpContext.Current.Session["user"];

            if (user == null)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return "";
            }

            if (user.IsAdmin)
                return null;
            else if (user.IsAgent || user.IsLeader)
                return user.Username;


            return user.Username;
        }
        public static string GetDefaultOne2OneSearchUser()
        {
            var user = (UserViewModel)HttpContext.Current.Session["user"];

            if (user == null)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return "";
            }

            if (user.IsAdmin || user.IsStaffLeader)
                return "CAL888";
            else if (user.IsAgent || user.IsLeader)
                return user.Username;


            return user.Username;
        }
        public static bool IsCompanyAccount(string user)
        {
            return user == MasterDataEnum.One2OneConfig.COMPANY_ACCOUNT;
        }

        public static bool IsLeader(string user)
        {
            return (user == MasterDataEnum.SLMConfig.ROLE_BD|| user== MasterDataEnum.SLMConfig.ROLE_BM);
        }

        public static bool IsAgent(string user)
        {
            return (user == MasterDataEnum.SLMConfig.ROLE_BE || user == MasterDataEnum.SLMConfig.ROLE_SBE);
        }

        public static string GetTopLeader( )
        {

            var user = GetLoginUserViewModel();
            if (IsCompanyAccount(user.Username))
            {
                return MasterDataEnum.One2OneConfig.TOP_LEADER;
            }
            else
            {
                return user.Username;
            }
        }

    }
}