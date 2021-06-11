using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriateCardGame
{
    /// <summary>
    /// GameObject Class, used for all object in the game to make sure they have all the required method to function
    /// </summary>
    ///<remarks>
    /// Nikolaj, Johnny
    /// </remarks>
    public abstract class GameObject
    {
        /// <summary>
        /// postion to determine where the object is
        /// </summary>
        public Vector2 position;
        /// <summary>
        /// the sprite for the gameobject
        /// </summary>
        public Texture2D sprite;
        /// <summary>
        /// Set color to white unless a specific color is wanted
        /// </summary>
        public Color color;

        /// <summary>
        /// Collision is needed to check for collision with other gameobjects and also the mouse
        /// </summary>
        public virtual Rectangle Collision
        {
            get
            {
                return new Rectangle(
                       (int)position.X,
                       (int)position.Y,
                       (int)sprite.Width,
                       (int)sprite.Height
                   );
            }
        }
        /// <summary>
        /// Used to load the texture file for the sprite
        /// </summary>
        public abstract void LoadContent(ContentManager contentManager);
        /// <summary>
        /// To run update on the gameobject if needed
        /// </summary>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// the draw method so that the gameobject will be drawn
        /// <para>call this in draw in gameworld!!!</para>
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, color);
        }

    }
}
