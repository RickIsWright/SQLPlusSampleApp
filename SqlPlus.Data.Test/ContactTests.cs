using NUnit.Framework;
using SqlPlus.Data.dbo;
using SqlPlus.Data.dbo.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SqlPlus.Data.Test
{
    public class ContactTests
    {
        Service service = ServiceFactory.GetService();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SelectById()
        {

            var input = new ContactByIdInput
            {
                //ContactId = 0
            };
            
            if(input.IsValid())
            {
                var output = service.ContactById(input);

                if(output.ReturnValue == ContactByIdOutput.Returns.Ok)
                {
                    var contact = output.ResultData;

                    Console.WriteLine(contact.Name);
                    Console.WriteLine(contact.Email);
                    Console.WriteLine(contact.ContactType);
                }
                else
                {
                    Console.WriteLine(output.ReturnValue);
                }

                Assert.Pass();
            }
            else
            {
                foreach(ValidationResult validationResult in input.ValidationResults)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }
                Assert.Fail();
            }
        }
    }
}
