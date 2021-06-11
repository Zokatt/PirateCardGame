using System.Collections.Generic;
using System.Data.SQLite;

namespace PriateCardGame.DatabaseEnemyDiff
{
    /// <summary>
    /// The mapper for the difficulty database, basically just reads the 2nd coloumn and returns the integer
    /// <para>As there'so only on row</para>
    /// </summary>
    ///<remarks>
    /// Johnny
    /// </remarks>
    public class DifficultyMapper : IDifficultyMapper
    {
        public int MapDiffifultyFromReader(SQLiteDataReader reader)
        {
            //doesn't matter what the number is at it'll get overridden anyways
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