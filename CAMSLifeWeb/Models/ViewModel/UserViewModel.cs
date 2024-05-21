using CaliphWeb.ViewModel.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.ViewModel
{
    public class UserViewModel
    {
     
      
            public int UserId { get; set; }
            public string Username { get; set; }
            public string DisplayName { get; set; }
            public string PW { get; set; }
            public string Fullname { get; set; }
            public string RoleCode { get; set; }
            public string RoleName { get; set; }
            public int RoleId { get; set; }
            public List<MenuList> MenuList { get; set; }

        public bool IsPotentialAgent => RoleId == (int)MasterDataEnum.RoleId.PontentialAgent;
        public bool IsAgent => RoleId == (int)MasterDataEnum.RoleId.Agent;
        public bool IsLeader => (RoleId == (int)MasterDataEnum.RoleId.Leader|| RoleId== (int)MasterDataEnum.RoleId.LeaderStaff);
        public bool IsStaffLeader => RoleId == (int)MasterDataEnum.RoleId.LeaderStaff;
        public bool IsAdmin => RoleId == (int)MasterDataEnum.RoleId.Admin;

        public bool AdminLoginAsAgent { get; set; }
        public UserViewModel AdminSession { get; set; }


    }

    public class MenuList
    {
        public int MainMenuId { get; set; }
        public string MainMenuName { get; set; }
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }
    }
   
    
}