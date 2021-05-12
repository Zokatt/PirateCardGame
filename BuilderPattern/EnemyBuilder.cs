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
                for (int i = 0; i < 8; i++)
                {
                    DeckList.Add(new Captain());
                }
            }
            
        }

        public List<CardBase> GetResult()
        {
            return DeckList;
        }
    }
}
