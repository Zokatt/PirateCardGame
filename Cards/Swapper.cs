using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class Swapper : CardBase
    {
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
        }

        public Swapper()
        {
            this.Star = 1;
            this.color = Color.White;
            this.Name = "Swapper";
            this.Damage = 1;
            this.Health = 2;
            this.Star = 1;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Swapper");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
