using PriateCardGame.Cards;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace PriateCardGame.Database
{
    public class CardMapper : ICardMapper
    {
        //This is used to read the rows from the database concerning cards
        public List<CardBase> MapCardsFromReader(SQLiteDataReader reader)
        {
            var result = new List<CardBase>();
            while (reader.Read())
            {
                //We get assign the different columns to a variable so that we can create a card out of those variables
                //The number in the paranthesis describes which coloumn it takes from
                var cardID = reader.GetInt32(0);
                var Name = reader.GetString(1);
                var storageState = reader.GetString(2);
                //The name is then used to determine which card to make with a switch statement
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
                    case "FatPirate":
                        result.Add(new FatPirate() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "DavyJonesLocker":
                        result.Add(new DavyJonesLocker() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "SniperParrot":
                        result.Add(new SniperParrot() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "Gambler":
                        result.Add(new Gambler() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "SmallSlime":
                        result.Add(new SmallSlime() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "BigSlime":
                        result.Add(new BigSlime() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                    case "Mimic":
                        result.Add(new Mimic() { CardID = cardID, Name = Name, storageState = storageState });
                        break;
                }

            }
            return result;
        }
    }
}
