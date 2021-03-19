namespace WeChat.Core.Model.ViewModel.ERP
{
    public class ScanStatus
    {
        /// <summary>
        /// 采购单号
        /// </summary>
        public string PurchaseNo { get; set; }

        /// <summary>
        /// 是否核实
        /// </summary>
        public bool Verify { get; set; }

        /// <summary>
        /// 快递公司名称
        /// </summary>
        public string LogisticsCompanyName { get; set; } = "";

    }

    public class SignModel
    {
        public string appid { get; set; }

        public string sign { get; set; }

        public string nonce { get; set; }
        
        public string timespan { get; set; }
    }
}