using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    /// <summary>
    /// Cannibal Card
    /// Heals itself when it damages something
    /// </summary>
    /// <remarks>
    /// Nikolaj,Johnny
    /// </remarks>
    class Cannibal : CardBase
    {

        public Cannibal()
        {
            this.Coin = 2;
            this.color = Color.White;
            this.Name = "Cannibal";
            this.Damage = 2;
            this.Health = 3;
        }
        /// <summary>
        /// Calls the cannibal card effect
        /// Which is to heal itself for however much damage it has
        /// And only if there's a card in front
        /// and if that card is not a cannibal
        /// </summary>
        /// <remarks>
        /// Nikolaj,Johnny
        /// </remarks>
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            /// <summary>
            /// Used to deternime whetever this card is the enemys or the players
            /// below 500 on the y axis is enemy
            /// </summary>
            if (this.position.Y<500)
            {
                /// <summary>
                /// If this is in the backrow
                /// </summary>
                if (this.spaceNumber <= 3)
                {
                    /// <summary>
                    /// check if the card in front is empty
                    /// if it is, do effect
                    /// </summary>
                    if (enemySpaces[this.spaceNumber + 4].card == null)
                    {

                        if (playerSpaces[this.spaceNumber].card!=null)
                        {
                            if (playerSpaces[this.spaceNumber].card.Name != "Cannibal")
                            {
                                this.Health += this.Damage;
                            }
                        }
                        else if (playerSpaces[this.spaceNumber+4].card!=null)
                        {
                            if (playerSpaces[this.spaceNumber+4].card.Name != "Cannibal")
                            {
                                this.Health += this.Damage;
                            }
                        }
                    }
                }
                else if(this.spaceNumber >=4)
                {
                    if (playerSpaces[this.spaceNumber-4].card != null)
                    {
                        if (playerSpaces[this.spaceNumber-4].card.Name != "Cannibal")
                        {
                            this.Health += this.Damage;
                        }
                    }
                    else if (playerSpaces[this.spaceNumber].card != null)
                    {
                        if (playerSpaces[this.spaceNumber].card.Name != "Cannibal")
                        {
                            this.Health += this.Damage;
                        }
                    }
                }
            }
            else
            {
                if (this.spaceNumber >=4)
                {
                    if (playerSpaces[this.spaceNumber - 4].card == null)
                    {
                        if (enemySpaces[this.spaceNumber].card != null)
                        {
                            if (enemySpaces[this.spaceNumber].card.Name != "Cannibal")
                            {
                                this.Health += this.Damage;
                            }
                        }
                        else if (enemySpaces[this.spaceNumber-4].card != null)
                        {
                            if (enemySpaces[this.spaceNumber-4].card.Name != "Cannibal")
                            {
                                this.Health += this.Damage;
                            }
                        }
                    }
                }
                else if (this.spaceNumber <=3)
                {
                    if (enemySpaces[this.spaceNumber+4].card != null)
                    {
                        if (enemySpaces[this.spaceNumber+4].card.Name != "Cannibal")
                        {
                            this.Health += this.Damage;
                        }
                    }
                    else if (enemySpaces[this.spaceNumber].card != null)
                    {
                        if (enemySpaces[this.spaceNumber].card.Name != "Cannibal")
                        {
                            this.Health += this.Damage;
                        }
                    }
                }
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Cannibal");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
