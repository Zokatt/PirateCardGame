using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.BuilderPattern
{
    interface IBuilder
    {

        List<GameObject> GetResult();

        void BuildEnemyDeck();
    }
}
