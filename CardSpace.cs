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
        public int spaceNumber;
        public int spaceStar;
        private Texture2D coin;

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
        public CardSpace(int i)
        {
            this.spaceNumber = i;
        }

        public void StarSetUp(List<CardSpace> refList)
        {
            switch (this.spaceNumber)
            {
                case 0:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber + 1].spaceStar += this.spaceStar;
                        refList[this.spaceNumber + 4].spaceStar += this.spaceStar;
                    }
                    break;
                case 1:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 1].spaceStar += this.spaceStar;
                        refList[this.spaceNumber + 1].spaceStar += this.spaceStar;
                        refList[this.spaceNumber + 4].spaceStar += this.spaceStar;
                    }
                    break;
                case 2:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 1].spaceStar += this.spaceStar;
                        refList[this.spaceNumber + 1].spaceStar += this.spaceStar;
                        refList[this.spaceNumber + 4].spaceStar += this.spaceStar;
                    }
                    break;
                case 3:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 1].spaceStar += this.spaceStar;
                        refList[this.spaceNumber + 4].spaceStar += this.spaceStar;
                    }
                    break;
                case 4:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 4].spaceStar += this.spaceStar;
                        refList[this.spaceNumber + 1].spaceStar += this.spaceStar;
                    }
                    break;
                case 5:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 4].spaceStar += this.spaceStar;
                        refList[this.spaceNumber - 1].spaceStar += this.spaceStar;
                        refList[this.spaceNumber + 1].spaceStar += this.spaceStar;
                    }
                    break;
                case 6:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 4].spaceStar += this.spaceStar;
                        refList[this.spaceNumber - 1].spaceStar += this.spaceStar;
                        refList[this.spaceNumber + 1].spaceStar += this.spaceStar;
                    }
                    break;
                case 7:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 4].spaceStar += this.spaceStar;
                        refList[this.spaceNumber - 1].spaceStar += this.spaceStar;
                    }
                    break;

            }
        }

        public void setCard(CardBase otherCard)
        {
            this.card = otherCard;
            this.card.position = this.position;
            this.card.spaceNumber = this.spaceNumber;
            this.spaceStar = this.card.Star;
        }

        public void DrawCoin(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.font, $"{spaceStar}", new Vector2(0, 0), Color.Black);
            spriteBatch.Draw(coin, new Vector2(this.position.X, this.position.Y), Color.White);
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
                this.position = new Vector2(525 +(150*(i-4)) , 750);
            }

            if (this.card !=null)
            {
                this.card.position = this.position;
            }
        }

        public void setEnemyPos(int i)
        {
            if (i <= 3)
            {
                this.position = new Vector2(525 + (150 * i), 55);
            }
            else if (i >= 4)
            {
                this.position = new Vector2(525 + (150 * (i - 4)), 280);
            }

            if (this.card != null)
            {
                this.card.position = this.position;
            }
        }

        public void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("CardSpace");
            this.coin = contentManager.Load<Texture2D>("Coin");
        }

    }
}
