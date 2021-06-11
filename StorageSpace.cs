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
    /// A space for the Deckbuilding screen where the cards will be shown
    /// </summary>
    ///<remarks>
    /// Nikolaj, Johnny
    /// </remarks>
    public class StorageSpace
    {
        public CardBase card;
        public Vector2 position;
        private int storageOwned;
        private int deckOwned;

        public virtual Rectangle Collision
        {
            get
            {
                return new Rectangle(
                       (int)position.X,
                       (int)position.Y,
                       (int)card.sprite.Width/2,
                       (int)card.sprite.Height/2
                   );
            }
        }

        public StorageSpace()
        {
        
        }

        public void Update(GameTime gameTime)
        {

        }

        public void LoadContent(ContentManager content)
        {
            if (this.card != null)
            {
                this.card.LoadContent(content);
            }
        }
        /// <summary>
        /// Sets the postion of whatever card is in this space 
        /// <para>use in combination with a for loop</para>
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        public void SetCardPos(int i)
        {
            if (i <= 4)
            {
                this.position = new Vector2(31+(200*i) , 37);
            }
            else if (i >= 5)
            {
                this.position = new Vector2(31 + (200 * (i-5)), 320);
            }

            if (this.card != null)
            {
                this.card.position = this.position;
            }
        }
        /// <summary>
        /// Sets the card for this storage space
        /// <para>use method in a for loop to set all cards in deckbuilding</para>
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void SetCard(int i)
        {
            var tmp = i + (GameWorld.pageNumber*10);
            switch (tmp)
            {   
                case 0:
                    this.card = new Swapper();  
                    break;
                case 1:
                    this.card = new Captain();
                    break;
                case 2:
                    this.card = new Cannibal();
                    break;
                case 3:
                    this.card = new Thief();
                    break;
                case 4:
                    this.card = new Cannon();
                    break;
                case 5:
                    this.card = new Musketeer();
                    break;
                case 6:
                    this.card = new Whale();
                    break;
                case 7:
                    this.card = new FatPirate();
                    break;
                case 8:
                    this.card = new DavyJonesLocker();
                    break;
                case 9:
                    this.card = new SniperParrot();
                    break;
                case 10:
                    this.card = new SmallSlime();
                    break;
                case 11:
                    this.card = new BigSlime();
                    break;
                case 12:
                    this.card = new Mimic();
                    break;
                case 13:
                    this.card = new Gambler();
                    break;
                case 14:
                    this.card = new Cannon();
                    break;
                case 15:
                    this.card = new Gambler();
                    break;
                case 16:
                    this.card = new Cannon();
                    break;
                case 17:
                    this.card = new Gambler();
                    break;
                case 18:
                    this.card = new Cannon();
                    break;
                case 19:
                    this.card = new Gambler();
                    break;
                case 20:
                    this.card = new Cannon();
                    break;
                case 21:
                    this.card = new Gambler();
                    break;
                case 22:
                    this.card = new Cannon();
                    break;
                case 23:
                    this.card = new Gambler();
                    break;

            }
        }
        /// <summary>
        /// Draws the card and how many of that card the player owns, both in storage and deck
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.card != null)
            {
                this.card.Draw(spriteBatch);
                spriteBatch.DrawString(GameWorld.font, $"({storageOwned}) in storage)", new Vector2(this.Collision.Left, this.Collision.Top - 20), Color.White);
                spriteBatch.DrawString(GameWorld.font, $"({deckOwned}) in deck", new Vector2(this.Collision.Left, this.Collision.Bottom + 5), Color.White);
            }
            
        }
        /// <summary>
        /// Used to set how many of that card the player owns
        /// </summary>
        ///<remarks>
        /// Nikolaj, Johnny
        /// </remarks>
        public void SetCardCount(int storage,int deck)
        {
            this.storageOwned = storage;
            this.deckOwned = deck;
        }
    }
}
