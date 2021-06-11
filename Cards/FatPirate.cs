using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    /// <summary>
    /// Fat Pirate Card
    /// <para>Will increase it's damage at the end of turn</para>
    /// </summary>
    ///<remarks>
    /// Johnny
    /// </remarks>
    class FatPirate : CardBase
    {
        /// <summary>
        /// Will increase it's own damage by 1
        /// </summary>
        ///<remarks>
        /// Johnny
        /// </remarks>
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            //if (this.position.Y > 500)
            //{
            //    if (playerSpaces[this.spaceNumber].card != null)
            //    {
            //        playerSpaces[this.spaceNumber].card.Damage += 1;
            //    }
            //}
            //if (this.position.Y < 500)
            //{
            //    if (enemySpaces[this.spaceNumber].card != null)
            //    {
            //        enemySpaces[this.spaceNumber].card.Damage += 1;
            //    }
            //}
            this.Damage += 1;

        }

        public FatPirate()
        {
            this.Coin = 4;
            this.color = Color.White;
            this.Name = "FatPirate";
            this.Damage = 3;
            this.Health = 6;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("FatPirate");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
