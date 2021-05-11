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
            GameWorld.enemyDeck;
            GameWorld.dropRepoTable();
            
            GameWorld.repo.Open();


            GameWorld.repo.AddCard("Captain");
            GameWorld.repo.AddCard("Captain");
            GameWorld.repo.AddCard("Captain");
            GameWorld.repo.AddCard("Captain");
            GameWorld.repo.AddCard("Captain");

            GameWorld.enemyDeck = GameWorld.enemyDeck.repo.FindDeck();

            GameWorld.repo.Close();
        }

        public List<GameObject> GetResult()
        {
            return enemyDeck;
        }
    }
}
