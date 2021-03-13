using System;
using System.Collections.Generic;
using System.Text;

namespace WeChat.Core.Model.EntityModel.ERP
{
    public class VW_SaleVolume1
    {
        public decimal TodaySales { get; set; }

        public decimal YesterdaySales { get; set; }

        public decimal MonthSales { get; set; }

        public decimal YearSales { get; set; }

        public decimal MPurAmount { get; set; }

        public decimal TPurAmount { get; set; }

        public decimal YPurAmount { get; set; }
    }
}
