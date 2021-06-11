using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    /// <summary>
    /// the Cannon card
    /// Only Shoots if there's a card behind it
    /// </summary>
    /// <remarks>
    /// Nikolaj,Johnny
    /// </remarks>
    class Cannon : CardBase
    {
        /// <summary>
        /// Will shoot whatever is in front for 10 damage if there's a card behind 
        /// the cannon to "fire" the cannon
        /// </summary>
        /// <remarks>
        /// Nikolaj,Johnny
        /// </remarks>
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            /// <summary>
            /// Used to deternime whetever it's the enemys or the players card
            /// below 500 is enemy
            /// </summary>
            if (this.position.Y < 500)
            {
                if (this.spaceNumber >=4)
                {
                    if (enemySpaces[this.spaceNumber-4].card != null)
                    {
                        if (playerSpaces[this.spaceNumber-4].card !=null)
                        {
                            playerSpaces[this.spaceNumber - 4].card.Health -= 10;
                            playerSpaces[this.spaceNumber - 4].card.damageTaken += 10;
                        }
                        else if (playerSpaces[this.spaceNumber].card!=null)
                        {
                            playerSpaces[this.spaceNumber].card.Health -= 10;
                            playerSpaces[this.spaceNumber].card.damageTaken += 10;
                        }
                        else
                        {
                            GameWorld.playerHealth -= 10;
                            GameWorld.healthDamage += 10;
                        }
                    }
                }
            }
            else
            {
                if (this.spaceNumber<=3)
                {
                    if (playerSpaces[this.spaceNumber+4].card!=null)
                    {
                        if (enemySpaces[this.spaceNumber + 4].card != null)
                        {
                            enemySpaces[this.spaceNumber + 4].card.Health -= 10;
                            enemySpaces[this.spaceNumber + 4].card.damageTaken += 10;
                        }
                        else if (enemySpaces[this.spaceNumber].card != null)
                        {
                            enemySpaces[this.spaceNumber].card.Health -= 10;
                            enemySpaces[this.spaceNumber].card.damageTaken += 10;
                        }
                        else
                        {
                            GameWorld.enemyHealth -= 10;
                            GameWorld.healthDamage += 10;
                        }
                    }
                }
            }
        }

        public Cannon()
        {
            this.Coin = 3;
            this.color = Color.White;
            this.Name = "Cannon";
            this.Damage = 0;
            this.Health = 4;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Cannon");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }

        public void setPos(int i)
        {
            if (i <= 3)
            {
                this.position = new Vector2(525 + (150 * i), 525);
            }
            else if (i >= 4)
            {
                this.position = new Vector2(525 + (150 * (i - 4)), 750);
            }

        }

    }
}
