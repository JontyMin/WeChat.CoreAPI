using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.BASE;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service.ERP
{
    public class SuperBuyerOrdersService:BaseService<SuperBuyerOrders>,ISuperBuyerOrdersService
    {
        public SuperBuyerOrdersService(IBaseRepository<SuperBuyerOrders> baseRepository) : base(baseRepository)
        {
        }
    }
}