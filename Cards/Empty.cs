using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class Empty : CardBase
    {

        public Empty()
        {
            this.Coin = 0;
        }
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            throw new NotImplementedException();
        }
    }
}
