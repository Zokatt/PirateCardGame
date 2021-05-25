using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class Gambler : CardBase
    {
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            //This card kills the card in front if it has odd health otherwise the card destroys itself. 
            if (this.position.Y > 500)
            {
                if (this.spaceNumber <= 3)
                {
                    if (enemySpaces[this.spaceNumber + 4].card != null)
                    {
                        int num = enemySpaces[this.spaceNumber + 4].card.Health;
                        if (num % 2 == 0)
                        {
                            enemySpaces[this.spaceNumber + 4].card.Health -= enemySpaces[this.spaceNumber + 4].card.Health;
                            enemySpaces[this.spaceNumber + 4].card.damageTaken += enemySpaces[this.spaceNumber + 4].card.damageTaken;
                        }
                        else
                        {
                            this.Health -= this.Health;
                        }


                    }
                    else if (enemySpaces[this.spaceNumber].card != null)
                    {
                        int num = enemySpaces[this.spaceNumber].card.Health;
                        if (num % 2 == 0)
                        {
                            enemySpaces[this.spaceNumber].card.Health -= enemySpaces[this.spaceNumber].card.Health;
                            enemySpaces[this.spaceNumber].card.damageTaken += enemySpaces[this.spaceNumber].card.Health;
                        }
                        else
                        {
                            this.Health -= this.Health;
                        }


                    }
                    else
                    {
                        GameWorld.enemyHealth -= this.Damage;
                        //attack enemy
                    }
                }
                else if (playerSpaces[this.spaceNumber - 4].card == null)
                {
                    if (enemySpaces[this.spaceNumber].card != null)
                    {
                        int num = enemySpaces[this.spaceNumber].card.Health;
                        if (num % 2 == 0)
                        {
                            enemySpaces[this.spaceNumber].card.Health -= enemySpaces[this.spaceNumber].card.Health;
                            enemySpaces[this.spaceNumber].card.damageTaken += enemySpaces[this.spaceNumber].card.Health;
                        }
                        else
                        {
                            this.Health -= this.Health;
                        }

                    }
                    else if (enemySpaces[this.spaceNumber - 4].card != null)
                    {
                        int num = enemySpaces[this.spaceNumber - 4].card.Health;
                        if (num % 2 == 0)
                        {
                            enemySpaces[this.spaceNumber - 4].card.Health -= enemySpaces[this.spaceNumber - 4].card.Health;
                            enemySpaces[this.spaceNumber - 4].card.damageTaken += enemySpaces[this.spaceNumber - 4].card.Health;
                        }
                        else
                        {
                            this.Health -= this.Health;
                        }

                    }
                    else
                    {
                        GameWorld.enemyHealth -= this.Damage;
                        //attack enemy
                    }
                }
            }
            if (this.position.Y < 500)
            {
                if (this.spaceNumber >= 4)
                {
                    if (playerSpaces[this.spaceNumber].card != null)
                    {
                        int num = playerSpaces[this.spaceNumber].card.Health;
                        if (num % 2 == 0)
                        {
                            playerSpaces[this.spaceNumber].card.Health -= playerSpaces[this.spaceNumber].card.Health;
                            playerSpaces[this.spaceNumber].card.damageTaken += playerSpaces[this.spaceNumber].card.damageTaken;
                        }
                        else
                        {
                            this.Health -= this.Health;
                        }


                    }
                    else if (playerSpaces[this.spaceNumber - 4].card != null)
                    {
                        int num = playerSpaces[this.spaceNumber - 4].card.Health;
                        if (num % 2 == 0)
                        {
                            playerSpaces[this.spaceNumber - 4].card.Health -= playerSpaces[this.spaceNumber - 4].card.Health;
                            playerSpaces[this.spaceNumber - 4].card.damageTaken += playerSpaces[this.spaceNumber - 4].card.Health;
                        }
                        else
                        {
                            this.Health -= this.Health;
                        }


                    }
                    else
                    {
                        GameWorld.playerHealth -= this.Damage;
                        //attack player
                    }
                }
                else if (enemySpaces[this.spaceNumber + 4].card == null) //forgot what - 4 is used for
                {
                    if (playerSpaces[this.spaceNumber + 4].card != null)
                    {
                        int num = playerSpaces[this.spaceNumber + 4].card.Health;
                        if (num % 2 == 0)
                        {
                            playerSpaces[this.spaceNumber + 4].card.Health -= playerSpaces[this.spaceNumber + 4].card.Health;
                            playerSpaces[this.spaceNumber + 4].card.damageTaken += playerSpaces[this.spaceNumber + 4].card.Health;
                        }
                        else
                        {
                            this.Health -= this.Health;
                        }

                    }
                    else if (enemySpaces[this.spaceNumber + 4].card != null) // Also this - 4
                    {
                        int num = playerSpaces[this.spaceNumber - 4].card.Health;
                        if (num % 2 == 0)
                        {
                            playerSpaces[this.spaceNumber - 4].card.Health -= playerSpaces[this.spaceNumber - 4].card.Health;
                            playerSpaces[this.spaceNumber - 4].card.damageTaken += playerSpaces[this.spaceNumber - 4].card.Health;
                        }
                        else
                        {
                            this.Health -= this.Health;
                        }

                    }
                    else
                    {
                        GameWorld.playerHealth -= this.Damage;
                        //attack player
                    }
                }

            }
        }

        public Gambler()
        {
            this.Name = "Gambler";
            this.color = Color.White;
            this.Damage = 1;
            this.Health = 50;

        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("SniperParrot"); // Needs a gambler sprite instead of this temporary sprite
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
