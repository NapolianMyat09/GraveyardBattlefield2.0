using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraveyardBattlefield
{
    internal class bullet
    {
        //fields
        private int width;
        private int height;
        private int bulletSpeed;
        protected Rectangle position;
        protected Texture2D texture;
        private string shootAngle;

        //property
        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }
        public int BulletSpeed
        {
            get { return bulletSpeed; }
        }

        public bullet(int width, int height, Rectangle position, Texture2D texture, string shootAngle)
        { 
            this.width = width;
            this.height = height;
            this.shootAngle = shootAngle;
            this.position = position;
            this.texture = texture;
            bulletSpeed = 10;
        }

        //methods
        public void shootBullet()
        {
            if (shootAngle == "up")
            {
                position.Y -= bulletSpeed;
            }
            if (shootAngle == "left")
            {
                position.X -= bulletSpeed;
            }
            if (shootAngle == "down")
            {
                position.Y += bulletSpeed;
            }
            if (shootAngle == "right")
            {
                position.X += bulletSpeed;
            }

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, Color.White);
        }
    }
}
