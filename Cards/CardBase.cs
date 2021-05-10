using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    public class CardBase : GameObject
    {
        public string Name { get; set; }

        public int Damage { get; set; }

        public int Health { get; set; }

        public int CardID { get; set; }

        public string storageState { get; set; }


        //Validate method?

        public void CardEffect()
        {

        }
        public override void LoadContent(ContentManager contentManager)
        {
            this.color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public void UpdateCardPos(int i)
        {
            this.position = new Vector2(600 + (i*140), 800);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, color);
        }

    }
}
