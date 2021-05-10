using Microsoft.Xna.Framework;
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

        public void setPos(int i)
        {
            if (i <= 3)
            {
                this.position = new Vector2(400 + (300 * i), 400);
            }
            else if (i >=4)
            {
                this.position = new Vector2(400 +(300*(i-4)) , 700);
            }

            if (this.card !=null)
            {
                this.card.position = this.position;
            }
        }

    }
}
