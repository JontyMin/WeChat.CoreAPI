using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service.ERP
{
    public class VW_OrderVolume2Service : BaseService<VW_OrderVolume2>, IVW_OrderVolume2Service
    {

        public VW_OrderVolume2Service(IBaseRepository<VW_OrderVolume2> baseRepository) : base(baseRepository)
        {
        }
    }
}
