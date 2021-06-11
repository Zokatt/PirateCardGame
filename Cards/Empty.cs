using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    /// <summary>
    /// Empty card is used for card check
    /// <para>this is has no health,damage and 0 coins</para>
    /// </summary>
    ///<remarks>
    /// Nikolaj
    /// </remarks>
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
