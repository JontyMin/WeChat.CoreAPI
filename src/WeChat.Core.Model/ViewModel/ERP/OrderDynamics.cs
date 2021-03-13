using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Core.Model.EntityModel.ERP;

namespace WeChat.Core.Model.ViewModel.ERP
{
    public class OrderDynamics
    {
        public VW_OrderVolume1 v1 { get; set; }
        public List<PlatOrdersModel> v2 { get; set; }
        
        public string[] montharrcategory { get; set; }
        public int[] montharrorders { get; set; }
        public string[] dayarrcategory { get; set; }
        public int[] dayarrorders { get; set; }

        public DateTime GetNowTime { get; set; }
    }

    public class PlatOrdersModel { 
        public string platname { get; set; }

        public int todayorders { get; set; }

        public int yesderdayorders { get; set; }

        public int thisyearorders { get; set; }
    }
}
