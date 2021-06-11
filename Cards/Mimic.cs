using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    /// <summary>
    /// The mimic card
    /// Will switch places with whatever card is in front, and then destroy itself
    /// </summary>
    ///<remarks>
    /// Nikolaj
    /// </remarks>
    class Mimic : CardBase
    {
        public Mimic()
        {
            this.Coin = 4;
            this.Name = "Mimic";
            this.Damage = 1;
            this.Health = 1;
            this.color = Color.White;
        }
        /// <summary>
        /// Switch places with whatever card is in front
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            /// <summary>
            /// checks whetever this card is owned by the enemy or the player
            /// below 500 is enemy
            /// </summary>
            if (this.position.Y<500)
            {
                /// <summary>
                /// checks whetever this card is in the backrow
                /// </summary>
                if (this.spaceNumber<=3)
                {
                    if (enemySpaces[this.spaceNumber+4].card==null)
                    {
                        if (playerSpaces[this.spaceNumber].card!=null)
                        {
                            enemySpaces[this.spaceNumber].SwitchCardEnemy(this.spaceNumber, this.spaceNumber, enemySpaces, playerSpaces);
                            this.Health = 0;
                        }
                        else if (playerSpaces[this.spaceNumber+4].card!=null)
                        {
                            enemySpaces[this.spaceNumber].SwitchCardEnemy(this.spaceNumber, this.spaceNumber+4, enemySpaces, playerSpaces);
                            this.Health = 0;
                        }
                    }
                }
                else
                {
                    if (playerSpaces[this.spaceNumber-4].card != null)
                    {
                        enemySpaces[this.spaceNumber].SwitchCardEnemy(this.spaceNumber, this.spaceNumber-4, enemySpaces, playerSpaces);
                        this.Health = 0;
                    }
                    else if (playerSpaces[this.spaceNumber].card != null)
                    {
                        enemySpaces[this.spaceNumber].SwitchCardEnemy(this.spaceNumber, this.spaceNumber, enemySpaces, playerSpaces);
                        this.Health = 0;
                    }
                }
                this.color = Color.White;
            }
            else
            {
                if (this.spaceNumber<=3)
                {
                    if (enemySpaces[this.spaceNumber+4].card !=null)
                    {
                        playerSpaces[this.spaceNumber].SwitchCardPlayer(this.spaceNumber, this.spaceNumber + 4, enemySpaces, playerSpaces);
                        this.Health = 0;
                    }
                    else if (enemySpaces[this.spaceNumber].card != null)
                    {
                        playerSpaces[this.spaceNumber].SwitchCardPlayer(this.spaceNumber, this.spaceNumber, enemySpaces, playerSpaces);
                        this.Health = 0;
                    }
                }
                else
                {
                    if (playerSpaces[this.spaceNumber-4].card == null)
                    {
                        if (enemySpaces[this.spaceNumber].card != null)
                        {
                            playerSpaces[this.spaceNumber].SwitchCardPlayer(this.spaceNumber, this.spaceNumber, enemySpaces, playerSpaces);
                            this.Health = 0;
                        }
                        else if (enemySpaces[this.spaceNumber-4].card != null)
                        {
                            playerSpaces[this.spaceNumber].SwitchCardPlayer(this.spaceNumber, this.spaceNumber-4, enemySpaces, playerSpaces);
                            this.Health = 0;
                        }
                    }
                }
                this.color = Color.White;
            }
            
        }
        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Mimic");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
