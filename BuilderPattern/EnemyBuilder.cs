using PriateCardGame.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.BuilderPattern
{
    class EnemyBuilder : IBuilder
    {
        private List<CardBase> DeckList = new List<CardBase>();
        private Enemy enemy;
        public void BuildEnemyDeck(int diff)
        {
            DeckList.Clear();

            if (diff == 1)
            {
                for (int i = 0; i < 20; i++)
                {
                    DeckList.Add(new Swapper());
                }
                for (int i = 0; i < 5; i++)
                {
                    DeckList.Add(new Cannibal());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new FatPirate());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new Musketeer());
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
                for (int i = 0; i < 3; i++)
                {
                    DeckList.Add(new Cannibal());
                }
                for (int i = 0; i < 5; i++)
                {
                    DeckList.Add(new FatPirate());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new Musketeer());
                }
                for (int i = 0; i < 12; i++)
                {
                    DeckList.Add(new Musketeer());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new Thief());
                }
            }
            else if (diff == 3)
            {
                for (int i = 0; i < 5; i++)
                {
                    DeckList.Add(new Swapper());
                }
                for (int i = 0; i < 8; i++)
                {
                    DeckList.Add(new Cannibal());
                }
                for (int i = 0; i < 8; i++)
                {
                    DeckList.Add(new Musketeer());
                }
                for (int i = 0; i < 3; i++)
                {
                    DeckList.Add(new FatPirate());
                }
                for (int i = 0; i < 5; i++)
                {
                    DeckList.Add(new SniperParrot());
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
                for (int i = 0; i < 5; i++)
                {
                    DeckList.Add(new SniperParrot());
                }
                for (int i = 0; i < 2; i++)
                {
                    DeckList.Add(new Swapper());
                }
                for (int i = 0; i < 3; i++)
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
            
        }

        public void AddThisManyOfThisCard(int amount, CardBase card)
        {
            for (int i = 0; i < amount; i++)
            {
                DeckList.Add(card);
            }
        }

        public List<CardBase> GetResult()
        {
            return DeckList;
        }
    }
}
