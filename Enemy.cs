using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    /// <summary>
    /// Class for the enemy AI, has also contains the enemy deck
    /// </summary>
    ///<remarks>
    /// Nikolaj, Mads
    /// </remarks>
    public class Enemy : GameObject
    {
        /// <summary>
        /// The difficulty of the enemy
        /// </summary>
        public int difficulty;
        /// <summary>
        /// The enemy deck, build this deck in the gameworld
        /// </summary>
        public List<CardBase> Deck;
        /// <summary>
        /// The hand of the enemy, it will draw up a certain amount of cards each round 
        /// </summary>
        public List<CardBase> EnemyHand;

        /// <summary>
        /// Enemy AI, call this at the end of turn to make the enemy take it's turn
        /// </summary>
        public void AITurn(List<CardSpace>EnemySpaces)
        {
            //sorts the enemy hand so that it places the lower cost cards first
            //and then the high cost cards last
            SortByCoinAlgoByQuickSort(ref EnemyHand);


           
            Random rnd = new Random();
            for (int i = 0; i < EnemyHand.Count; i++)
            {
                var placed = false;
                while (placed == false)
                {
                    //should only be able to place if there's an empty spot
                    var Empty = 0;
                    foreach (var item in EnemySpaces)
                    {
                        //it wont make sense if it places the cannon on the backrow, so if the card it's trying to place is a cannon
                        //only check the front row for empty spots
                        if (item.spaceNumber >= 4
                            && item.card == null
                            && item.spaceCoin >= EnemyHand[i].Coin
                            && EnemyHand[i].Name == "Cannon")
                        {
                            Empty += 1;
                        }
                        if (item.card == null && item.spaceCoin >= EnemyHand[i].Coin && EnemyHand[i].Name != "Cannon")
                        {
                            //if there's an empty spot increase the integer
                            Empty += 1;
                        }
                    }
                    //if there's an empty spot on the board place a card
                    if (Empty >=1)
                    {
                        var temp = 0;
                        if (EnemyHand[i].Name == "Cannon")
                        {
                            temp = rnd.Next(4, EnemySpaces.Count);
                        }
                        else
                        {
                            //try to place the card on a random space
                            temp = rnd.Next(0, EnemySpaces.Count);
                        }
                        //only stop trying once it actually places a card
                        //there needs to be enough coins on the space to place the card
                        if (EnemySpaces[temp].card == null && EnemySpaces[temp].spaceCoin >= EnemyHand[i].Coin)
                        {
                            EnemySpaces[temp].setCard(EnemyHand[i]);
                            EnemyHand.RemoveAt(i);
                            i -= 1;
                            placed = true;
                        }
                        //update the coins for all the spaces since a new card has been placed
                        GameWorld.UpdateCoinsForLists();
                    }
                    else
                    {
                        placed = true;
                    }
                }  
            }
        }
        /// <summary>
        /// Used to sort the enemy hand by coin value so that it places the lower cost first
        /// <para>so it actually has a chance to place high cost cards</para>
        /// </summary>
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


        /// <summary>
        /// Draws cards for The AI. putting cards from the deck to it's hand
        /// </summary>
        public void DrawHand()
        {
            //if it's hand is full remove the 2 last card as they're the highest cost ones
            if (EnemyHand.Count >=5)
            {
                EnemyHand.RemoveAt(4);
                EnemyHand.RemoveAt(3);
            }
            Random rnd = new Random();
            if (GameWorld.turn <= 2)
            {
                //only draw 5 cards if it's the first turnn
                for (int i = 0; i < 5; i++)
                {
                    if (Deck.Count >= 1 && EnemyHand.Count < 5)
                    {
                        //draw a random card from the deck and adds it to the hand
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

        /// <summary>
        /// constructor for the enemy which takes an int in the overload
        /// </summary>
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
