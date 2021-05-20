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
                for (int i = 0; i < 10; i++)
                {
                    DeckList.Add(new Swapper());
                }
                for (int i = 0; i < 3; i++)
                {
                    DeckList.Add(new Cannibal());
                }
                for (int i = 0; i < 1; i++)
                {
                    DeckList.Add(new FatPirate());
                }
                for (int i = 0; i < 1; i++)
                {
                    DeckList.Add(new Musketeer());
                }
            }
            
        }

        public List<CardBase> GetResult()
        {
            return DeckList;
        }
    }
}
