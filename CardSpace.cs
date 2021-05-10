using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    public class CardSpace
    {
        public CardBase card;
        public Vector2 position;
        private Texture2D sprite;
        public bool CanPlace = false;

        public virtual Rectangle Collision
        {
            get
            {
                return new Rectangle(
                       (int)position.X,
                       (int)position.Y,
                       (int)sprite.Width,
                       (int)sprite.Height
                   );
            }
        }
        public CardSpace()
        {

        }
        public void setCard(CardBase otherCard)
        {
            this.card = otherCard;
            this.card.position = this.position;
        }


        public void DrawCard(SpriteBatch spritebatch)
        {
            card.Draw(spritebatch);
        }

        public void DrawCanPlaceHere(SpriteBatch spriteBatch)
        {
            if (CanPlace == true)
            {
                spriteBatch.Draw(sprite, position, Color.White);
            }
        }
        public void setPos(int i)
        {
            if (i <= 3)
            {
                this.position = new Vector2(525 + (150 * i), 525);
            }
            else if (i >=4)
            {
                this.position = new Vector2(525 +(150*(i-4)) , 725);
            }

            if (this.card !=null)
            {
                this.card.position = this.position;
            }
        }

        public void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("CardSpace");
        }

    }
}
