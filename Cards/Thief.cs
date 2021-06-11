using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    /// <summary>
    /// The Thief card
    /// <para>Will steal the ability of whatever card is in front</para>
    /// </summary>
    ///<remarks>
    /// Nikolaj
    /// </remarks>
    class Thief : CardBase
    {
        /// <summary>
        /// The delegate handler for the thief event
        /// </summary>
        delegate void StealEventHandler(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces);
        /// <summary>
        /// The event for the thief
        /// </summary>
        event StealEventHandler StealAbilityEvent;
        /// <summary>
        /// A card which will clone the target so that we can steal the AdditionalCardEffect
        /// </summary>
        private CardBase stealTarget;

        /// <summary>
        /// This will run whatever effect that has been stolen
        /// </summary>
        ///<remarks>
        /// Nikolaj
        /// </remarks>
        private void OnStealEvent(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (StealAbilityEvent!=null)
            {
                StealAbilityEvent(enemySpaces,playerSpaces);
            }
        }
        /// <summary>
        /// Steal ability of card in front
        /// </summary>
        /// <remarks>
        /// Nikolaj
        /// </remarks>
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
                            /// <summary>
                            /// Makes a clone of the card in front
                            /// </summary>
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
            /// <summary>
            /// If there is a stolen effect
            /// <para>and it's not another thief</para>
            /// </summary>
            if (stealTarget != null && stealTarget.Name != "Thief")
            {
                //sets all the locale cards fields so that it will attack the correct player
                stealTarget.position = this.position;
                stealTarget.spaceNumber = this.spaceNumber;
                stealTarget.Damage = this.Damage;
                stealTarget.Health = this.Health;
                //Adds the card effect of the stolen card to the steal event
                StealAbilityEvent += stealTarget.AdditionalCardEffect;
                //runs that event
                OnStealEvent(enemySpaces, playerSpaces);
                //sets the thief fields in case the effect were effects such as increase it's own damage
                this.Damage = stealTarget.Damage;
                this.Health = stealTarget.Health;
            }
            //resets the stealtarget 
            stealTarget = null;
        }

        public Thief()
        {
            this.Coin = 2;
            this.color = Color.White;
            this.Name = "Thief";
            this.Damage = 1;
            this.Health = 4;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Thief");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }

       

    }
}
