using System;
using System.Linq;
using System.Threading.Tasks;
using WeChat.Core.IRepository.BASE;
using WeChat.Core.IService.ERP;
using WeChat.Core.Model;
using WeChat.Core.Model.EntityModel.ERP;
using WeChat.Core.Model.Enums;
using WeChat.Core.Model.ViewModel.ERP;
using WeChat.Core.Service.BASE;

namespace WeChat.Core.Service.ERP
{
    public class CategoryFormEbayUKService:BaseService<CategoryFormEbayUK>,ICategoryFormEbayUKService
    {

        public CategoryFormEbayUKService(IBaseRepository<CategoryFormEbayUK> baseRepository) : base(baseRepository)
        {
        }

    }
}