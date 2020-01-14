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
                //Edit the server name and password

                return "Server = {server}; Initial Catalog = SqlPlusDemo; User ID = sa; Password = {password}";
            }
        }
    }
}
