using System.Threading.Tasks;
using WeChat.Core.IService.BASE;
using WeChat.Core.Model.EntityModel;
using WeChat.Core.Model.ViewModel;

namespace WeChat.Core.IService
{
    public interface IWeChatSupplierService:IBaseService<WeChatSupplier>
    {
        /// <summary>
        /// 供应商入驻
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> CreateSupplier(ApplySettledViewModel model);
    }
}