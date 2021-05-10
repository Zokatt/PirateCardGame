using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public static List<CardBase> playerCards;
        public static CardBase allCards;


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

            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            allCards.LoadContent(this.Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            allCards.Update(gameTime);

            

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

            allCards.Draw(this._spriteBatch);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
