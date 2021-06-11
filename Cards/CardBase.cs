using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    /// <summary>
    /// The cardbase that all cards are based on
    /// </summary>
    /// <remarks>
    /// Nikolaj, Johnny helped setting up draw
    /// </remarks>
    public abstract class CardBase : GameObject
    {
        /// <summary>
        /// used to check whetever this card has taken damage so that a circle with the amount can be shown
        /// </summary>
        public bool tookDamage;
        /// <summary>
        /// The amount of damage this card has taken, will be shown in a red circle
        /// </summary>
        public int damageTaken;
        /// <summary>
        /// red circle used to show how much damage has been taken
        /// </summary>
        protected Texture2D DamageBox;
        /// <summary>
        /// Collision which is used to check whetever the player is holding the mosuer over it or not
        /// </summary>
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
        /// <summary>
        /// How much damage the card has
        /// </summary>
        public int Damage { get; set; }
        /// <summary>
        /// How much Health the card has
        /// </summary>
        public int Health { get; set; }

        public int CardID { get; set; }
        /// <summary>
        /// whetever it's in the deck or storage
        /// </summary>
        public string storageState { get; set; }
        /// <summary>
        /// which space it's in
        /// </summary>
        public int spaceNumber { get; set; }
        /// <summary>
        /// How many coins it's worth
        /// </summary>
        public int Coin { get; set; }


        //Validate method?

        /// <summary>
        /// The cardeffect for all cards.
        /// will attack either a players card/health or enemy card/health 
        /// depending on it's position, below 500 is belonging to enemy
        /// Then at the end it runs AddionalCardEffect
        /// </summary>
        /// <remarks>
        /// Nikolaj
        /// </remarks>
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
            /// <summary>
            /// The AddionalCardEffect changes depending on which card is being activated
            /// </summary>
            AdditionalCardEffect(enemySpaces,playerSpaces);
        }
        public override void LoadContent(ContentManager contentManager)
        {
            this.color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            
        }
        /// <summary>
        /// For showing the players hand
        /// <para>Run this through a for loop </para>
        /// </summary>
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
        /// <summary>
        /// For showing the players deck in deckbuilding
        /// <para>Run this through a for loop </para>
        /// </summary>
        public void SetDeckBuildingPosition(int i,int scrollValue)
        {
            this.position = new Vector2(50 + ((i-scrollValue) * 120),800 );
        }

        /// <summary>
        /// The draw for all cards
        /// <para>Also draws Health and Damage </para>
        /// <para>And will rotate the card if it belongs to the enemy </para>
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public override void Draw(SpriteBatch spriteBatch)
        {
            /// <summary>
            /// For scaling down the card by half since the card is pretty big
            /// </summary>
            Vector2 origin = new Vector2(Collision.Width / 2f, Collision.Height / 2f);
            if (sprite!=null)
            {
                if (this.position.Y < 500 && GameWorld.gameState == GameState.CardBoard)
                {
                    /// <summary>
                    /// Draws the card
                    /// </summary>
                    spriteBatch.Draw(sprite, new Vector2(position.X + 90, position.Y + 145), null, color, (float)Math.PI,
                    origin, 0.5f, SpriteEffects.None, 0f);
                    /// <summary>
                    /// Draws the damage
                    /// </summary>
                    spriteBatch.DrawString(GameWorld.font, $"{this.Damage}", new Vector2(this.position.X + 42, this.position.Y - 65), Color.Black,
                        (float)Math.PI, origin, 1, SpriteEffects.None, 0f);
                    /// <summary>
                    /// Draws the Health
                    /// </summary>
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
                /// <summary>
                /// Draws the red circle which shows how much damage the card takes
                /// </summary>
                if (tookDamage == true)
                {
                    spriteBatch.Draw(DamageBox, new Vector2(this.position.X + 25, this.position.Y + 90), color);
                    spriteBatch.DrawString(GameWorld.font, $"-{damageTaken}", new Vector2(this.position.X + 40, this.position.Y + 100), Color.White);
                }
            }
            
            
        }
        /// <summary>
        /// A draw specifically for the Reward card
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void Draw2(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, color, 0f,
                          Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(GameWorld.font, $"{this.Damage}", new Vector2(this.position.X + 25, this.position.Y + 245), Color.Black);
            spriteBatch.DrawString(GameWorld.font, $"{this.Health}", new Vector2(this.position.X + 150, this.position.Y + 245), Color.Goldenrod);

        }
        /// <summary>
        /// Addtional card effect which will be called at the end of card effect
        /// <para>Abstract so that it can be overridden</para>
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public abstract void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces);
        /// <summary>
        /// Will Attack the enemy
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        private void attackEnemy(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            /// <summary>
            /// If this is not the backrow
            /// </summary>
            if (this.spaceNumber <= 3)
            {
                /// <summary>
                /// If there is a card on the front row of the enemy
                /// </summary>
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
            /// <summary>
            /// If this is in the backrow and there's no card in front
            /// </summary>
            else if (playerSpaces[this.spaceNumber - 4].card == null)
            {
                /// <summary>
                /// start atttacking the enemy card / health
                /// </summary>
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
        /// <summary>
        /// Will Attack the player
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        private void attackPlayer(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            /// <summary>
            /// If this is in the backrow and there's no card in front
            /// </summary>
            if (this.spaceNumber <= 3)
            {
                if (enemySpaces[this.spaceNumber+4].card == null)
                {
                    /// <summary>
                    /// start atttacking the players card / health
                    /// </summary>
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
        /// <summary>
        /// Used to clone a card by the thief to counter an event problem
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public CardBase Clone()
        {
            return (CardBase)this.MemberwiseClone();
        }
    }
}
