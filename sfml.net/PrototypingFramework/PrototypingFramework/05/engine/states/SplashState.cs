using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypingFramework.engine.states
{
    class SplashState:IGameState
    {
   
        private bool _exit;
        public override void  Init(GameEngine game)
{
 	 base.Init(game);
             _exit = false;

        
          
}
   

      

        public override void Cleanup()
        {
           
        }

        public override void Update( float gameTime)
        {
            if (this._exit)
                _game.PopState();
        }

        public override void Draw()
        {
     
        }

//=====Events=================================================================================================
        //changed event args to object because we need no info
        void Window_InputPressed(object sender, object eventArgs)
        {
            this._exit = true;
        }



        public override void BindEvents()
        {
            _game.Window.KeyPressed += new EventHandler<SFML.Window.KeyEventArgs>(Window_InputPressed);
            _game.Window.MouseButtonPressed += new EventHandler<SFML.Window.MouseButtonEventArgs>(Window_InputPressed);
        }

        public override void UnbindEvents()
        {
            _game.Window.KeyPressed -= new EventHandler<SFML.Window.KeyEventArgs>(Window_InputPressed);
            _game.Window.MouseButtonPressed -= new EventHandler<SFML.Window.MouseButtonEventArgs>(Window_InputPressed);
        }
    }
}
