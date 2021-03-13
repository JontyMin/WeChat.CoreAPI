using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service.ERP
{
    public class VW_OrderVolume1Service : BaseService<VW_OrderVolume1>, IVW_OrderVolume1Service
    {

        public VW_OrderVolume1Service(IBaseRepository<VW_OrderVolume1> baseRepository) : base(baseRepository)
        {
        }
    }
}
