using SqlPlus.Data.Default;
using SqlPlusDemo.Settings;

namespace SqlPlusDemo.Tests
{
    public class ServiceFactory
    {
        public static Service DefaultService()
        {
            return new Service(ConnectionStrings.Default);
        }
    }
}
