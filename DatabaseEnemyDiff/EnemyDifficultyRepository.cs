using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data;
using PriateCardGame.Database;

namespace PriateCardGame.DatabaseEnemyDiff
{
    public class EnemyDifficultyRepository
    {
        private readonly SQLiteDatabaseProvider provider;
        private readonly IDifficultyMapper mapper;
        private IDbConnection connection;

        public EnemyDifficultyRepository(SQLiteDatabaseProvider provider, IDifficultyMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        private void CreateDatabaseTables()
        {
            var cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Difficulty (diffID INTEGER PRIMARY KEY, Diff INTEGER);", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }


        public void UpdateUnlockedDifficulty(int diff)
        {
            var cmd = new SQLiteCommand($"UPDATE Difficulty set Diff = '{diff}' where diffID = 0", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void AddUnlockedDifficulty(int diff)
        {
            var cmd = new SQLiteCommand($"Insert into Difficulty (diffID, Diff) VALUES (0, '{diff}')", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public int FindDiff()
        {
            var cmd = new SQLiteCommand($"Select * from Difficulty where diffID = 0", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapDiffifultyFromReader(reader);
            return result;
        }

        public void Open()
        {
            if (connection == null)
            {
                connection = provider.CreateConnection();
            }
            connection.Open();

            CreateDatabaseTables();
        }

        public void Close()
        {
            connection.Close();
        }

        public void DropTable()
        {
            var cmd = new SQLiteCommand($"DROP TABLE Difficulty", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

    }
}
