using SqlPlusDemo.Settings;

namespace SqlPlusDemo.Tests
{
    public class ServiceFactory
    {
        public static SqlPlus.Data.Service Data()
        {
            return new SqlPlus.Data.Service(ConnectionStrings.Default);
        }

        public static SqlPlus.Data.SampleNamespace.Service Sample()
        {
            return new SqlPlus.Data.SampleNamespace.Service(ConnectionStrings.Default);
        }
    }
}
