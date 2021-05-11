using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    public class CardBase : GameObject
    {
        public override Rectangle Collision
        {
            get
            {
                return new Rectangle(
                       (int)position.X,
                       (int)position.Y,
                       (int)sprite.Width/2,
                       (int)sprite.Height/2
                   );
            }
        }
        public string Name { get; set; }

        public int Damage { get; set; }

        public int Health { get; set; }

        public int CardID { get; set; }

        public string storageState { get; set; }

        public int spaceNumber { get; set; }


        //Validate method?

        public void CardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.spaceNumber <= 3)
            {
                if (enemySpaces[this.spaceNumber + 4].card != null)
                {
                    enemySpaces[this.spaceNumber + 4].card.Health -= this.Damage;
                }
                else if (enemySpaces[this.spaceNumber].card != null)
                {
                    enemySpaces[this.spaceNumber].card.Health -= this.Damage;
                }
                else
                {
                    //attack enemy
                }
            }
            else if (playerSpaces[this.spaceNumber - 4].card == null)
            {
                if (enemySpaces[this.spaceNumber].card != null)
                {
                    enemySpaces[this.spaceNumber].card.Health -= this.Damage;
                }
                else if (enemySpaces[this.spaceNumber - 4].card != null)
                {
                    enemySpaces[this.spaceNumber - 4].card.Health -= this.Damage;
                }
                else
                {
                    //attack enemy
                }
            }

            AdditionalCardEffect(enemySpaces,playerSpaces);
        }
        public override void LoadContent(ContentManager contentManager)
        {
            this.color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public void UpdateCardPos(int i)
        {
            this.position = new Vector2(500 + (i*140), 800);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, color, 0f,
            Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

            spriteBatch.DrawString(GameWorld.font, $"{this.Damage}", new Vector2(this.position.X+17, this.position.Y+160), Color.Black);
            spriteBatch.DrawString(GameWorld.font, $"{this.Health}", new Vector2(this.position.X+100, this.position.Y+160), Color.Goldenrod);
        }

        public virtual void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {

        }


    }
}
