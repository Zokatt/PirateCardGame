using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PriateCardGame.Cards;
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
        public bool canPlaceButNotEnoughCoins = false;
        public int spaceNumber;
        public int spaceCoin;
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

        public CardBase checkCard()
        {
            CardBase tmp;
            if (this.card == null)
            {
                tmp = new Empty();
            }
            else
            {
                tmp = this.card;
            }

            return tmp;
        }

        public void CoinSetUp(List<CardSpace> refList)
        {
            switch (this.spaceNumber)
            {
                case 0:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber + 1].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber + 4].spaceCoin += this.spaceCoin;
                    }
                    break;
                case 1:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 1].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber + 1].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber + 4].spaceCoin += this.spaceCoin;
                    }
                    break;
                case 2:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 1].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber + 1].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber + 4].spaceCoin += this.spaceCoin;
                    }
                    break;
                case 3:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 1].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber + 4].spaceCoin += this.spaceCoin;
                    }
                    break;
                case 4:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 4].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber + 1].spaceCoin += this.spaceCoin;
                    }
                    break;
                case 5:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 4].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber - 1].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber + 1].spaceCoin += this.spaceCoin;
                    }
                    break;
                case 6:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 4].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber - 1].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber + 1].spaceCoin += this.spaceCoin;
                    }
                    break;
                case 7:
                    if (refList[this.spaceNumber].card != null)
                    {
                        refList[this.spaceNumber - 4].spaceCoin += this.spaceCoin;
                        refList[this.spaceNumber - 1].spaceCoin += this.spaceCoin;
                    }
                    break;

            }
        }

        public void setCard(CardBase otherCard)
        {
            this.card = otherCard;
            this.card.position = this.position;
            this.card.spaceNumber = this.spaceNumber;
            this.spaceCoin = this.card.Coin;
        }

        public void DrawCoin(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.font, $"{spaceCoin}", new Vector2(this.position.X + 25, this.position.Y +5), Color.Black);
            spriteBatch.Draw(coin, new Vector2(this.position.X,this.position.Y), null, Color.White, 0f,
                         Vector2.Zero, 0.4f, SpriteEffects.None, 0f);
        }

        public void DrawCard(SpriteBatch spritebatch)
        {
            card.Draw(spritebatch);
        }

        public void DrawCanPlaceHere(SpriteBatch spriteBatch)
        {
            if (CanPlace == true && canPlaceButNotEnoughCoins ==true)
            {
                spriteBatch.Draw(sprite, position, Color.DarkGoldenrod);
            }
            else if (CanPlace == true)
            {
                spriteBatch.Draw(sprite, position, Color.Lime);
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
