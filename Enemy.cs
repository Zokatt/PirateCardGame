﻿using Microsoft.Xna.Framework;
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

        public List<CardBase> EnemyHand;

        public void AITurn(List<CardSpace>EnemySpaces)
        {
            Random rnd = new Random();




            for (int i = 0; i < EnemyHand.Count; i++)
            {
                var temp = rnd.Next(0, EnemySpaces.Count);
                if (EnemySpaces[temp].card == null)
                {
                    EnemySpaces[temp].card = EnemyHand[i];
                    EnemyHand.RemoveAt(i);
                }



            }
        }

        public void DrawHand()
        {
            Random rnd = new Random();
            if (GameWorld.turn <= 1)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (Deck.Count >= 1 && EnemyHand.Count < 5)
                    {
                        int temp = rnd.Next(0, Deck.Count);

                        EnemyHand.Add(Deck[temp]);
                        Deck.RemoveAt(temp);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    if (Deck.Count >= 1 && EnemyHand.Count < 5)
                    {
                        int temp = rnd.Next(0, Deck.Count);

                        EnemyHand.Add(Deck[temp]);
                        Deck.RemoveAt(temp);
                    }
                }
            }
            //GameWorld.LoadContent();

        }

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
