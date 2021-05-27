using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class SniperParrot : CardBase
    {
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            
        }

        public SniperParrot()
        {
            this.Star = 1;
            this.Name = "SniperParrot";
            this.color = Color.White;
            this.Damage = 3;
            this.Health = 1;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("SniperParrot");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
