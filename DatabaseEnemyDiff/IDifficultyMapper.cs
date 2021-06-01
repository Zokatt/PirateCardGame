using System.Collections.Generic;
using System.Data.SQLite;

namespace PriateCardGame.DatabaseEnemyDiff
{
    public interface IDifficultyMapper
    {
        int MapDiffifultyFromReader(SQLiteDataReader reader);
    }
}