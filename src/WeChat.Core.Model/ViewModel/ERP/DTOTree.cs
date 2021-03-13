using System.Collections.Generic;

namespace WeChat.Core.Model.ViewModel.ERP
{
    public class DTOTree
    {
        //标题
        public string text { get; set; }
        public string value { get; set; }
        public List<DTOTree> children { get; set; }
    }
}
