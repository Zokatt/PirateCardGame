using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.BuilderPattern
{
    public interface IBuilder
    {

        List<CardBase> GetResult();

        void BuildEnemyDeck();
    }
}
