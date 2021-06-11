using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PriateCardGame.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    /// <summary>
    /// The CardSpace is used to contain a card 
    /// </summary>
    ///<remarks>
    /// Nikolaj
    /// </remarks>
    public class CardSpace
    {
        /// <summary>
        /// The card this is in this cardspace
        /// </summary>
        public CardBase card;
        /// <summary>
        /// where this cardspace is
        /// </summary>
        public Vector2 position;
        /// <summary>
        /// this sprite is for when the player hovers the mouse cursor of a free space where they can place a card
        /// </summary>
        private Texture2D sprite;
        /// <summary>
        /// The bool for wheteve or not the player can place a card on this space
        /// </summary>
        public bool CanPlace = false;
        /// <summary>
        /// For when the player can place a card but there's not enough coins on the space
        /// </summary>
        public bool canPlaceButNotEnoughCoins = false;
        /// <summary>
        /// For whatever number the cardspace is in the list, will deternime backrow and frontrow
        /// </summary>
        public int spaceNumber;
        /// <summary>
        /// However many coins are on a space
        /// </summary>
        public int spaceCoin;
        /// <summary>
        /// The texture for the coin
        /// </summary>
        private Texture2D coin;
        /// <summary>
        /// The collision to check whetever the player is hovering their cursor over the cardspace
        /// </summary>
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
        /// <summary>
        /// The constructor for a cardspace
        /// <para>Use this in combination with a for loop as the spacenumber needs to be set </para>
        /// </summary>
        public CardSpace(int i)
        {
            this.spaceNumber = i;
        }
        /// <summary>
        /// Method for checking whetever there's a card or not
        /// <para>if there's not a card on this space this returns an empty card</para>
        /// </summary>
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
        /// <summary>
        /// Used to setup coins for all spaces
        /// <para>use in combination with a foreach loop to go through all spaces</para>
        /// </summary>
        public void CoinSetUp(List<CardSpace> refList)
        {
            /// <summary>
            /// Switch case for all spacenumbers so the correct spaces will add up
            /// </summary>
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
        /// <summary>
        /// Method to set a card into a space
        /// <para>This sets things such a spacenumber and postion</para>'
        /// <para>Also Sets The coins on this space equal to the coin worth on the card</para>
        /// </summary>
        public void setCard(CardBase otherCard)
        {
            this.card = otherCard;
            this.card.position = this.position;
            this.card.spaceNumber = this.spaceNumber;
            this.spaceCoin = this.card.Coin;
        }
        /// <summary>
        /// Draws amount of coins
        /// </summary>
        public void DrawCoin(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.font, $"{spaceCoin}", new Vector2(this.position.X + 25, this.position.Y +5), Color.Black);
            spriteBatch.Draw(coin, new Vector2(this.position.X,this.position.Y), null, Color.White, 0f,
                         Vector2.Zero, 0.4f, SpriteEffects.None, 0f);
        }
        /// <summary>
        /// Used to draw the card
        /// </summary>
        public void DrawCard(SpriteBatch spritebatch)
        {
            card.Draw(spritebatch);
        }
        /// <summary>
        /// Whenever the player hovers over an empty space,if they can place a card here draw the sprite for it 
        /// </summary>
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
        /// <summary>
        /// Used in initalize to setup all the spaces and their postions 
        /// <para>use with a for loop</para>
        /// </summary>
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
        /// <summary>
        /// Used in initalize to setup all the spaces for the enemy and their postions 
        /// <para>use with a for loop</para> 
        /// </summary>
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
        /// <summary>
        /// This is to switch cards around for the mimic card
        /// <para>This one is if the mimic is owned by the player</para>
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void SwitchCardPlayer(int mimicSpaceNumber,int targetCardNumber, List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            /// <summary>
            /// makes a temporary with all the info of the mimic card
            /// </summary>
            var tmp = playerSpaces[mimicSpaceNumber].card;
            /// <summary>
            /// Sets the card on this space to whatever card is the target on the enemy space
            /// </summary>
            setCard(enemySpaces[targetCardNumber].card);
            /// <summary>
            /// Heals the card by one considering this method runs after normal attack phase
            /// so the mimic would've attack it once
            /// </summary>
            playerSpaces[mimicSpaceNumber].card.Health += 1;
            playerSpaces[mimicSpaceNumber].card.damageTaken = 0;
            /// <summary>
            /// Sets the enemy we switched with mimix we saved earlier
            /// </summary>
            enemySpaces[targetCardNumber].setCard(tmp);
        }
        /// <summary>
        /// This is to switch cards around for the mimic card
        /// <para>This one is if the mimic is owned by the enemy</para>
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void SwitchCardEnemy(int mimicSpaceNumber, int targetCardNumber, List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            var tmp = enemySpaces[mimicSpaceNumber].card;

            setCard(playerSpaces[targetCardNumber].card);
            enemySpaces[mimicSpaceNumber].card.Health += 1;
            enemySpaces[mimicSpaceNumber].card.damageTaken = 0;

            playerSpaces[targetCardNumber].setCard(tmp);
        }

    }
}
