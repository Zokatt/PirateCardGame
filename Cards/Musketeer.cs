using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    /// <summary>
    /// The Musketeer card
    /// Will damage all enemy cards in the same coloumn
    /// </summary>
    ///<remarks>
    /// Johnny
    /// </remarks>
    class Musketeer : CardBase
    {
        /// <summary>
        /// Will damage all enemy cards in the same coloumn
        /// <para>Also checks whoever it's owned by, by checking its position</para>
        /// </summary>
        ///<remarks>
        /// Johnny
        /// </remarks>
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.position.Y < 500)
            {
                MusketeerAttackPlayer(enemySpaces, playerSpaces);
            }
            else if (this.position.Y > 500)
            {
                MusketeerAttackEnemy(enemySpaces, playerSpaces);
            }

        }

        public Musketeer()
        {
            this.color = Color.White;
            this.Name = "Musketeer";
            this.Damage = 1;
            this.Health = 5;
            this.Coin = 2;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Musketeer");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
        /// <summary>
        /// Damages player cards in the same coloumn
        /// </summary>
        ///<remarks>
        /// Johnny
        /// </remarks>
        private void MusketeerAttackPlayer(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.spaceNumber <= 3)
            {
                if (playerSpaces[this.spaceNumber].card != null)
                {
                    playerSpaces[this.spaceNumber].card.Health -= this.Damage;
                    playerSpaces[this.spaceNumber].card.damageTaken += this.Damage;

                }
                if (playerSpaces[this.spaceNumber + 4].card != null)
                {
                    playerSpaces[this.spaceNumber + 4].card.Health -= this.Damage;
                    playerSpaces[this.spaceNumber + 4].card.damageTaken += this.Damage;
                }
                else
                {
                    //attack player
                }

            }
            else
            {
                if (playerSpaces[this.spaceNumber - 4].card != null)
                {
                    playerSpaces[this.spaceNumber - 4].card.Health -= this.Damage;
                    playerSpaces[this.spaceNumber - 4].card.damageTaken += this.Damage;
                }
                if (playerSpaces[this.spaceNumber].card != null)
                {
                    playerSpaces[this.spaceNumber].card.Health -= this.Damage;
                    playerSpaces[this.spaceNumber].card.damageTaken += this.Damage;
                }
                else
                {
                    //attack player
                }
            }

        }
        /// <summary>
        /// Damages enemy cards in the same coloumn
        /// </summary>
        ///<remarks>
        /// Johnny
        /// </remarks>
        private void MusketeerAttackEnemy(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.spaceNumber <=3)
            {
                if (enemySpaces[this.spaceNumber + 4].card != null)
                {
                    enemySpaces[this.spaceNumber + 4].card.Health -= this.Damage;
                    enemySpaces[this.spaceNumber + 4].card.damageTaken += this.Damage;
                    
                }
                if (enemySpaces[this.spaceNumber].card != null)
                {
                    enemySpaces[this.spaceNumber].card.Health -= this.Damage;
                    enemySpaces[this.spaceNumber].card.damageTaken += this.Damage;
                }
                else
                {
                    //attack enemy
                }
            }
            else
            {
                if (enemySpaces[this.spaceNumber].card != null)
                {
                    enemySpaces[this.spaceNumber].card.Health -= this.Damage;
                    enemySpaces[this.spaceNumber].card.damageTaken += this.Damage;

                }
                if (enemySpaces[this.spaceNumber - 4].card != null)
                {
                    enemySpaces[this.spaceNumber - 4].card.Health -= this.Damage;
                    enemySpaces[this.spaceNumber - 4].card.damageTaken += this.Damage;
                }
                else
                {
                    //attack enemy
                }
            }
        }
    }
}
