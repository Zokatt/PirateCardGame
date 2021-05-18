using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    class Whale : CardBase
    {
        public int whaleDmg = 6;
        //public int whaleRound = 0;
        public List<int> whaleRound = new List<int>()
        {
            1,2,3,4,5,6,7,8
        };

        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            
            foreach (CardSpace item in playerSpaces)
            {
                if (item.card != null)
                {
                    item.card.Health -= whaleDmg;
                    item.card.damageTaken -= whaleDmg;
                    //whaleRound += 1;
                }
                //if (whaleRound >= 2)
                //{
                //    whaleDmg = 0;
                //}
                if (item.card == null)
                {
                    //whaleRound = 0;
                }
            }
            for (int i = 0; i < playerSpaces.Count; i++)
            {
                if (playerSpaces[i].card != null)
                {
                    playerSpaces[i].card.Health -= whaleDmg;
                    playerSpaces[i].card.damageTaken -= whaleDmg;

                }
            }
            foreach (CardSpace item in enemySpaces)
            {
                if (item.card != null)
                {
                    item.card.Health -= whaleDmg;
                    item.card.damageTaken -= whaleDmg;
                }
            }
        }

        public Whale()
        {
            this.Name = "Whale";
            this.Damage = 0;
            this.Health = 100;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("Whale");
            this.color = Color.White;
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }
    }
}
