using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    public class CardSpace
    {
        private CardBase card;
        public Vector2 position;

        public void setCar(CardBase otherCard)
        {
            this.card = otherCard;
            this.card.position = this.position;
        }

        public void DrawCard(SpriteBatch spritebatch)
        {
            card.Draw(spritebatch);
        }

    }
}
