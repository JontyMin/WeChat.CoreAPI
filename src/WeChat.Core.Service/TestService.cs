using WeChat.Core.IRepository;
using WeChat.Core.IService;
using WeChat.Core.Repository;

namespace WeChat.Core.Service
{
    public class TestService:ITestService
    {
        private readonly ITestRepository _testRepository;
        //private ITestRepository test = new TestRepository();

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        
        public int SumService(int i, int j)
        {
            return _testRepository.Sum(i, j);
        }

        public string payType()
        {
            return "WeChatPay";
        }
    }
}