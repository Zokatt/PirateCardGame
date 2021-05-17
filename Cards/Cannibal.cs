﻿using Microsoft.Xna.Framework;
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
            this.Damage = 1;
            this.Health = 5;
        }
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            if (this.position.Y<500)
            {
                if (this.spaceNumber <= 3)
                {
                    if (enemySpaces[this.spaceNumber + 4].card == null)
                    {
                        this.Health += this.Damage;
                    }
                }
                else if(this.spaceNumber >=4)
                {
                    this.Health += this.Damage;
                }
            }
            else
            {
                if (this.spaceNumber >=4)
                {
                    if (playerSpaces[this.spaceNumber -4].card == null)
                    {
                        this.Health += this.Damage;
                    }
                }
                else if (this.spaceNumber <=3)
                {
                    this.Health += this.Damage;
                }
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Cannibal");
            this.color = Color.White;
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}