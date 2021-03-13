using WeChat.Core.Common.Helper;

namespace WeChat.Core.Repository.SqlSugar
{
    public class BaseDBConfig
    {
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static string ConnectionString { get; set; } =
            AppSettings.app(new string[] {"AppSettings", "ConnectionString"});
    }
}