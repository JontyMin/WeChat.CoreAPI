using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Core.Model.EntityModel.ERP;

namespace WeChat.Core.Model.ViewModel.ERP
{
    public class SalesDynamics
    {
        public VW_SaleVolume1 f1 { get; set; }
        public string[] montharrcategory { get; set; }
        public int[] montharrorders { get; set; }
        public string[] dayarrcategory { get; set; }
        public int[] dayarrorders { get; set; }
        public decimal[] montharrsales { get; set; }
        public decimal[] dayarrsales { get; set; }

        public List<PlatSaleModel> f3 { get; set; }

        public DateTime GetNowTime { get; set; }
    }

    public class PlatSaleModel {
        public string platname { get; set; }

        public decimal monthsales { get; set; }

        public decimal yesderdaysales { get; set; }

        public decimal todaysales { get; set; }
    }
}
