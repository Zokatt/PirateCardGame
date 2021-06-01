using System.Collections.Generic;
using System.Data.SQLite;

namespace PriateCardGame.DatabaseEnemyDiff
{
    public class DifficultyMapper : IDifficultyMapper
    {
        public int MapDiffifultyFromReader(SQLiteDataReader reader)
        {
            var result = 69;
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var diff = reader.GetInt32(1);
                result = diff;

            }
            return result;
        }
    }
}