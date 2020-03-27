using NUnit.Framework;
using System;

namespace SqlPlusDemo.Tests
{
    public class MiscTests
    {
        [Test]
        public void GetDateTest()
        {
            var result = ServiceFactory.Data().GetSQLDateTime();
            Assert.IsTrue(result != null);
            Console.WriteLine(result.ReturnValue);
        }
    }
}
