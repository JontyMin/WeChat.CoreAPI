using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service.ERP
{
    public class PurchaseQuestionService:BaseService<PurchaseQuestion>,IPurchaseQuestionService
    {
        public PurchaseQuestionService(IBaseRepository<PurchaseQuestion> baseRepository) : base(baseRepository)
        {
        }
    }
}