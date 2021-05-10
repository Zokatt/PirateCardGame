﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace PriateCardGame.Database
{
    interface ICardMapper
    {
        List<CardBase> MapCardsFromReader(SQLiteDataReader reader);
    }
}