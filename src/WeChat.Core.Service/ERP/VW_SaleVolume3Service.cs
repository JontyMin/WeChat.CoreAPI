using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service.ERP
{
    public class VW_SaleVolume3Service : BaseService<VW_SaleVolume3>, IVW_SaleVolume3Service
    {

        public VW_SaleVolume3Service(IBaseRepository<VW_SaleVolume3> baseRepository) : base(baseRepository)
        {
        }
    }
}
