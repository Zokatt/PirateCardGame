using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    /// <summary>
    /// the pirate captain card
    /// will hit every enemy card on the board for a random amount of damage between 0-3
    /// </summary>
    /// <remarks>
    /// Nikolaj
    /// </remarks>
    class Captain : CardBase
    {
        /// <summary>
        /// will do random amounts of damage between 0-3 to every card on the opponents
        /// </summary>
        /// <remarks>
        /// Nikolaj
        /// </remarks>
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            /// <summary>
            /// Generates a random number between 0 and 3
            /// </summary>
            Random rnd = new Random();
            var tmp = rnd.Next(0, 4);
            /// <summary>
            /// checks whetever this card is owned by the enemy or the player
            /// below 500 is enemy
            /// </summary>
            if (this.position.Y <500)
            {
                foreach (CardSpace item in playerSpaces)
                {
                    if (item.card!=null)
                    {
                        item.card.Health -= tmp;
                        item.card.damageTaken += tmp;
                    }
                }
            }
            else
            {
                foreach (CardSpace item in enemySpaces)
                {
                    if (item.card!=null)
                    {
                        item.card.Health -= tmp;
                        item.card.damageTaken += tmp;
                    }
                }
            }
        }

        public Captain()
        {
            this.Coin = 5;
            this.color = Color.White;
            this.Name = "Captain";
            this.Damage = 4;
            this.Health = 3;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("PirateCaptain");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }

    }
}
