using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.BuilderPattern
{
    /// <summary>
    /// The director for the builder pattern
    /// Which makes the enemy deck
    /// </summary>
    /// <remarks>
    /// Johnny
    /// </remarks>
    public class Director
    {
        private IBuilder builder;

        public Director(IBuilder builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Construcs a deck for the enemy based on the current dificulty
        /// </summary>
        /// <remarks>
        /// Nikolaj,Johnny
        /// </remarks>
        public List<CardBase> ConstructEnemyDeck(int diff)
        {

            builder.BuildEnemyDeck(diff);

            return builder.GetResult();
        }

    }
}
