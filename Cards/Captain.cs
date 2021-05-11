using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class Captain : CardBase
    {
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            Random rnd = new Random();
            if (this.position.Y <500)
            {
                foreach (CardSpace item in playerSpaces)
                {
                    if (item.card!=null)
                    {
                        item.card.Health -= rnd.Next(0,5);
                    }
                }
            }
            else
            {
                foreach (CardSpace item in enemySpaces)
                {
                    if (item.card!=null)
                    {
                        item.card.Health -= rnd.Next(0, 5);
                    }
                }
            }
        }

        public Captain()
        {
            this.Damage = 4;
            this.Health = 12;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("PirateCaptain");
            this.color = Color.White;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, color, 0f,
            Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

            spriteBatch.DrawString(GameWorld.font, $"{this.Damage}", new Vector2(this.position.X + 17, this.position.Y + 160), Color.Black);
            spriteBatch.DrawString(GameWorld.font, $"{this.Health}", new Vector2(this.position.X + 100, this.position.Y + 160), Color.Goldenrod);
        }
    }
}
