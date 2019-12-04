using System;
using System.Collections.Generic;
using System.Text;

namespace SqlPlus.Data.Test
{
    public class Event
    {
        public string Type { set; get; }
        public string Id { set; get; }
        public string Action { set; get; }

    }

    public class EventDistributor
    {
        public static void Send(Event evnt)
        {
            //Push To bus
        }
    }
}
/*
 * 
 * if(input.IsValid())
            {
                var output = service.ContactById(input);

                if(output.ReturnValue == ContactByIdOutput.Returns.NotFound)
                {
                    Console.WriteLine(
                        $"No record exists for ContactId:{input.ContactId}");
                }
                else
                {
                    var contact = output.ResultData;
                    Console.WriteLine(
                        $"Name:{contact.Name} " +
                        $"Email:{contact.Email} " +
                        $"Type: {contact.ContactType}");
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

    */
