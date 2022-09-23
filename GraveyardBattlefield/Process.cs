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
     * Names: Tracy Chun, Jason Wang, Napolian Myat, Ryan Matos
     * Class: Process
     * Purpose: - methods and fields that deal with processing data 
     * 
     * Updates:
     * 
     */
    internal class Process
    {
        //fields
        public KeyboardState PreviouskbState { get; set; }
        /// <summary>
        /// Checks if the key processed through is pressed by checking keyboard states
        /// </summary>
        /// <param name="key"></param>
        /// <param name="kbState"></param>
        public void SingleKeyPress(Keys key, KeyboardState kbState)
        {
            if(kbState.IsKeyDown(key) && PreviouskbState.IsKeyUp(key))
            {

            }
        }
    }
}
