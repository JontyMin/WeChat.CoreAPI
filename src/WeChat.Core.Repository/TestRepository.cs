using WeChat.Core.IRepository;

namespace WeChat.Core.Repository
{
    public class TestRepository: ITestRepository
    {
        public int Sum(int i, int j)
        {
            return i + j;
        }
    }
}