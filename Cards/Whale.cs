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
        //public int whaleDmg = 6;
        public int whaleSpace = 0;
        public int whale1 = 0;
        public int whale2 = 0;
        public int whale3 = 0;
        public int whale4 = 0;
        public int whale5 = 0;
        public int whale6 = 0;
        public int whale7 = 0;
        public int whale8 = 0;
        public int whaleDmg1 = 6;
        public int whaleDmg2 = 6;
        public int whaleDmg3 = 6;
        public int whaleDmg4 = 6;
        public int whaleDmg5 = 6;
        public int whaleDmg6 = 6;
        public int whaleDmg7 = 6;
        public int whaleDmg8 = 6;
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {

            //foreach (CardSpace item in playerSpaces)
            //{
            //    if (item.card != null)
            //    {
            //        item.card.Health -= whaleDmg;
            //        item.card.damageTaken -= whaleDmg;

            //    }

            //}
            //foreach (CardSpace item in enemySpaces)
            //{
            //    if (item.card != null)
            //    {
            //        item.card.Health -= whaleDmg;
            //        item.card.damageTaken -= whaleDmg;
            //    }
            //}

            for (int i = 0; i < playerSpaces.Count; i++)
            {
                if (playerSpaces[i].card != null)
                {
                    whaleSpace = i;

                    switch (whaleSpace)
                    {
                        case 1:
                            whale1 += 1;
                            playerSpaces[i].card.Health -= whaleDmg1;
                            if (whale1 >= 2)
                            {
                                whaleDmg1 = 0;
                            }
                            break;
                        case 2:
                            whale2 += 1;
                            playerSpaces[i].card.Health -= whaleDmg2;
                            if (whale2 >= 2)
                            {
                                whaleDmg2 = 0;
                            }
                            break;
                        case 3:
                            whale3 += 1;
                            playerSpaces[i].card.Health -= whaleDmg3;
                            if (whale3 >= 2)
                            {
                                whaleDmg3 = 0;
                            }
                            break;
                        case 4:
                            whale4 += 1;
                            playerSpaces[i].card.Health -= whaleDmg4;
                            if (whale4 >= 2)
                            {
                                whaleDmg4 = 0;
                            }
                            break;
                        case 5:
                            whale5 += 1;
                            playerSpaces[i].card.Health -= whaleDmg5;
                            if (whale5 >= 2)
                            {
                                whaleDmg5 = 0;
                            }
                            break;
                        case 6:
                            whale6 += 1;
                            playerSpaces[i].card.Health -= whaleDmg6;
                            if (whale6 >= 2)
                            {
                                whaleDmg6 = 0;
                            }
                            break;
                        case 7:
                            whale7 += 1;
                            playerSpaces[i].card.Health -= whaleDmg7;
                            if (whale7 >= 2)
                            {
                                whaleDmg7 = 0;
                            }
                            break;
                        case 8:
                            whale8 += 1;
                            playerSpaces[i].card.Health -= whaleDmg8;
                            if (whale8 >= 2)
                            {
                                whaleDmg8 = 0;
                            }
                            break;
                    }
                }
            }
            for (int i = 0; i < enemySpaces.Count; i++)
            {
                if (enemySpaces[i].card != null)
                {
                    whaleSpace = i;

                    switch (whaleSpace)
                    {
                        case 0:
                            whale1 += 1;
                            enemySpaces[i].card.Health -= whaleDmg1;
                            if (whale1 >= 1)
                            {
                                whaleDmg1 = 0;
                            }
                            break;
                        case 1:
                            whale2 += 1;
                            enemySpaces[i].card.Health -= whaleDmg2;
                            if (whale2 >= 1)
                            {
                                whaleDmg2 = 0;
                            }
                            break;
                        case 2:
                            whale3 += 1;
                            enemySpaces[i].card.Health -= whaleDmg3;
                            if (whale3 >= 1)
                            {
                                whaleDmg3 = 0;
                            }
                            break;
                        case 3:
                            whale4 += 1;
                            enemySpaces[i].card.Health -= whaleDmg4;
                            if (whale4 >= 1)
                            {
                                whaleDmg4 = 0;
                            }
                            break;
                        case 4:
                            whale5 += 1;
                            enemySpaces[i].card.Health -= whaleDmg5;
                            if (whale5 >= 1)
                            {
                                whaleDmg5 = 0;
                            }
                            break;
                        case 5:
                            whale6 += 1;
                            enemySpaces[i].card.Health -= whaleDmg6;
                            if (whale6 >= 1)
                            {
                                whaleDmg6 = 0;
                            }
                            break;
                        case 6:
                            whale7 += 1;
                            enemySpaces[i].card.Health -= whaleDmg7;
                            if (whale7 >= 1)
                            {
                                whaleDmg7 = 0;
                            }
                            break;
                        case 7:
                            whale8 += 1;
                            enemySpaces[i].card.Health -= whaleDmg8;
                            if (whale8 >= 1)
                            {
                                whaleDmg8 = 0;
                            }
                            break;
                    }
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
