using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace PriateCardGame.Database
{
    /// <summary>
    /// The repository from where we manage and acces the Card Database
    /// </summary>
    ///<remarks>
    /// Nikolaj
    /// </remarks>
    public class CardRepository
    {
        /// <summary>
        /// Provider is used to determine which database file the tables go in
        /// </summary>
        private readonly SQLiteDatabaseProvider provider;
        /// <summary>
        /// The mapper is used to read from the database file and convert it to useable code for the game
        /// </summary>
        private readonly ICardMapper mapper;
        /// <summary>
        /// Connection is used to make a connection to the database
        /// </summary>
        private IDbConnection connection;
        //The constructor for the Repository is made so that you can deside which database file you want
        //And which mapper or "converter" you want for that repository
        public CardRepository(SQLiteDatabaseProvider provider, ICardMapper mapper)
        {
            this.provider = provider;
            this.mapper = mapper;
        }
        /// <summary>
        /// This makes the table if it doesn't exist
        /// <para>With a cardID as primary key, "Name" which is a string which is also used to determine which card it is</para>
        /// <para>And storageState which is used to determine whetever it's in the deck or stage</para>
        /// </summary>
        private void CreateDatabaseTables()
        {
            var cmd = new SQLiteCommand($"CREATE TABLE IF NOT EXISTS Cards (cardID INTEGER PRIMARY KEY, Name STRING,StorageState STRING);", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        ///This is used to open a connection to the database
        /// </summary>
        public void Open()
        {
            if (connection == null)
            {
                connection = provider.CreateConnection();
            }
            connection.Open();
            
            CreateDatabaseTables();
        }
        /// <summary>
        ///This is used to close a connection to the database
        /// </summary>
        public void Close()
        {
            connection.Close();
        }
        /// <summary>
        ///This finds any card in the repository that has the storageState "deck" to find the players deck
        /// </summary>
        public List<CardBase> FindDeck()
        {
            var cmd = new SQLiteCommand($"Select * from Cards where StorageState = '{GameWorld.Deck}'",(SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapCardsFromReader(reader);
            return result;
        }
        /// <summary>
        ///This finds all cards in the repository, both in storage and deck
        /// </summary>
        public List<CardBase> FindAllCards()
        {
            var cmd = new SQLiteCommand($"Select * from Cards", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();

            var result = mapper.MapCardsFromReader(reader);
            return result;
        }
        /// <summary>
        ///This will insert a card into the database with the storageState deck
        ///<para>said another way:This will add a card to a players deck</para>
        /// </summary>
        public void AddCard(string cardName)
        {
            var cmd = new SQLiteCommand($"Insert into Cards (Name,StorageState) VALUES('{cardName}', '{GameWorld.Deck}')",(SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        ///This will add a card to the storage, so the player can use it for later
        /// </summary>
        public void AddCardToStorage(string cardName)
        {
            var cmd = new SQLiteCommand($"Insert into Cards (Name,StorageState) VALUES('{cardName}', '{GameWorld.Storage}')", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        ///This will change a card from being in the storage to the deck
        ///<para>It checks the database with the cards name and updates the first card found to the deck</para>
        /// </summary>
        public void AddToDeck(string cardName)
        {
            //Update card set storageState = "deck"  where cardID IN(select cardID from card where storageState = "storage" LIMIT 1)

            var cmd = new SQLiteCommand($"Update Cards set StorageState = '{GameWorld.Deck}' where cardID IN(select cardID from cards where Name ='{cardName}' and StorageState = '{GameWorld.Storage}' LIMIT 1)", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        ///This will remove all cards from the table
        /// </summary>
        public void DropTable()
        {
            var cmd = new SQLiteCommand($"DROP TABLE Cards", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        ///This will remove a card from the deck and place it in the storage
        /// </summary>
        public void removeCard(string cardName)
        {
            var cmd = new SQLiteCommand($"Update Cards set StorageState = '{GameWorld.Storage}' where cardID IN(select cardID from cards where Name ='{cardName}' and StorageState = '{GameWorld.Deck}' LIMIT 1)", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        ///This will find all cards in the table with the same name
        ///<para>Used to show a number to the user of how many of that card they own in the storage</para>
        /// </summary>
        public List<CardBase> FindAllCardsInStorageWithThisName(string name)
        {
            var cmd = new SQLiteCommand($"Select * from Cards where Name = '{name}' and StorageState = '{GameWorld.Storage}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();
            var result = mapper.MapCardsFromReader(reader);
            return result;
        }
        /// <summary>
        ///This will find all cards in the table with the same name
        ///<para>Used to show a number to the user of how many of that card they own in the deck</para>
        /// </summary>
        public List<CardBase> FindAllCardsInDeckWithThisName(string name)
        {
            var cmd = new SQLiteCommand($"Select * from Cards where Name = '{name}' and StorageState = '{GameWorld.Deck}'", (SQLiteConnection)connection);
            var reader = cmd.ExecuteReader();
            var result = mapper.MapCardsFromReader(reader);
            return result;
        }
        /// <summary>
        /// Will put all cards from deck into storage
        /// </summary>
        public void ClearDeck()
        {
            var cmd = new SQLiteCommand($"Update Cards set StorageState = '{GameWorld.Storage}' where StorageState = '{GameWorld.Deck}'", (SQLiteConnection)connection);
            cmd.ExecuteNonQuery();
        }

    }
}
