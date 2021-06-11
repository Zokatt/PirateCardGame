using System.Collections.Generic;
using System.Data.SQLite;

namespace PriateCardGame.DatabaseEnemyDiff
{
    /// <summary>
    /// The interface used to convert data from the database to an actual integer
    /// </summary>
    ///<remarks>
    /// Nikolaj,Johnny
    /// </remarks>
    public interface IDifficultyMapper
    {
        int MapDiffifultyFromReader(SQLiteDataReader reader);
    }
}