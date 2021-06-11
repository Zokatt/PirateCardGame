using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    /// <summary>
    /// This class is used for any UI the game might have
    /// </summary>
    ///<remarks>
    /// Nikolaj, Johnny
    /// </remarks>
    public class UI : GameObject
    {
        /// <summary>
        /// Used to deternime which sprite this UI should be generates with
        /// </summary>
        public string spritePick;
        /// <summary>
        /// Used to check whetever this ui has been clicked
        /// </summary>
        public bool clicked;
        /// <summary>
        /// when making a new UI, write the sprite you want in the constructor
        /// </summary>
        public UI(string whichUI,Vector2 pos)
        {
            this.spritePick = whichUI;
            this.color = Color.White;
            this.position = pos;
        }
        /// <summary>
        /// when loadcontent is called, whatever was written in the constructor will be loaded
        /// </summary>
        public override void LoadContent(ContentManager contentManager)
        {
            this.sprite = contentManager.Load<Texture2D>($"{spritePick}");
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
