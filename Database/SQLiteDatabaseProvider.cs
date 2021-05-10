using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace PriateCardGame.Database
{
    class SQLiteDatabaseProvider
    {
        private readonly string connectionString;

        public SQLiteDatabaseProvider(string Connectionstring)
        {
            this.connectionString = Connectionstring;
        }

        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(connectionString);
        }
    }
}
