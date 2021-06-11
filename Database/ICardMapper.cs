using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace PriateCardGame.Database
{/// <summary>
 /// The interface used to convert data from the database to actual cards
 /// </summary>
 ///<remarks>
 /// Nikolaj
 /// </remarks>
    public interface ICardMapper
    {
        List<CardBase> MapCardsFromReader(SQLiteDataReader reader);
    }
}
