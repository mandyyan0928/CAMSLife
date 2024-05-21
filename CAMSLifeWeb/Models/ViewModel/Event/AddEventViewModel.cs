using CaliphWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Models.ViewModel
{
    public class AddEventViewModel
    {
        public AddEventViewModel()
        {
            EventTypeList = new List<MasterData>();
            EventHostList = new List<MasterData>();
            EventChannelList = new List<MasterData>();
            AttendantTypeList = new List<MasterData>();
        }

        public List<MasterData> EventTypeList { get; set; }

        public List<MasterData> EventHostList { get; set; }

        public List<MasterData> EventChannelList { get; set; }

        public List<MasterData> AttendantTypeList { get; set; }
    }
}