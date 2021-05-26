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
                           
                            stealTarget = playerSpaces[this.spaceNumber].card.Clone();
                        }
                        else if (playerSpaces[this.spaceNumber + 4].card != null)
                        {
                            stealTarget = playerSpaces[this.spaceNumber+4].card.Clone();
                        }
                        
                    }
                    else if (enemySpaces[this.spaceNumber + 4].card != null)
                    {
                        stealTarget = enemySpaces[this.spaceNumber + 4].card.Clone();
                    }
                }
                else if (this.spaceNumber >= 4)
                {
                        if (playerSpaces[this.spaceNumber-4].card != null)
                        {
                        stealTarget = playerSpaces[this.spaceNumber - 4].card.Clone();
                        }
                        else if (playerSpaces[this.spaceNumber].card != null)
                        {
                        stealTarget = playerSpaces[this.spaceNumber].card.Clone();
                        }
                    
                }
            }
            else
            {
                if (this.spaceNumber <= 3)
                {
                        if (enemySpaces[this.spaceNumber+4].card != null)
                        {
                        stealTarget = enemySpaces[this.spaceNumber + 4].card.Clone();
                        }
                        else if(enemySpaces[this.spaceNumber].card != null)
                        {
                        stealTarget = enemySpaces[this.spaceNumber].card.Clone();
                        }
                }
                else if (this.spaceNumber >= 4)
                {
                    if (playerSpaces[this.spaceNumber-4].card == null)
                    {
                        if (enemySpaces[this.spaceNumber].card != null)
                        {
                            stealTarget = enemySpaces[this.spaceNumber].card.Clone();
                        }
                        else if (enemySpaces[this.spaceNumber-4].card != null)
                        {
                            stealTarget = enemySpaces[this.spaceNumber - 4].card.Clone();
                        }
                    }
                    else if (playerSpaces[this.spaceNumber-4].card !=null)
                    {
                        stealTarget = playerSpaces[this.spaceNumber - 4].card.Clone();
                    }

                }
            }

            if (stealTarget != null && stealTarget.Name != "Thief")
            {
                stealTarget.position = this.position;
                stealTarget.spaceNumber = this.spaceNumber;
                stealTarget.Damage = this.Damage;
                stealTarget.Health = this.Health;
                StealAbilityEvent += stealTarget.AdditionalCardEffect;
                OnStealEvent(enemySpaces, playerSpaces);

                this.Damage = stealTarget.Damage;
                this.Health = stealTarget.Health;
            }
            stealTarget = null;
        }

        public Thief()
        {

            this.color = Color.White;
            this.Name = "Thief";
            this.Damage = 1;
            this.Health = 6;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Thief");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }

       

    }
}
