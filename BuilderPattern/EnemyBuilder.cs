using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.BuilderPattern
{
    class EnemyBuilder : IBuilder
    {
        
        private Enemy enemy;
        public void BuildEnemyDeck()
        {

            GameWorld.repo.Open();
            GameWorld.repo.DropTable();
            GameWorld.repo.Close();

            GameWorld.repo.Open();


            GameWorld.repo.AddCard("Captain");
            GameWorld.repo.AddCard("Captain");
            GameWorld.repo.AddCard("Captain");
            GameWorld.repo.AddCard("Captain");
            GameWorld.repo.AddCard("Captain");

            GameWorld.enemyDeck = GameWorld.repo.FindDeck();

            GameWorld.repo.Close();
        }

        public List<CardBase> GetResult()
        {
            return GameWorld.enemyDeck;
        }
    }
}
