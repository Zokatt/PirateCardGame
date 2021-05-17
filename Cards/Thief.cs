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
        delegate void StealEventHandler(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces);
        event StealEventHandler StealAbilityEvent;
        private CardBase stealTarget;


        private void OnStealEvent(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (StealAbilityEvent!=null)
            {
                StealAbilityEvent(enemySpaces,playerSpaces);
            }
        }

        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.position.Y < 500)
               {
                if (this.spaceNumber <= 3)
                {
                    if (enemySpaces[this.spaceNumber + 4].card == null)
                    {
                        if (playerSpaces[this.spaceNumber].card !=null)
                        {
                            stealTarget = playerSpaces[this.spaceNumber].card;

                        }
                        else
                        {
                            stealTarget = playerSpaces[this.spaceNumber + 4].card;
                        }
                    }
                    else
                    {
                        stealTarget = enemySpaces[this.spaceNumber + 4].card;
                    }
                }
                else if (this.spaceNumber >= 4)
                {
                        if (playerSpaces[this.spaceNumber-4].card != null)
                        {
                        stealTarget = playerSpaces[this.spaceNumber - 4].card;
                        }
                        else
                        {
                        stealTarget = playerSpaces[this.spaceNumber].card;
                        }
                    
                }
            }
            else
            {
                if (this.spaceNumber <= 3)
                {
                        if (enemySpaces[this.spaceNumber+4].card != null)
                        {
                            stealTarget = enemySpaces[this.spaceNumber + 4].card;
                        }
                        else
                        {
                        stealTarget = enemySpaces[this.spaceNumber].card;
                        }
                }
                else if (this.spaceNumber >= 4)
                {
                    if (playerSpaces[this.spaceNumber-4].card == null)
                    {
                        if (enemySpaces[this.spaceNumber].card != null)
                        {
                            stealTarget = enemySpaces[this.spaceNumber].card;
                        }
                        else
                        {
                            stealTarget = enemySpaces[this.spaceNumber - 4].card;
                        }
                    }
                    else
                    {
                        stealTarget = playerSpaces[this.spaceNumber - 4].card;
                    }

                }
            }

            stealTarget.position = this.position;
            stealTarget.spaceNumber = this.spaceNumber;
            StealAbilityEvent += stealTarget.AdditionalCardEffect;
            OnStealEvent(enemySpaces,playerSpaces);
            stealTarget = null;
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

       

    }
}
