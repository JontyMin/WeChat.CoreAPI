using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service.ERP
{
    public class ScanningCheckService:BaseService<ScanningCheck>, IScanningCheckService
    {
        public ScanningCheckService(IBaseRepository<ScanningCheck> baseRepository) : base(baseRepository)
        {
        }
    }
}