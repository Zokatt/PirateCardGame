using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    public class Enemy : GameObject
    {
        public int difficulty;
        public List<CardBase> Deck;
        public Enemy(int diff)
        {
            Deck = new List<CardBase>();
            this.difficulty = diff;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
