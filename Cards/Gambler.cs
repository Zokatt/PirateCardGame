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
            //This card kills the card in front if it has odd health otherwise the card destroys itself 

            //Checks for the player cards which is above 500 in the y position
            if (this.position.Y > 500)
            {
                //Checks the front row of the player's cards
                if (this.spaceNumber <= 3)
                {
                    //Checks if there is a card in the front row of the enemy's board
                    if (enemySpaces[this.spaceNumber + 4].card != null)
                    {
                        //Checks if the health of the enemy it attacks is an odd number
                        int num = enemySpaces[this.spaceNumber + 4].card.Health;
                        if (num % 2 == 1)
                        {
                            enemySpaces[this.spaceNumber + 4].card.Health -= enemySpaces[this.spaceNumber + 4].card.Health;
                            enemySpaces[this.spaceNumber + 4].card.damageTaken += enemySpaces[this.spaceNumber + 4].card.damageTaken;
                        }
                        else
                        {
                            //Otherwise it destroys itself
                            this.Health -= this.Health;
                        }


                    }
                    else if (enemySpaces[this.spaceNumber].card != null)
                    {
                        //Checks if the health of the enemy it attacks is an odd number
                        int num = enemySpaces[this.spaceNumber].card.Health;
                        if (num % 2 == 1)
                        {
                            //Kills the enemy card it attacks if it is an odd number
                            enemySpaces[this.spaceNumber].card.Health -= enemySpaces[this.spaceNumber].card.Health;
                            enemySpaces[this.spaceNumber].card.damageTaken += enemySpaces[this.spaceNumber].card.Health;
                        }
                        else
                        {
                            //It destroys itself
                            this.Health -= this.Health;
                        }


                    }
                    else
                    {
                        GameWorld.enemyHealth -= this.Damage;
                        //Attack enemy
                    }
                }
                //Checks if the players front row has no cards if a card in the players back row is placed
                else if (playerSpaces[this.spaceNumber - 4].card == null)
                {
                    //Checks the enemy's front row has a card
                    if (enemySpaces[this.spaceNumber].card != null)
                    {
                        //Checks if the health of the enemy it attacks is an odd number
                        int num = enemySpaces[this.spaceNumber].card.Health;
                        if (num % 2 == 1)
                        {
                            //Kills the enemy card it attacks if it is an odd number
                            enemySpaces[this.spaceNumber].card.Health -= enemySpaces[this.spaceNumber].card.Health;
                            enemySpaces[this.spaceNumber].card.damageTaken += enemySpaces[this.spaceNumber].card.Health;
                        }
                        else
                        {
                            //The card destroys itself
                            this.Health -= this.Health;
                        }

                    }
                    else if (enemySpaces[this.spaceNumber - 4].card != null)
                    {
                        //Checks if the health of the enemy it attacks in the back row is an odd number
                        int num = enemySpaces[this.spaceNumber - 4].card.Health;
                        if (num % 2 == 1)
                        {
                            //Kills the enemy card it attacks if it is an odd number
                            enemySpaces[this.spaceNumber - 4].card.Health -= enemySpaces[this.spaceNumber - 4].card.Health;
                            enemySpaces[this.spaceNumber - 4].card.damageTaken += enemySpaces[this.spaceNumber - 4].card.Health;
                        }
                        else
                        {
                            //The card destroys itself
                            this.Health -= this.Health;
                        }

                    }
                    else
                    {
                        GameWorld.enemyHealth -= this.Damage;
                        //Attack enemy
                    }
                }
            }
            //Checks for the enemy cards which is below 500 in the y position
            if (this.position.Y < 500)
            {
                //Checks the front row of the enemy's cards
                if (this.spaceNumber >= 4)
                {
                    //Checks if there is a card in the front row of the player's board
                    if (playerSpaces[this.spaceNumber].card != null)
                    {
                        //Checks if the health of the player it attacks in the back row is an odd number
                        int num = playerSpaces[this.spaceNumber].card.Health;
                        if (num % 2 == 1)
                        {
                            //Kills the player card it attacks if it is an odd number
                            playerSpaces[this.spaceNumber].card.Health -= playerSpaces[this.spaceNumber].card.Health;
                            playerSpaces[this.spaceNumber].card.damageTaken += playerSpaces[this.spaceNumber].card.damageTaken;
                        }
                        else
                        {
                            //The card destroys itself
                            this.Health -= this.Health;
                        }


                    }
                    //Checks the front row of the player's cards if there is placed a card in the back row
                    else if (playerSpaces[this.spaceNumber - 4].card != null)
                    {
                        //Checks if the health of the player it attacks in the front row is an odd number
                        int num = playerSpaces[this.spaceNumber - 4].card.Health;
                        if (num % 2 == 1)
                        {
                            //Kills the player card the enemy attacks if it is an odd number
                            playerSpaces[this.spaceNumber - 4].card.Health -= playerSpaces[this.spaceNumber - 4].card.Health;
                            playerSpaces[this.spaceNumber - 4].card.damageTaken += playerSpaces[this.spaceNumber - 4].card.Health;
                        }
                        else
                        {
                            //The card destroys itself
                            this.Health -= this.Health;
                        }


                    }
                    else
                    {
                        GameWorld.playerHealth -= this.Damage;
                        //Attack player
                    }
                }
                //Checks if there is a card in the front row ONLY if there is a card in the back row for the enemy's board
                else if (enemySpaces[this.spaceNumber + 4].card == null) 
                {
                    //Checks the front row for the player's board
                    if (playerSpaces[this.spaceNumber].card != null)
                    {
                        //Checks if the health of the player it attacks in the front row is an odd number
                        int num = playerSpaces[this.spaceNumber].card.Health;
                        if (num % 2 == 1)
                        {
                            //Kills the player card it attacks if it is an odd number
                            playerSpaces[this.spaceNumber].card.Health -= playerSpaces[this.spaceNumber].card.Health;
                            playerSpaces[this.spaceNumber].card.damageTaken += playerSpaces[this.spaceNumber].card.Health;
                        }
                        else
                        {
                            //The card destroys itself
                            this.Health -= this.Health;
                        }

                    }
                    //else if (enemySpaces[this.spaceNumber].card != null) 
                    //{
                    //    int num = playerSpaces[this.spaceNumber - 4].card.Health;
                    //    if (num % 2 == 1)
                    //    {
                    //        playerSpaces[this.spaceNumber - 4].card.Health -= playerSpaces[this.spaceNumber - 4].card.Health;
                    //        playerSpaces[this.spaceNumber - 4].card.damageTaken += playerSpaces[this.spaceNumber - 4].card.Health;
                    //    }
                    //    else
                    //    {
                    //        this.Health -= this.Health;
                    //    }

                    //}
                    else
                    {
                        GameWorld.playerHealth -= this.Damage;
                        //Attack player
                    }
                }

            }
        }

        public Gambler()
        {
            this.Star = 1;
            this.Name = "Gambler";
            this.color = Color.White;
            this.Damage = 0;
            this.Health = 10;

        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Gambler");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
