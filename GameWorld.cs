using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PriateCardGame.BuilderPattern;
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
        public static Rectangle screenBounds = new Rectangle(0, 0, 1600, 1000);
        public static string Deck;
        public static string Storage;
        public static CardRepository repo;
        public static List<CardBase> playerCards;
        public static List<CardSpace> playerSpaces;
        public static List<CardSpace> enemySpaces;
        public static List<CardBase> PlayerDeck;
        public static List<CardBase> enemyDeck;
        public static CardBase refCard;
        private Texture2D background;
        public static SpriteFont font;
        public static Point mousePos;
        public static MouseState mouseState;
        public static bool cardInfo = false;
        public static bool bPress = false;
        public static bool tPress = false;
        public static Director director = new Director(new EnemyBuilder());

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
            enemySpaces = new List<CardSpace>();
            PlayerDeck = new List<CardBase>();
            enemyDeck = new List<CardBase>();
            for (int i = 0; i < 8; i++)
            {
                playerSpaces.Add(new CardSpace(i));
            }

            for (int i = 0; i < 8; i++)
            {
                enemySpaces.Add(new CardSpace(i));
            }

            //for (int i = 0; i < 8; i++)
            //{
            //    enemySpaces[i].setCard(new Captain());
            //}
            //for (int i = 0; i < 8; i++)
            //{
            //    enemyDeck[i].
            //}

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

            background = Content.Load<Texture2D>("Background");
            font = Content.Load<SpriteFont>("Font");

            director.ConstructEnemyDeck();

            foreach (var item in playerSpaces)
            {
                item.LoadContent(this.Content);
                if (item.card != null)
                {
                    item.card.LoadContent(this.Content);
                }
            }
            foreach (var item in enemySpaces)
            {
                item.LoadContent(this.Content);
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

            Input(gameTime);


            mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);

            if (mouseState.LeftButton == ButtonState.Pressed) //to check for postion placements
            {

            }//breakpoint here to test <--

            if (cardInfo == true)
            {
                cardInfo = false;
            }

            ListUpdate(playerCards);
            for (int i = 0; i < playerSpaces.Count; i++)
            {
                playerSpaces[i].setPos(i);
                playerSpaces[i].CanPlace = false;
            }
            for (int i = 0; i < enemySpaces.Count; i++)
            {
                enemySpaces[i].setEnemyPos(i);
            }
            //for (int i = 0; i < enemyDeck.Count; i++)
            //{
            //    enemyDeck[i].setEnemyPos(i);
            //}

            // TODO: Add your update logic here


            if (refCard != null)
            {
                refCard.position.X = mousePos.X;
                refCard.position.Y = mousePos.Y;
            }

            foreach (CardSpace item in playerSpaces)
            {
                if (item.Collision.Contains(mousePos) && refCard != null && item.card == null)
                {
                    item.CanPlace = true;
                }
                if (item.Collision.Contains(mousePos) && mouseState.LeftButton == ButtonState.Pressed && bPress == false && refCard != null && item.card == null)
                {
                    item.setCard(refCard);
                    playerCards.Remove(refCard);
                    refCard = null;

                    bPress = true;
                }
            }

            if (mouseState.LeftButton == ButtonState.Released) //so that the player can click the mouse again
            {
                bPress = false;
            }

            base.Update(gameTime);
        }

        public void ListUpdate(List<CardBase> refList)
        {
            for (int i = 0; i < refList.Count; i++)
            {
                refList[i].UpdatePlayerCardPos(i);
            }
            for (int i = 0; i < refList.Count; i++)
            {
                if (refList[i].Collision.Contains(mousePos))
                {
                    cardInfo = true;
                }
                if (refList[i].Collision.Contains(mousePos) && mouseState.LeftButton == ButtonState.Pressed && bPress == false)
                {
                    refCard = refList[i];
                    //playerCards.RemoveAt(i);
                    bPress = true;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Vector2(-150, 0), Color.White);

            foreach (CardBase item in playerCards)
            {
                item.Draw(this._spriteBatch);

                if (cardInfo == true)
                {
                    for (int i = 0; i < playerCards.Count; i++)
                    {
                        _spriteBatch.Draw(item.sprite, new Vector2(1300, 400), null, Color.White, 0f,
                        Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        _spriteBatch.DrawString(GameWorld.font, $"{item.Damage}", new Vector2(1337, 730), Color.Black);
                        _spriteBatch.DrawString(GameWorld.font, $"{item.Health}", new Vector2(1505, 727), Color.Goldenrod);
                    }
                }
            }
            foreach (var item in playerSpaces)
            {
                item.DrawCanPlaceHere(this._spriteBatch);
                if (item.card != null)
                {
                    item.card.Draw(this._spriteBatch);
                }
            }
            foreach (var item in enemySpaces)
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
            while (playerCards.Count < 5)
            {
                int temp = rnd.Next(0, PlayerDeck.Count);

                playerCards.Add(PlayerDeck[temp]);
                PlayerDeck.RemoveAt(temp);
            }
        }

        public void Input(GameTime gametime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.T) && tPress == false )
            {
                tPress = true;
                foreach (CardSpace item in playerSpaces)
                {
                    if (item.card != null)
                    {
                        item.card.CardEffect(enemySpaces, playerSpaces);
                        if (item.card.Health <= 0)
                        {
                            item.card = null;
                        }
                    }
                }

                foreach (CardSpace item in enemySpaces)
                {
                    if (item.card != null)
                    {
                       item.card.CardEffect(enemySpaces, playerSpaces);
                        if (item.card.Health <= 0)
                        {
                            item.card = null;
                        }
                    }
                }
            }

            if (state.IsKeyUp(Keys.T))
            {
                tPress = false;
            }
        }
    }
}
