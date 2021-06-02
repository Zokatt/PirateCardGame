using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    public abstract class CardBase : GameObject
    {
        public bool tookDamage;
        public int damageTaken;
        protected Texture2D DamageBox;

        public override Rectangle Collision
        {     get
            {
                if (sprite!=null)
                {
                    return new Rectangle(
                                           (int)position.X,
                                           (int)position.Y,
                                           (int)sprite.Width / 2,
                                           (int)sprite.Height / 2
                                       );
                }
                else
                {
                    return new Rectangle(
                       (int)position.X,
                       (int)position.Y,
                       (int)1,
                       (int)1
                   );
                }
                
            }
        }
        public string Name { get; set; }

        public int Damage { get; set; }

        public int Health { get; set; }

        public int CardID { get; set; }

        public string storageState { get; set; }

        public int spaceNumber { get; set; }

        public int Coin { get; set; }


        //Validate method?

        public void CardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.position.Y > 500)
            {
                attackEnemy(enemySpaces,playerSpaces);
            }
            else if (this.position.Y <500)
            {
                attackPlayer(enemySpaces,playerSpaces);
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

        public void UpdatePlayerCardPos(int i)
        {
            if (GameWorld.mousePos.Y < 800 || GameWorld.refCard !=null)
            {
                this.position = new Vector2(500 + (i * 140), 930);
            }
            else
            {
                this.position = new Vector2(500 + (i * 140), 800);
            }
        }

        public void SetDeckBuildingPosition(int i,int scrollValue)
        {
            this.position = new Vector2(50 + ((i-scrollValue) * 120),800 );
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(Collision.Width / 2f, Collision.Height / 2f);
            if (sprite!=null)
            {
                if (this.position.Y < 500 && GameWorld.gameState == GameState.CardBoard)
                {
                    spriteBatch.Draw(sprite, new Vector2(position.X + 90, position.Y + 145), null, color, (float)Math.PI,
                    origin, 0.5f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(GameWorld.font, $"{this.Damage}", new Vector2(this.position.X + 42, this.position.Y - 65), Color.Black,
                        (float)Math.PI, origin, 1, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(GameWorld.font, $"{this.Health}", new Vector2(this.position.X - 42, this.position.Y - 65), Color.Goldenrod,
                        (float)Math.PI, origin, 1, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(sprite, position, null, color, 0f,
                    Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                    spriteBatch.DrawString(GameWorld.font, $"{this.Damage}", new Vector2(this.position.X + 17, this.position.Y + 160), Color.Black);
                    spriteBatch.DrawString(GameWorld.font, $"{this.Health}", new Vector2(this.position.X + 100, this.position.Y + 160), Color.Goldenrod);
                }
                if (tookDamage == true)
                {
                    spriteBatch.Draw(DamageBox, new Vector2(this.position.X + 25, this.position.Y + 90), color);
                    spriteBatch.DrawString(GameWorld.font, $"-{damageTaken}", new Vector2(this.position.X + 40, this.position.Y + 100), Color.White);
                }
            }
            
            
        }

        public void Draw2(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, color, 0f,
                          Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(GameWorld.font, $"{this.Damage}", new Vector2(this.position.X + 25, this.position.Y + 245), Color.Black);
            spriteBatch.DrawString(GameWorld.font, $"{this.Health}", new Vector2(this.position.X + 150, this.position.Y + 245), Color.Goldenrod);

        }

        public abstract void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces);

        private void attackEnemy(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.spaceNumber <= 3)
            {
                if (enemySpaces[this.spaceNumber + 4].card != null)
                {
                    enemySpaces[this.spaceNumber + 4].card.Health -= this.Damage;
                    enemySpaces[this.spaceNumber + 4].card.damageTaken += this.Damage;
                }
                else if (enemySpaces[this.spaceNumber].card != null)
                {
                    enemySpaces[this.spaceNumber].card.Health -= this.Damage;
                    enemySpaces[this.spaceNumber].card.damageTaken += this.Damage;
                }
                else
                {
                    GameWorld.enemyHealth -= this.Damage;
                    GameWorld.healthDamage += this.Damage;
                    //attack enemy
                }
            }
            else if (playerSpaces[this.spaceNumber - 4].card == null)
            {
                if (enemySpaces[this.spaceNumber].card != null)
                {
                    enemySpaces[this.spaceNumber].card.Health -= this.Damage;
                    enemySpaces[this.spaceNumber].card.damageTaken += this.Damage;
                }
                else if (enemySpaces[this.spaceNumber - 4].card != null)
                {
                    enemySpaces[this.spaceNumber - 4].card.Health -= this.Damage;
                    enemySpaces[this.spaceNumber - 4].card.damageTaken += this.Damage;
                }
                else
                {
                    GameWorld.enemyHealth -= this.Damage;
                    GameWorld.healthDamage += this.Damage;
                    //attack enemy
                }
            }
        }

        private void attackPlayer(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.spaceNumber <= 3)
            {
                if (enemySpaces[this.spaceNumber+4].card == null)
                {
                    if (playerSpaces[this.spaceNumber].card != null)
                    {
                        playerSpaces[this.spaceNumber].card.Health -= this.Damage;
                        playerSpaces[this.spaceNumber].card.damageTaken+= this.Damage;
                    }
                    else if (playerSpaces[this.spaceNumber+4].card != null)
                    {
                        playerSpaces[this.spaceNumber+4].card.Health -= this.Damage;
                        playerSpaces[this.spaceNumber + 4].card.damageTaken += this.Damage;
                    }
                    else
                    {
                        GameWorld.playerHealth -= this.Damage;
                        GameWorld.healthDamage += this.Damage;
                        //attack player
                    }
                }
                
            }
            else
            {
                if (playerSpaces[this.spaceNumber-4].card != null)
                {
                    playerSpaces[this.spaceNumber-4].card.Health -= this.Damage;
                    playerSpaces[this.spaceNumber - 4].card.damageTaken += this.Damage;
                }
                else if (playerSpaces [this.spaceNumber].card != null)
                {
                    playerSpaces[this.spaceNumber].card.Health -= this.Damage;
                    playerSpaces[this.spaceNumber].card.damageTaken += this.Damage;
                }
                else
                {
                    GameWorld.playerHealth -= this.Damage;
                    GameWorld.healthDamage += this.Damage;
                    //attack player
                }
            }
        }

        public CardBase Clone()
        {
            return (CardBase)this.MemberwiseClone();
        }
    }
}
