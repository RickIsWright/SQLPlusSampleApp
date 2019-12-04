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
                return "Server = DESKTOP-E6COJ7P; Initial Catalog = SqlPlusDemo; User ID = sa; Password = 3apples;";
            }
        }
    }
}
