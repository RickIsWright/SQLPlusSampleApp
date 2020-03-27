using SqlPlus.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SqlPlusDemo.Web.Models
{
    /// <summary>
    /// SQL+.NET
    /// Creating a custom view model then mapping objects is silly.
    /// Simply derive from the input object to extend functionality.
    /// Add properties for the little math gadget
    /// </summary>
    public class FeedbackInputViewModel : FeedbackInsertInput
    {
        public FeedbackInputViewModel()
        {
            Random random = new Random();
            NumberOne = random.Next(1, 10);
            NumberTwo = random.Next(1, 10);
        }

        public int NumberOne { set; get; }

        public int NumberTwo { set; get; }

        public int NumberResult { set; get; }

        /// <summary>
        /// SQL+.NET Enforcing some custom validation in the derived class
        /// </summary>
        public override bool IsValid()
        {
            if (NumberOne + NumberTwo != NumberResult)
            {
                List<string> memberNames = new List<string> { nameof(NumberResult) };
                ValidationResults.Add(new ValidationResult($"Sorry, {NumberOne} + {NumberTwo} does not equal {NumberResult}", memberNames));
            }
            return base.IsValid();
        }
    }
}
