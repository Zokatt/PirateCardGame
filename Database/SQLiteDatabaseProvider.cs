using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace PriateCardGame.Database
{
    /// <summary>
    /// Used to manage the connection to the database
    /// </summary>
    ///<remarks>
    /// Nikolaj
    /// </remarks>
    public class SQLiteDatabaseProvider
    {
        private readonly string connectionString;

        public SQLiteDatabaseProvider(string Connectionstring)
        {
            this.connectionString = Connectionstring;
        }
        /// <summary>
        /// used to create a connetion to the database
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
