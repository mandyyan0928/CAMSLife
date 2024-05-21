using System;

namespace CaliphWeb.ViewModel
{
    public class MasterData
    {
        public int MasterDataId { get; set; }
        public int MasterId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }


}