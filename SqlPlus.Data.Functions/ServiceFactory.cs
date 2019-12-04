using Microsoft.Extensions.Logging;
using SqlPlus.Data.Default;
using SqlPlus.Data.Default.Models;
using SqlPlusDemo.Settings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

/// <summary>
/// Class for creating Service objects taylored to the needs of azure functions.
/// Limited retries and the logging object passed in from the function.
/// </summary>
namespace SqlPlus.Data.Functions
{
    public class ServiceFactory
    {
        private class FunctionLogger : ITransientLogger
        {
            private readonly ILogger log;

            public FunctionLogger(ILogger log)
            {
                this.log = log;
            }
            public void Log(SqlException sqlException)
            {
                throw new NotImplementedException();
            }
        }
        private class FunctionRetryOptions : RetryOptions
        {
            public FunctionRetryOptions(ILogger log) :
            base(
                new List<int> { 2, 4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001 },
                new List<int> { 1000, 2000, 5000 },
                new FunctionLogger(log)
                )
            { }
        }
        public static Service DefaultService(ILogger log)
        {
            return new Service(ConnectionStrings.Default, new FunctionRetryOptions(log));
        }
    }
}