using System;

namespace SqlPlusDemo.Settings
{
    /// <summary>
    /// I did this so you don't have to edit configuration files all over the place.
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
