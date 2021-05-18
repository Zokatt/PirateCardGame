using PriateCardGame.Cards;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace PriateCardGame.Database
{
    public class CardMapper : ICardMapper
    {
        public List<CardBase> MapCardsFromReader(SQLiteDataReader reader)
        {
            var result = new List<CardBase>();
            while (reader.Read())
            {
                var cardID = reader.GetInt32(0);
                var Name = reader.GetString(1);
                var storageState = reader.GetString(2);

                switch (Name)
                {
                    case "Captain":
                        result.Add(new Captain() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "Swapper":
                        result.Add(new Swapper() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "Cannon":
                        result.Add(new Cannon() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "Thief":
                        result.Add(new Thief() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "Cannibal":
                        result.Add(new Cannibal() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "Musketeer":
                        result.Add(new Musketeer() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "Whale":
                        result.Add(new Whale() { CardID = cardID, Name = Name, storageState = storageState });
                        break;

                }

            }
            return result;
        }
    }
}
