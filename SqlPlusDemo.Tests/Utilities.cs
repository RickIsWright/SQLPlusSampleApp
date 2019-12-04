using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SqlPlusDemo.Tests
{
    public class Utilities
    {
        /// <summary>
        /// SQL+.NET - simply writing out the errors to the console
        /// </summary>
        public static void WriteValidationErrors(List<ValidationResult> list)
        {
            foreach (ValidationResult vr in list)
            {
                Console.WriteLine(vr.ErrorMessage);
            }
        }
    }
}
