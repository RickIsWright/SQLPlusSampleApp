using SqlPlus.Data;
using SqlPlusDemo.Settings;

namespace SqlPlusDemo.Tests
{
    public class ServiceFactory
    {
        public static Service Default()
        {
            return new SqlPlus.Data.Service(ConnectionStrings.Default);
        }

        public static SqlPlus.Data.SampleNamespace.Service NamespaceService()
        {
            return new SqlPlus.Data.SampleNamespace.Service(ConnectionStrings.Default);
        }

    }
}
