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
        //FIELDS
        private int width;
        private int height;
        private int bulletSpeed;
        protected Rectangle position;
        protected Texture2D texture;
        private string shootAngle;

        //PROPERTIES
        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }
        public int BulletSpeed
        {
            get { return bulletSpeed; }
        }



        //CONSTRUCTOR
        public bullet(int width, int height, Rectangle position, Texture2D texture, string shootAngle)
        { 
            this.width = width;
            this.height = height;
            this.shootAngle = shootAngle;
            this.position = position;
            this.texture = texture;
            bulletSpeed = 25;
        }

        //METHODS
        //Bullet shooting
        public void shootBullet()
        {
            if (shootAngle == "up")
            {
                position.Y -= bulletSpeed; //(where it is shot position +- speed of bullet) will give us the direction of bullet projectile movement
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

        //Draw bullet
        public void Draw(SpriteBatch sb)
        {
            //Rectangle zombieRect = new Rectangle(position.X, position.Y, 20, 20);
            sb.Draw(texture, new Rectangle(position.X, position.Y, 20, 20), Color.White);
        }
    }
}
