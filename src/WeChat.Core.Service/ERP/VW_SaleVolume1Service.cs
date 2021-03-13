using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service.ERP
{
    public class VW_SaleVolume1Service : BaseService<VW_SaleVolume1>, IVW_SaleVolume1Service
    {

        public VW_SaleVolume1Service(IBaseRepository<VW_SaleVolume1> baseRepository) : base(baseRepository)
        {
        }
    }
}
