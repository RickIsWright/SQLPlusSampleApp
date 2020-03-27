using NUnit.Framework;
using SqlPlus.Data.Models;
using System;

namespace SqlPlusDemo.Tests
{
    /// <summary>
    /// This class illustrates testing validation of inputs as well as execution of procedures.
    /// </summary>
    public class FeedbackTests
    {

        /// <summary>
        /// Illustrating a valid call for insert - all properties pass validation
        /// and we leave the Feedback Id set to it's default 0
        /// Utilizing dbo.FeedbackUpsert
        /// </summary>
        [Test]
        [Order(1)]
        public void ValidInsert()
        {
            var input = new FeedbackInsertInput
            {
                Email = "Alan@SQLPLUS.net",
                FirstName = "Alan",
                LastName = "Hyneman",
                Message = "This is a message",
                Subject = "Thank is a subject"
            };
            Assert.IsTrue(input.IsValid());

            //Call the service to get the output object
            var output = ServiceFactory.Data().FeedbackInsert(input);

            //We can test the return value against the enumeration for Inserted
            Assert.IsTrue(output.ReturnValue == FeedbackInsertOutput.Returns.Ok);

        }

        /// <summary>
        /// Illustrating an inline-table function 
        /// </summary>
        [Test]
        [Order(2)]
        public void FeedbackTable()
        {
            var input = new FeedbackTableInput
            {
                FeedbackId = 1
            };
            Assert.IsTrue(input.IsValid());

            //Call the service to get the output object
            var output = ServiceFactory.Data().FeedbackTable(input);
            Console.WriteLine(output.ResultData.Created);
        }

        /// <summary>
        /// Illustrating a valid call for delete - we use the value from 
        /// the previously created record (1)
        /// </summary>
        [Test]
        [Order(3)]
        public void FeedbackTableMulti()
        {
            var input = new FeedbackTableMultiInput
            {
                FeedbackId = 1
            };
            Assert.IsTrue(input.IsValid());

            //Call the service to get the output object
            var output = ServiceFactory.Data().FeedbackTableMulti(input);

            foreach(FeedbackTableMultiResult row in output.ResultData)
            {
                Console.WriteLine(row.LastName);
            }
        }

        /// <summary>
        /// Illustrating a valid call for update - all properties pass validation
        /// and we use the value from the previously created record (1)
        /// Utilizing dbo.FeedbackUpsert
        /// </summary>
        [Test]
        [Order(4)]
        public void ValidUpdate()
        {
            var input = new FeedbackUpdateInput
            {
                FeedbackId = 1,
                Email = "SomeoneElse@SQLPLUS.net",
                FirstName = "Someone",
                LastName = "Else",
                Message = "This is a different message",
                Subject = "A different subject"
            };
            Assert.IsTrue(input.IsValid());
            
            //Call the service to get the output object
            var output = ServiceFactory.Data().FeedbackUpdate(input);

            //We can test the return value against the enumeration for Modified
            Assert.IsTrue(output.ReturnValue == FeedbackUpdateOutput.Returns.Ok);

        }

        /// <summary>
        /// Illustrating the required tag - all properties are marked required
        /// and Feedback Id has a default of zero.
        /// </summary>
        [Test]
        [Order(5)]
        public void NullParameters()
        {
            var input = new FeedbackInsertInput
            {
                //Email = "Alan@SQLPLUS.net",
                //FirstName = "Alan",
                //LastName = "Hyneman",
                //Message = "So grateful for everyone providing feedback",
                //Subject = "Thank You"
            };

            //Expecting a failure
            Assert.IsTrue(input.IsValid() == false);

            Utilities.WriteValidationErrors(input.ValidationResults);
        }

        /// <summary>
        /// Illustrating the Email tag, email is not valid but all required fields pass
        /// </summary>
        [Test]
        [Order(6)]
        public void InvalidEmail()
        {
            var input = new FeedbackInsertInput
            {
                Email = "ThisIsNotAnEmail",
                FirstName = "Alan",
                LastName = "Hyneman",
                Message = "So grateful for everyone providing feedback",
                Subject = "Thank You"
            };
            //Expecting a failure
            Assert.IsTrue(input.IsValid() == false);
            Utilities.WriteValidationErrors(input.ValidationResults);
        }


        /// <summary>
        /// Illustrating a valid call for select by id
        /// Utilizing dbo.FeedbackById
        /// </summary>
        [Test]
        [Order(7)]
        public void ById()
        {
            var input = new FeedbackByIdInput
            {
                FeedbackId = 1
            };
            Assert.IsTrue(input.IsValid());

            //Call the service to get the output object
            var output = ServiceFactory.Data().FeedbackById(input);

            //We can test the return value against the enumeration for Ok
            Assert.IsTrue(output.ReturnValue == FeedbackByIdOutput.Returns.Ok);

            //Check the result data aligns with the values from test 2
            Assert.IsTrue(output.ResultData.Email == "SomeoneElse@SQLPLUS.net");

            var feedback = output.ResultData;
            Console.WriteLine(feedback.Created);
            Console.WriteLine(feedback.Email);
            Console.WriteLine(feedback.FeedbackId);
            Console.WriteLine(feedback.FirstName);
            Console.WriteLine(feedback.LastName);
            Console.WriteLine(feedback.Message);
            Console.WriteLine(feedback.Subject);
        }

        /// <summary>
        /// Illustrating a valid call for delete - we use the value from 
        /// the previously created record (1)
        /// </summary>
        [Test]
        [Order(8)]
        public void ValidDelete()
        {
            var input = new FeedbackDeleteInput
            {
                FeedbackId = 1
            };
            Assert.IsTrue(input.IsValid());

            //Call the service to get the output object
            var output = ServiceFactory.Data().FeedbackDelete(input);

            //We can test the return value against the enumeration for Deleted
            Assert.IsTrue(output.ReturnValue == FeedbackDeleteOutput.Returns.Ok);

        }
    }
}
