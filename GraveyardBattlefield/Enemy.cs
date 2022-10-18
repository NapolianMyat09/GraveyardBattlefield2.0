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
    /*
     * Project: Graveyard BattleField
     * Names: Tracy Chun, Jason Wang, Napolian Myat
     * Class: Enemy
     * Purpose: Handles anything involving enemies' damage, health, position, etc.
     * 
     * Updates:
     * 
     */
    internal class Enemy:GameObject
    {
        //fields
        private int width;
        private int height;

        //constructor
        public Enemy(int width, int height, Texture2D texture, Rectangle position) : base(position, texture)
        {
            this.width = width;
            this.height = height;
        }

        public override void Update(GameTime gametime) { }
    }
}
