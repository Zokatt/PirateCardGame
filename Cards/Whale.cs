using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    /// <summary>
    /// The whale card
    /// Will damage ALL card on the board, including ally cards
    /// </summary>
    ///<remarks>
    /// Johnny
    /// </remarks>
    class Whale : CardBase
    {
        /// <summary>
        /// the amount of damage the whale does to all cards
        /// </summary>
        public int whaleDmg = 6;
        /// <summary>
        /// Will damage ALL cards by 6
        /// </summary>
        ///<remarks>
        /// Johnny
        /// </remarks>
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            foreach (CardSpace item in playerSpaces)
            {
                if (item.card != null)
                {
                    item.card.Health -= whaleDmg;
                    item.card.damageTaken += whaleDmg;
                }
            }
            foreach (CardSpace item in enemySpaces)
            {
                if (item.card != null)
                {
                    item.card.Health -= whaleDmg;
                    item.card.damageTaken += whaleDmg;
                }
            }
        }

        public Whale()
        {
            this.Coin = 3;
            this.color = Color.White;
            this.Name = "Whale";
            this.Damage = 0;
            this.Health = 10;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Whale");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
