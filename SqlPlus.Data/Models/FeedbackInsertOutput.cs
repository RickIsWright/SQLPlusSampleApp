// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by SqlPlus.net
//     For more information on SqlPlus.net visit http://www.SqlPlus.net
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SqlPlus.Data.Models
{
    /// <summary>
    /// Output object for FeedbackInsert method.
    /// </summary>
    public partial class FeedbackInsertOutput
    {
        public enum Returns
        {
             Ok = 1
        }

		public int? FeedbackId { set; get; }

		public Returns ReturnValue { set; get; }

    }
}