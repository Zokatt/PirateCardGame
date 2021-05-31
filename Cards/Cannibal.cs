using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class Cannibal : CardBase
    {

        public Cannibal()
        {
            this.Coin = 2;
            this.color = Color.White;
            this.Name = "Cannibal";
            this.Damage = 2;
            this.Health = 3;
        }
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.position.Y<500)
            {
                if (this.spaceNumber <= 3)
                {
                    if (enemySpaces[this.spaceNumber + 4].card == null)
                    {
                        if (playerSpaces[this.spaceNumber].card!=null)
                        {
                            if (playerSpaces[this.spaceNumber].card.Name != "Cannibal")
                            {
                                this.Health += this.Damage;
                            }
                        }
                        else if (playerSpaces[this.spaceNumber+4].card!=null)
                        {
                            if (playerSpaces[this.spaceNumber+4].card.Name != "Cannibal")
                            {
                                this.Health += this.Damage;
                            }
                        }
                    }
                }
                else if(this.spaceNumber >=4)
                {
                    if (playerSpaces[this.spaceNumber-4].card != null)
                    {
                        if (playerSpaces[this.spaceNumber-4].card.Name != "Cannibal")
                        {
                            this.Health += this.Damage;
                        }
                    }
                    else if (playerSpaces[this.spaceNumber].card != null)
                    {
                        if (playerSpaces[this.spaceNumber].card.Name != "Cannibal")
                        {
                            this.Health += this.Damage;
                        }
                    }
                }
            }
            else
            {
                if (this.spaceNumber >=4)
                {
                    if (playerSpaces[this.spaceNumber - 4].card == null)
                    {
                        if (enemySpaces[this.spaceNumber].card != null)
                        {
                            if (enemySpaces[this.spaceNumber].card.Name != "Cannibal")
                            {
                                this.Health += this.Damage;
                            }
                        }
                        else if (enemySpaces[this.spaceNumber-4].card != null)
                        {
                            if (enemySpaces[this.spaceNumber-4].card.Name != "Cannibal")
                            {
                                this.Health += this.Damage;
                            }
                        }
                    }
                }
                else if (this.spaceNumber <=3)
                {
                    if (enemySpaces[this.spaceNumber+4].card != null)
                    {
                        if (enemySpaces[this.spaceNumber+4].card.Name != "Cannibal")
                        {
                            this.Health += this.Damage;
                        }
                    }
                    else if (enemySpaces[this.spaceNumber].card != null)
                    {
                        if (enemySpaces[this.spaceNumber].card.Name != "Cannibal")
                        {
                            this.Health += this.Damage;
                        }
                    }
                }
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Cannibal");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
