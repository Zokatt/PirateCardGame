using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using PriateCardGame.Database;

namespace PriateCardGame.DatabaseEnemyDiff
{
    /// <summary>
    /// The repository that manages the difficulty database
    /// </summary>
    ///<remarks>
    /// Nikolaj,Johnny
    /// </remarks>
    public class EnemyDifficultyRepository
    {
        /// <summary>
        /// Provider is used to determine which database file the tables go in
        /// </summary>
        private readonly SQLiteDatabaseProvider provider;
        /// <summary>
        /// The mapper is used to read from the database file and convert it to useable code for the game
        /// </summary>
        private readonly IDifficultyMapper mapper;
        /// <summary>
        /// Connection is used to make a connection to the database
        /// </summary>
        private IDbConnection connection;

        //The constructor for the Repository is made so that you can deside which database file you want
        //And which mapper or "converter" you want for that repository
        public EnemyDifficultyRepository(SQLiteDatabaseProvider provider, IDifficultyMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        /// <summary>
        /// This makes the table if it doesn't exist
        /// <para>With a diffID as primary key and Diff which is an integer</para>
        /// </summary>
        ///<remarks>
        /// Nikolaj,Johnny
        /// </remarks>
        private void CreateDatabaseTables()
        {
            var cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Difficulty (diffID INTEGER PRIMARY KEY, Diff INTEGER);", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates the unlocked difficulty
        /// <para>By updating the only row in the table</para>
        /// </summary>
        ///<remarks>
        /// Nikolaj,Johnny
        /// </remarks>
        public void UpdateUnlockedDifficulty(int diff)
        {
            var cmd = new SQLiteCommand($"UPDATE Difficulty set Diff = '{diff}' where diffID = 0", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Adds the only row to the table
        /// </summary>
        ///<remarks>
        /// Nikolaj,Johnny
        /// </remarks>
        public void AddUnlockedDifficulty(int diff)
        {
            var cmd = new SQLiteCommand($"Insert into Difficulty (diffID, Diff) VALUES (0, '{diff}')", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Used to find the unlocked difficulty
        /// </summary>
        ///<remarks>
        /// Nikolaj,Johnny
        /// </remarks>
        public int FindDiff()
        {
            var cmd = new SQLiteCommand($"Select * from Difficulty where diffID = 0", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapDiffifultyFromReader(reader);
            return result;
        }
        /// <summary>
        ///This is used to open a connection to the database
        /// </summary>
        public void Open()
        {
            if (connection == null)
            {
                connection = provider.CreateConnection();
            }
            connection.Open();

            CreateDatabaseTables();
        }
        /// <summary>
        ///This is used to close a connection to the database
        /// </summary>
        public void Close()
        {
            connection.Close();
        }
        /// <summary>
        ///This Drop the table effectively resetting player progress
        /// </summary>
        public void DropTable()
        {
            Open();
            var cmd = new SQLiteCommand($"DROP TABLE Difficulty", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
            Close();
        }

    }
}
