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
            var tmp = rnd.Next(0, 5);
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
            this.Damage = 4;
            this.Health = 12;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("PirateCaptain");
            this.color = Color.White;
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }

    }
}
