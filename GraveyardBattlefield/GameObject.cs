using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace GraveyardBattlefield
{
    public abstract class GameObject
    {
        //fields
        protected static Vector2 position;
        protected static Texture2D texture;


        //property
        public static Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public static Texture2D Texture { get; set; }

        //constructor
        protected GameObject(Vector2 position, Texture2D texture)
        {
            GameObject.Position = position;
            GameObject.Texture = texture;
        }

        //methods
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, Position, Color.White);
        }

        public virtual void Update(GameTime gametime, KeyboardState currentKBState) { }
    }
}
