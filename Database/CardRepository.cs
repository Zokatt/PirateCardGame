using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace PriateCardGame.Database
{
    class CardRepository
    {
        private readonly SQLiteDatabaseProvider provider;
        private readonly ICardMapper mapper;
        private IDbConnection connection;

        public CardRepository(SQLiteDatabaseProvider provider, ICardMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }

        private void CreateDatabaseTables()
        {
            var cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Cards (cardID INTEGER PRIMARY KEY, Name STRING,StorageState STRING,);", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
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

        public List<CardBase> FindDeck()
        {
            var cmd = new SQLiteCommand($"Select * from Cards where StorageState = '{GameWorld.Deck}'",(SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapCardsFromReader(reader);
            return result;
        }

        public void AddCard(string cardName)
        {
            var cmd = new SQLiteCommand($"Insert into Cards (Name,StorageStage) VALUES('{cardName}', 'deck')",(SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void AddToDeck(string cardName)
        {
            //Update card set storageState = "deck"  where cardID IN(select cardID from card where storageState = "storage" LIMIT 1)

            var cmd = new SQLiteCommand($"Update Cards set StorageState = '{GameWorld.Deck}' where cardID IN(select cardID from cards were Name ={cardName} LIMIT 1)", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        public void DropTable()
        {
            var cmd = new SQLiteCommand($"DROP TABLE Cards", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }
        
        public void removeCard(int cardID)
        {
            
        }

    }
}
