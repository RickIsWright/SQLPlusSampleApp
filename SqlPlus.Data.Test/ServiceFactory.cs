using SqlPlus.Data.dbo;

namespace SqlPlus.Data.Test
{
    public class ServiceFactory
    {
        public static Service GetService()
        {
            return new Service("Server = DESKTOP-E6COJ7P; Initial Catalog = SqlPlusDemo; User ID = sa; Password = 3apples;");
        }
    }
}

/*
 * //Input parameters are mapped to an input object
            var input = new ContactByIdInput
            {
                ContactId = 1
            };

            //Every input object has an IsValid method
            if (input.IsValid())
            {
                //create a service object
                
                
                
                
                
                //output parameters and result sets
                if (output.ReturnValue == ContactByIdOutput.Returns.NotFound)
                {
                    Console.WriteLine("Not Found");
                }
                else
                {
                    var contact = output.ResultData;
                    Console.Write($"Name:{contact.Name} Email:{contact.Email} Type: {contact.ContactType}");
                }
                Assert.Pass();
            }
            else
            {
                //if the input is not valid Validation Results is populated
                foreach (ValidationResult vr in input.ValidationResults)
                {
                    Console.WriteLine(vr.ErrorMessage);
                }
                Assert.Fail();
            }

    */