using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service.ERP
{
    public class VW_OrderVolume3Service : BaseService<VW_OrderVolume3>, IVW_OrderVolume3Service
    {

        public VW_OrderVolume3Service(IBaseRepository<VW_OrderVolume3> baseRepository) : base(baseRepository)
        {
        }
    }
}
