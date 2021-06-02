using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class BigSlime : CardBase
    {
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
        }

        public BigSlime()
        {
            this.Coin = 3;
            this.Name = "BigSlime";
            this.color = Color.White;
            this.Damage = 3;
            this.Health = 4;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("BigSlime");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
