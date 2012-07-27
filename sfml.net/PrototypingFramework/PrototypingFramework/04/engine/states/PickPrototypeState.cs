using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PrototypingFramework.engine.states
{
    public class PickPrototypeState : IGameState
    {
        private SFML.Graphics.Text[] _prototypeLabels;

        private Type[] _prototypeStates;

        public override void Init(GameEngine game)
        {
            base.Init(game);
            List<SFML.Graphics.Text> protptypeLabel = new List<SFML.Graphics.Text>();
            List<Type> prototypeStates = new List<Type>();

            Assembly assembly = Assembly.GetExecutingAssembly();


            Type[] classes = assembly.GetTypes();
            int i =0;
            foreach (Type t in classes)
            {
                if (t.BaseType.Name.Equals("IPrototype"))
                {
                    SFML.Graphics.Text text = new SFML.Graphics.Text(i+". :"+t.Name);
                    text.Position = new SFML.Window.Vector2f(50, 20 + i * 30);
                    protptypeLabel.Add(text);
                    prototypeStates.Add(t);
                    i++;
                }
            }

            _prototypeLabels = protptypeLabel.ToArray<SFML.Graphics.Text>();
            _prototypeStates = prototypeStates.ToArray<Type>();

          
        }


        public override void Cleanup()
        {
          
        }

        public override void Update( float gameTime)
        {

        }

        public override void Draw()
        {
            for (int i = 0; i < _prototypeLabels.Length; ++i)
            {
               
                _game.Window.Draw(_prototypeLabels[i]);
            }
        }


    

        void Window_MouseButtonPressed(object sender, SFML.Window.MouseButtonEventArgs e)
        {
            Console.WriteLine("c");
            for (int i = 0; i < _prototypeLabels.Length; ++i)
            {
                SFML.Graphics.FloatRect fr = _prototypeLabels[i].GetGlobalBounds();
                Console.WriteLine(_prototypeLabels[i].DisplayedString + "x: " + fr.Left + "y: " + fr.Top + "w: " + fr.Width + "h: " + fr.Height + "|" + "X: " + e.X + "Y: " + e.Y);
                if (fr.Contains(e.X, e.Y))
                {
                   _game.PushState((IGameState)(_prototypeStates[i].GetConstructor(new Type[] { }).Invoke(new Object[] { })));
                }
            }
        }

        void Window_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            if (e.Code == SFML.Window.Keyboard.Key.Num1)
            {
                _game.PushState((IGameState)(_prototypeStates[1].GetConstructor(new Type[] { }).Invoke(new Object[] { })));
            }
        }

        public override void BindEvents()
        {
            _game.Window.MouseButtonPressed += new EventHandler<SFML.Window.MouseButtonEventArgs>(Window_MouseButtonPressed);
            _game.Window.KeyPressed += new EventHandler<SFML.Window.KeyEventArgs>(Window_KeyPressed);
        }

      

        public override void UnbindEvents()
        {
            _game.Window.MouseButtonPressed -= new EventHandler<SFML.Window.MouseButtonEventArgs>(Window_MouseButtonPressed);
            _game.Window.KeyPressed -= new EventHandler<SFML.Window.KeyEventArgs>(Window_KeyPressed);
        }
    }
}
