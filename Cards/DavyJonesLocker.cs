using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame.Cards
{
    public class DavyJonesLocker : CardBase
    {
        private CardBase tmpCard;
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            Random rnd = new Random();
            int rndCardToHand = rnd.Next(0,101);

            if (this.Health <= 0)
            {
                //switch (rndCard)
                //{
                //    case 1:
                //        tmpCard = new Cannibal();
                //        break;
                //    case 2:
                //        tmpCard = new Cannon();
                //        break;
                //    case 3:
                //        tmpCard = new Captain();
                //        break;
                //    case 4:
                //        tmpCard = new DavyJonesLocker();
                //        break;
                //    case 5:
                //        tmpCard = new FatPirate();
                //        break;
                //    case 6:
                //        tmpCard = new Musketeer();
                //        break;
                //    case 7:
                //        tmpCard = new Swapper();
                //        break;
                //    case 8:
                //        tmpCard = new Thief();
                //        break;
                //    case 9:
                //        tmpCard = new Whale();
                //        break;
                //}


                if (rndCardToHand < 40)
                {
                    tmpCard = new Swapper();
                }
                if (rndCardToHand < 60 && rndCardToHand >= 40)
                {
                    tmpCard = new Cannibal();
                }
                if (rndCardToHand < 75 && rndCardToHand >= 60)
                {
                    tmpCard = new Musketeer();
                }
                if (rndCardToHand < 85 && rndCardToHand >= 75)
                {
                    tmpCard = new Thief();
                }
                if (rndCardToHand < 90 && rndCardToHand >= 85)
                {
                    tmpCard = new DavyJonesLocker();
                }
                if (rndCardToHand < 93 && rndCardToHand >= 90)
                {
                    tmpCard = new Cannon();
                }
                if (rndCardToHand < 96 && rndCardToHand >= 93)
                {
                    tmpCard = new FatPirate();
                }
                if (rndCardToHand < 100 && rndCardToHand >= 96)
                {
                    tmpCard = new Whale();
                }
                if (rndCardToHand == 100)
                {
                    tmpCard = new Captain();
                }


                if (this.position.Y < 500)
                {
                    GameWorld.enemy.EnemyHand.Add(tmpCard);
                }
                else
                {
                    GameWorld.playerCards.Add(tmpCard);
                }

            }

        }


        public DavyJonesLocker()
        {
            this.color = Color.White;
            this.Name = "DavyJonesLocker";
            this.Damage = 0;
            this.Health = 1;
            this.Coin = 2;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("DavyJonesLocker");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }

    }
}
