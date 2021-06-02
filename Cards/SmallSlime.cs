using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class SmallSlime : CardBase
    {
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            
        }

        public SmallSlime()
        {
            this.Coin = 1;
            this.Name = "SmallSlime";
            this.color = Color.White;
            this.Damage = 2;
            this.Health = 2;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("SmallSlime");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
