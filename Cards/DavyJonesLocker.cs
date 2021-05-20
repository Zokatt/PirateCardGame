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
        public override void AdditionalCardEffect(List<CardSpace> enemySpaces, List<CardSpace> playerSpaces)
        {
            Random rnd = new Random();
            int rndCard = rnd.Next(1, 10);
            string cardName = "";
            if (this.Health <= 0)
            {
                switch (rndCard)
                {
                    case 1:
                        cardName = "Cannibal";
                        break;
                    case 2:
                        cardName = "Cannon";
                        break;
                    case 3:
                        cardName = "Captain";
                        break;
                    case 4:
                        cardName = "DavyJonesLocker";
                        break;
                    case 5:
                        cardName = "FatPirate";
                        break;
                    case 6:
                        cardName = "Musketeer";
                        break;
                    case 7:
                        cardName = "Swapper";
                        break;
                    case 8:
                        cardName = "Thief";
                        break;
                    case 9:
                        cardName = "Whale";
                        break;
                }
                GameWorld.repo.Open();

                GameWorld.repo.AddCard(cardName);

                GameWorld.PlayerDeck = GameWorld.repo.FindDeck();

                GameWorld.repo.Close();
                
            }

        }


        public DavyJonesLocker()
        {
            this.color = Color.White;
            this.Name = "DavyJonesLocker";
            this.Damage = 1;
            this.Health = 5;
            
        }

        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>("PirateCaptain");
            this.DamageBox = contentManager.Load<Texture2D>("DamageBox");
        }

    }
}
