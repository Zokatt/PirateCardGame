using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.BuilderPattern
{
    /// <summary>
    /// Interface for the enemy deck builder
    /// </summary>
    /// <remarks>
    /// Johnny
    /// </remarks>
    public interface IBuilder
    {

        List<CardBase> GetResult();

        void BuildEnemyDeck(int diff);
    }
}
