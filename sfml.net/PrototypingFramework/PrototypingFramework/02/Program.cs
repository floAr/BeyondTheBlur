using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using PrototypingFramework.engine;

namespace PrototypingFramework
{
    /// <summary>
    /// Main class of our project
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry routine for our programm
        /// </summary>
        /// <param name="args">Parameters passed from console as string array</param>
        static void Main(string[] args)
        {

            GameEngine _game = new GameEngine();
            _game.Init("Prototype Framework");
            while (_game.isRunning)
            {
                _game.Update();
                _game.Draw();
            }
            _game.Cleanup();
           
        }
    }
}
