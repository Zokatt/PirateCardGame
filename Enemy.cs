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

        public List<CardBase> EnemyHand;

        public void AITurn(List<CardSpace>EnemySpaces)
        {
            SortByCoinAlgoByQuickSort(ref EnemyHand);

            Random rnd = new Random();
            for (int i = 0; i < EnemyHand.Count; i++)
            {
                var placed = false;
                while (placed == false)
                {
                    var Empty = 0;
                    foreach (var item in EnemySpaces)
                    {
                        if (item.card == null && item.spaceCoin >= EnemyHand[i].Coin)
                        {
                            Empty += 1;
                        }
                    }
                    if (Empty >=1)
                    {

                        var temp = rnd.Next(0, EnemySpaces.Count);
                        if (EnemySpaces[temp].card == null && EnemySpaces[temp].spaceCoin >= EnemyHand[i].Coin)
                        {
                            EnemySpaces[temp].setCard(EnemyHand[i]);
                            EnemyHand.RemoveAt(i);
                            i -= 1;
                            placed = true;
                        }

                        GameWorld.UpdateCoinsForLists();
                    }
                    else
                    {
                        placed = true;
                    }
                }  
            }
        }

        public List<CardBase> SortByCoinAlgoByQuickSort(ref List<CardBase> ListOfCards)
        {
            if (ListOfCards.Count <= 1)
            {
                return ListOfCards;
            }
            CardBase pivot = ListOfCards[0];


            List<CardBase> before = new List<CardBase>();
            List<CardBase> after = new List<CardBase>();

            for (int i = 1; i < ListOfCards.Count; i++)
            {
                if (ListOfCards[i].Coin < pivot.Coin)
                {
                    before.Add(ListOfCards[i]);
                }
                else
                {
                    after.Add(ListOfCards[i]);
                }
            }
            List<CardBase> result = new List<CardBase>();
            result.AddRange(SortByCoinAlgoByQuickSort(ref before));
            result.Add(pivot);
            result.AddRange(SortByCoinAlgoByQuickSort(ref after));
            ListOfCards = result;
            return ListOfCards;
        }



        public void DrawHand()
        {
            if (EnemyHand.Count >=10)
            {
                EnemyHand.RemoveAt(9);
                EnemyHand.RemoveAt(8);
            }
            Random rnd = new Random();
            if (GameWorld.turn <= 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (Deck.Count >= 1 && EnemyHand.Count < 10)
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
                    if (Deck.Count >= 1 && EnemyHand.Count < 10)
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
            EnemyHand = new List<CardBase>();
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
