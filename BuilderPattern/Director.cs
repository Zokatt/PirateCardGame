using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.BuilderPattern
{
    class Director
    {
        private IBuilder builder;

        public Director(IBuilder builder)
        {
            this.builder = builder;
        }

        public List<GameObject> ConstructEnemyDeck()
        {

            builder.BuildEnemyDeck();

            return builder.GetResult();
        }

    }
}
