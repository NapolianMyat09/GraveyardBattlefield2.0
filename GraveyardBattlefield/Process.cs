using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraveyardBattlefield
{
    /*
     * Project: Graveyard BattleField
     * Names: Tracy Chun, Jason Wang, Napolian Myat
     * Class: Process
     * Purpose: - methods and fields that deal with processing data 
     * 
     * Updates:
     * 
     */
     class Process
    {
        //fields
        public static KeyboardState prevKbState;
        public static KeyboardState currentkbState = Keyboard.GetState();
        
        /// <summary>
        /// Checks if the key processed through is pressed by checking keyboard states
        /// </summary>
        /// <param name="key"></param>
        /// <param name="kbState"></param>
        public static bool SingleKeyPress(Keys key, KeyboardState kbState)
        {
            if(kbState.IsKeyDown(key) && prevKbState.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// load text file with data in a specific format 
        /// save all the information in the correct places
        /// </summary>
        /// <param name="path"></param>
        public void LoadFile(string path)
        {
            StreamReader output = null;
            {
                try
                {
                    //check for the file path
                    output = new StreamReader(path);

                    //loop through data in the text file and split apart with console writelines
                    string line = null;
                    List<string> lines = new List<string>();
                    int i = 0;
                    while((line = output.ReadLine()) != null)
                    {
                        lines[i] = output.ReadLine();
                        i++;
                    }
                    /*
                     * put each item in the respective field using the list
                     */
                    output.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("There seems to an error loading the text file: " + ex.Message);
                }
            }
        }
    }
}
