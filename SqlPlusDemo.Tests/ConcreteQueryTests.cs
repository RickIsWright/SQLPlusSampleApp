using NUnit.Framework;
using SqlPlus.Data.Models;
using SqlPlus.Data.SampleNamespace.Models;
using System;

namespace SqlPlusDemo.Tests
{
    public class ConcreteQueryTests
    {
        [Test]
        public void HelloWorldTest()
        {
            var input = new HelloWorldInput { Name = "Alan" };
            Assert.IsTrue(input.IsValid());

            var result = ServiceFactory.Sample().HelloWorld(input);

            Assert.IsTrue(result.ReturnValue == HelloWorldOutput.Returns.Ok);

            Console.WriteLine(result.ResultData.WelcomMessage);

        }
    }
}
