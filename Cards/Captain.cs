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
        public Captain()
        {
            this.Damage = 4;
            this.Health = 12;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("PirateCaptain");
            this.color = Color.White;
        }
    }
}
