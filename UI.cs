using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    public class UI : GameObject
    {
        public string spritePick;
        public UI(string whichUI,Vector2 pos)
        {
            this.spritePick = whichUI;
            this.color = Color.White;
            this.position = pos;
        }
        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>($"{spritePick}");
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
