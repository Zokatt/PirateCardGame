﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PriateCardGame.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
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

        public void SetCardPos(int i)
        {
            if (i <= 3)
            {
                this.position = new Vector2(31+(200*i) , 37);
            }
            else if (i >= 4)
            {
                this.position = new Vector2(31 + (200 * i), 237);
            }

            if (this.card != null)
            {
                this.card.position = this.position;
            }
        }

        public void SetCard(int i)
        {
            var tmp = i + GameWorld.pageNumber;
            switch (i)
            {   
                case 0:
                    this.card = new Swapper();  
                    break;
                case 1:
                    this.card = new Captain();
                    break;
                case 2:
                    this.card = new Thief();
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.card != null)
            {
                this.card.Draw(spriteBatch);
                spriteBatch.DrawString(GameWorld.font, $"({storageOwned}) in storage)", new Vector2(this.Collision.Left, this.Collision.Top - 20), Color.Black);
                spriteBatch.DrawString(GameWorld.font, $"({deckOwned}) in deck", new Vector2(this.Collision.Left, this.Collision.Bottom + 5), Color.Black);
            }
            
        }

        public void SetCardCount(int storage,int deck)
        {
            this.storageOwned = storage;
            this.deckOwned = deck;
        }
    }
}