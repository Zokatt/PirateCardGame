using PriateCardGame.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.BuilderPattern
{
    /// <summary>
    /// The builder For the enemy deck
    /// Builds a deck for the enemy based on the difficulty chosen
    /// </summary>
    /// <remarks>
    /// Nikolaj,Johnny
    /// </remarks>
    class EnemyBuilder : IBuilder
    {
        private List<CardBase> DeckList = new List<CardBase>();
        /// <summary>
        /// Builds a deck for the enemy based on the difficulty chosen
        /// </summary>
        public void BuildEnemyDeck(int diff)
        {
            /// <summary>
            /// Clears the deck in case there's previously made deck
            /// </summary>
            DeckList.Clear();

            if (diff == 1)
            {
                for (int i = 0; i < 15; i++)
                {
                    DeckList.Add(new Swapper());
                }
                for (int i = 0; i < 5; i++)
                {
                    DeckList.Add(new Cannibal());
                }
                for (int i = 0; i < 1; i++)
                {
                    DeckList.Add(new FatPirate());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new Musketeer());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new BigSlime());
                }
                for (int i = 0; i < 1; i++)
                {
                    DeckList.Add(new Mimic());
                }



                //AddThisManyOfThisCard(20, new Swapper());
                //AddThisManyOfThisCard(5, new Cannibal());
                //AddThisManyOfThisCard(2, new FatPirate());
                //AddThisManyOfThisCard(2, new Musketeer());
            }
            else if (diff == 2)
            {
                for (int i = 0; i < 8; i++)
                {
                    DeckList.Add(new Swapper());
                }
                for (int i = 0; i < 8; i++)
                {
                    DeckList.Add(new SmallSlime());
                }
                for (int i = 0; i < 3; i++)
                {
                    DeckList.Add(new Cannibal());
                }
                for (int i = 0; i < 5; i++)
                {
                    DeckList.Add(new FatPirate());
                }
                for (int i = 0; i < 12; i++)
                {
                    DeckList.Add(new Musketeer());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new Thief());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new BigSlime());
                }
            }
            else if (diff == 3)
            {
                for (int i = 0; i < 5; i++)
                {
                    DeckList.Add(new Swapper());
                }
                for (int i = 0; i < 4; i++)
                {
                    DeckList.Add(new SmallSlime());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new Cannibal());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new Musketeer());
                }
                for (int i = 0; i < 4; i++)
                {
                    DeckList.Add(new FatPirate());
                }
                for (int i = 0; i < 5; i++)
                {
                    DeckList.Add(new SniperParrot());
                }
                for (int i = 0; i < 4; i++)
                {
                    DeckList.Add(new Cannon());
                }
                for (int i = 0; i < 1; i++)
                {
                    DeckList.Add(new Captain());
                }
            }
            else if (diff == 4)
            {
                for (int i = 0; i < 10; i++)
                {
                    DeckList.Add(new Thief());
                }
                for (int i = 0; i < 6; i++)
                {
                    DeckList.Add(new Musketeer());
                }
                for (int i = 0; i < 8; i++)
                {
                    DeckList.Add(new SniperParrot());
                }
                for (int i = 0; i < 8; i++)
                {
                    DeckList.Add(new SmallSlime());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new Swapper());
                }
                for (int i = 0; i < 4; i++)
                {
                    DeckList.Add(new FatPirate());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new Captain());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new DavyJonesLocker());
                }
            }
            else if (diff == 5)
            {
                for (int i = 0; i < 10; i++)
                {
                    DeckList.Add(new Thief());
                }
                for (int i = 0; i < 10; i++)
                {
                    DeckList.Add(new Mimic());
                }
                for (int i = 0; i < 15; i++)
                {
                    DeckList.Add(new SmallSlime());
                }

            }

        }

        /// <summary>
        /// Return the decklist for use to the enemy
        /// </summary>
        public List<CardBase> GetResult()
        {
            return DeckList;
        }
    }
}
