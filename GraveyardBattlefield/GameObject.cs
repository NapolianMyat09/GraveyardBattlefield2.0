using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraveyardBattlefield
{
    internal abstract class GameObject
    {
        //fields
        protected Rectangle position;
        protected Texture2D texture;

        //property
        public Rectangle Position
        {
            get { return position; }
        }

        //constructor
        protected GameObject(Rectangle position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
        }

        //methods
        public virtual void Draw(SpriteBatch sb) { }

        public abstract void Update(GameTime gametime);
    }
}
