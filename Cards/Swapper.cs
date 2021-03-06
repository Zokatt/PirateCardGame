using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    /// <summary>
    /// The swapper card
    /// does nothing special
    /// </summary>
    ///<remarks>
    /// Nikolaj,Johnny
    /// </remarks>
    class Swapper : CardBase
    {
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
        }

        public Swapper()
        {
            this.color = Color.White;
            this.Name = "Swapper";
            this.Damage = 1;
            this.Health = 3;
            this.Coin = 1;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Swapper");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
