using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.BuilderPattern
{
    public class Director
    {
        private IBuilder builder;

        public Director(IBuilder builder)
        {
            this.builder = builder;
        }

        public List<CardBase> ConstructEnemyDeck(int diff)
        {

            builder.BuildEnemyDeck(diff);

            return builder.GetResult();
        }

    }
}
