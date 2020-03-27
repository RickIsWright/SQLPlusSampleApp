using System;

namespace SqlPlusDemo.Settings
{
    /// <summary>
    /// This is just a hacky way to make the connection string available to all the projects, this is after all a demo.
    /// </summary>
    public class ConnectionStrings
    {
        public static String Default
        {
            get
            {
                return "Server = (local); Database = SqlPlusDemo; Integrated Security = true;";
            }
        }
    }
}