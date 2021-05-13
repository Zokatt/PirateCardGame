using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class Thief : CardBase
    {
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.position.Y < 500)
            {
                ThiefAttackPlayer(enemySpaces, playerSpaces);


            }
            else if (this.position.Y > 500)
            {
                ThiefAttackEnemy(enemySpaces, playerSpaces);

            }

        }

        public Thief()
        {
            this.Name = "Thief";
            this.Damage = 1;
            this.Health = 6;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Thief");
            this.color = Color.White;
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }

        private void ThiefAttackEnemy(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            Random rnd = new Random();
            int lifeSteal = rnd.Next(1, 4);
            if (this.spaceNumber <= 3)
            {
                if (enemySpaces[this.spaceNumber + 4].card != null)
                {
                    enemySpaces[this.spaceNumber + 4].card.Health -= lifeSteal;
                    enemySpaces[this.spaceNumber + 4].card.damageTaken += lifeSteal;
                    playerSpaces[this.spaceNumber].card.Health += lifeSteal;
                }
                else if (enemySpaces[this.spaceNumber].card != null)
                {
                    enemySpaces[this.spaceNumber].card.Health -= lifeSteal;
                    enemySpaces[this.spaceNumber].card.damageTaken += lifeSteal;
                    playerSpaces[this.spaceNumber].card.Health += lifeSteal;
                }
                else
                {
                    //attack enemy
                }
            }
            else if (playerSpaces[this.spaceNumber - 4].card == null)
            {
                if (enemySpaces[this.spaceNumber].card != null)
                {
                    enemySpaces[this.spaceNumber].card.Health -= lifeSteal;
                    enemySpaces[this.spaceNumber].card.damageTaken += lifeSteal;
                    playerSpaces[this.spaceNumber].card.Health += lifeSteal;
                }
                else if (enemySpaces[this.spaceNumber - 4].card != null)
                {
                    enemySpaces[this.spaceNumber - 4].card.Health -= lifeSteal;
                    enemySpaces[this.spaceNumber - 4].card.damageTaken += lifeSteal;
                    playerSpaces[this.spaceNumber].card.Health += lifeSteal;
                }
                else
                {
                    //attack enemy
                }
            }
        }

        private void ThiefAttackPlayer(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            Random rnd = new Random();
            int lifeSteal = rnd.Next(1, 4);
            if (this.spaceNumber <= 3)
            {
                if (enemySpaces[this.spaceNumber + 4].card == null)
                {
                    if (playerSpaces[this.spaceNumber].card != null)
                    {
                        playerSpaces[this.spaceNumber].card.Health -= lifeSteal;
                        playerSpaces[this.spaceNumber].card.damageTaken += lifeSteal;
                        enemySpaces[this.spaceNumber].card.Health += lifeSteal;
                    }
                    else if (playerSpaces[this.spaceNumber + 4].card != null)
                    {
                        playerSpaces[this.spaceNumber + 4].card.Health -= lifeSteal;
                        playerSpaces[this.spaceNumber + 4].card.damageTaken += lifeSteal;
                        enemySpaces[this.spaceNumber].card.Health += lifeSteal;
                    }
                }
                else
                {
                    //attack enemy
                }
            }
            else
            {
                if (playerSpaces[this.spaceNumber - 4].card != null)
                {
                    playerSpaces[this.spaceNumber - 4].card.Health -= this.Damage;
                    playerSpaces[this.spaceNumber - 4].card.damageTaken += this.Damage;
                    enemySpaces[this.spaceNumber].card.Health += lifeSteal;
                }
                else if (playerSpaces[this.spaceNumber].card != null)
                {
                    playerSpaces[this.spaceNumber].card.Health -= this.Damage;
                    playerSpaces[this.spaceNumber].card.damageTaken += this.Damage;
                    enemySpaces[this.spaceNumber].card.Health += lifeSteal;
                }
                else
                {
                    //attack enemy
                }
            }
        }

    }
}
