using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class Whale : CardBase
    {
        public int whaleDmg = 6;
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
