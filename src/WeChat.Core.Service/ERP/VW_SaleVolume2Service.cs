using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service.ERP
{
    public class VW_SaleVolume2Service : BaseService<VW_SaleVolume2>, IVW_SaleVolume2Service
    {

        public VW_SaleVolume2Service(IBaseRepository<VW_SaleVolume2> baseRepository) : base(baseRepository)
        {
        }
    }
}
