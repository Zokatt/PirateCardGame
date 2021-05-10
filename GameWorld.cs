using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PriateCardGame.Cards;
using PriateCardGame.Database;
using System;
using System.Collections.Generic;

namespace PriateCardGame
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Rectangle screenBounds = new Rectangle(0, 0, 1920, 1000);
        public static string Deck;
        public static string Storage;
        public static CardRepository repo;
        public static List<CardBase> playerCards;
        public static List<CardSpace> playerSpaces;
        public static List<CardBase> PlayerDeck;
        public static CardBase refCard;


        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = screenBounds.Height;
            _graphics.PreferredBackBufferWidth = screenBounds.Width;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Nikolaj
            //JS
            //Mads
            //Det her er en ændring nikolaj har lavet

            playerCards = new List<CardBase>();
            playerSpaces = new List<CardSpace>();
            PlayerDeck = new List<CardBase>();
            for (int i = 0; i < 8; i++)
            {
                playerSpaces.Add(new CardSpace());
            }

            //for (int i = 0; i < 50; i++)
            //{
            //    PlayerDeck.Add(new Captain());
            //}

            var mapper = new CardMapper();
            var provider = new SQLiteDatabaseProvider("Data Source=Cards.db;Version=3;new=true");
            repo = new CardRepository(provider, mapper);

            dropRepoTable();

            repo.Open();
            

            repo.AddCard("Captain");
            repo.AddCard("Captain");
            repo.AddCard("Captain");
            repo.AddCard("Captain");
            repo.AddCard("Captain");
            repo.AddCard("Captain");
            repo.AddCard("Captain");
            repo.AddCard("Captain");
            repo.AddCard("Captain");
            repo.AddCard("Captain");

            PlayerDeck = repo.FindDeck();

            repo.Close();

            //Random rnd = new Random();

            //for (int i = 0; i < 5; i++)
            //{
            //    int temp = rnd.Next(0, PlayerDeck.Count);

            //    playerCards.Add(PlayerDeck[temp]);
            //    PlayerDeck.RemoveAt(temp);
            //}

            DrawHand();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (var item in playerSpaces)
            {
                if (item.card != null)
                {
                    item.card.LoadContent(this.Content);
                }
            }
            for (int i = 0; i < playerCards.Count; i++)
            {
                playerCards[i].LoadContent(this.Content);
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            ListUpdate(playerCards);
            for (int i = 0; i < playerSpaces.Count; i++)
            {
                playerSpaces[i].setPos(i);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void ListUpdate(List<CardBase> refList)
        {
            for (int i = 0; i < refList.Count; i++)
            {
                refList[i].UpdateCardPos(i);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            foreach (CardBase item in playerCards)
            {
                item.Draw(this._spriteBatch);
            }
            foreach (var item in playerSpaces)
            {
                if (item.card != null)
                {
                    item.card.Draw(this._spriteBatch);
                }
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void dropRepoTable()
        {
            repo.Open();
            repo.DropTable();
            repo.Close();
        }

        public void DrawHand()
        {
            Random rnd = new Random();
            while (playerCards.Count <5)
            {
                int temp = rnd.Next(0, PlayerDeck.Count);

                playerCards.Add(PlayerDeck[temp]);
                PlayerDeck.RemoveAt(temp);
            }
        }
    }
}
