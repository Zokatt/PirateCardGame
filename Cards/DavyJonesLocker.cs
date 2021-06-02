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
            int rndCardToHand = rnd.Next(0,105);

            if (this.Health <= 0)
            {

                if (rndCardToHand < 8)
                {
                    tmpCard = new Swapper();
                }
                if (rndCardToHand < 16 && rndCardToHand >= 8)
                {
                    tmpCard = new SmallSlime();
                }
                if (rndCardToHand < 24 && rndCardToHand >= 16)
                {
                    tmpCard = new SniperParrot();
                }
                if (rndCardToHand < 32 && rndCardToHand >= 24)
                {
                    tmpCard = new Cannibal();
                }
                if (rndCardToHand < 40 && rndCardToHand >= 32)
                {
                    tmpCard = new Musketeer();
                }
                if (rndCardToHand < 48 && rndCardToHand >= 40)
                {
                    tmpCard = new Thief();
                }
                if (rndCardToHand < 56 && rndCardToHand >= 48)
                {
                    tmpCard = new Gambler();
                }
                if (rndCardToHand < 64 && rndCardToHand >= 56)
                {
                    tmpCard = new DavyJonesLocker();
                }
                if (rndCardToHand < 72 && rndCardToHand >= 64)
                {
                    tmpCard = new BigSlime();
                }
                if (rndCardToHand < 80 && rndCardToHand >= 72)
                {
                    tmpCard = new Cannon();
                }
                if (rndCardToHand < 96 && rndCardToHand >= 88)
                {
                    tmpCard = new FatPirate();
                }
                if (rndCardToHand < 104 && rndCardToHand >= 96)
                {
                    tmpCard = new Whale();
                }
                if (rndCardToHand == 104)
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
