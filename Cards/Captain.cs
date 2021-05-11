using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class Captain : CardBase
    {
        public override void CardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.spaceNumber <=3)
            {
                if (enemySpaces[this.spaceNumber + 4].card != null)
                {
                    enemySpaces[this.spaceNumber + 4].card.Health -= this.Damage;
                }
                else if (enemySpaces[this.spaceNumber].card != null)
                {
                    enemySpaces[this.spaceNumber].card.Health -= this.Damage;
                }
                else
                {
                    //attack enemy
                }
            }
            else if (playerSpaces[this.spaceNumber -4].card == null)
            {
                if (enemySpaces[this.spaceNumber ].card != null)
                {
                    enemySpaces[this.spaceNumber ].card.Health -= this.Damage;
                }
                else if (enemySpaces[this.spaceNumber-4].card != null)
                {
                    enemySpaces[this.spaceNumber-4].card.Health -= this.Damage;
                }
                else
                {
                    //attack enemy
                }
            }
            
        }

        public Captain()
        {
            this.Damage = 4;
            this.Health = 12;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("PirateCaptain");
            this.color = Color.White;
        }
    }
}
