using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class Mimic : CardBase
    {
        public Mimic()
        {
            this.Coin = 4;
            this.Name = "Mimic";
            this.Damage = 1;
            this.Health = 1;
            this.color = Color.White;
        }
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.position.Y<500)
            {
                if (this.spaceNumber<=3)
                {
                    if (enemySpaces[this.spaceNumber+4].card==null)
                    {
                        if (playerSpaces[this.spaceNumber].card!=null)
                        {
                            enemySpaces[this.spaceNumber].SwitchCardEnemy(this.spaceNumber, this.spaceNumber, enemySpaces, playerSpaces);
                            this.Health = 0;
                        }
                        else if (playerSpaces[this.spaceNumber+4].card!=null)
                        {
                            enemySpaces[this.spaceNumber].SwitchCardEnemy(this.spaceNumber, this.spaceNumber+4, enemySpaces, playerSpaces);
                            this.Health = 0;
                        }
                    }
                }
                else
                {
                    if (playerSpaces[this.spaceNumber-4].card != null)
                    {
                        enemySpaces[this.spaceNumber].SwitchCardEnemy(this.spaceNumber, this.spaceNumber-4, enemySpaces, playerSpaces);
                        this.Health = 0;
                    }
                    else if (playerSpaces[this.spaceNumber].card != null)
                    {
                        enemySpaces[this.spaceNumber].SwitchCardEnemy(this.spaceNumber, this.spaceNumber, enemySpaces, playerSpaces);
                        this.Health = 0;
                    }
                }
                this.color = Color.White;
            }
            else
            {
                if (this.spaceNumber<=3)
                {
                    if (enemySpaces[this.spaceNumber+4].card !=null)
                    {
                        playerSpaces[this.spaceNumber].SwitchCardPlayer(this.spaceNumber, this.spaceNumber + 4, enemySpaces, playerSpaces);
                        this.Health = 0;
                    }
                    else if (enemySpaces[this.spaceNumber].card != null)
                    {
                        playerSpaces[this.spaceNumber].SwitchCardPlayer(this.spaceNumber, this.spaceNumber, enemySpaces, playerSpaces);
                        this.Health = 0;
                    }
                }
                else
                {
                    if (playerSpaces[this.spaceNumber-4].card == null)
                    {
                        if (enemySpaces[this.spaceNumber].card != null)
                        {
                            playerSpaces[this.spaceNumber].SwitchCardPlayer(this.spaceNumber, this.spaceNumber, enemySpaces, playerSpaces);
                            this.Health = 0;
                        }
                        else if (enemySpaces[this.spaceNumber-4].card != null)
                        {
                            playerSpaces[this.spaceNumber].SwitchCardPlayer(this.spaceNumber, this.spaceNumber-4, enemySpaces, playerSpaces);
                            this.Health = 0;
                        }
                    }
                }
                this.color = Color.White;
            }
            
        }
        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Mimic");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
