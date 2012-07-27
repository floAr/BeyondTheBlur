using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using System.Diagnostics;
using PrototypingFramework.engine.states;

namespace PrototypingFramework.engine
{
    public class GameEngine
    {

        /// <summary>
        /// SFML window to render all our stuff
        /// </summary>
        private RenderWindow _window;

        /// <summary>
        /// Stack for the game states
        /// </summary>
        private Stack<IGameState> _states;

        /// <summary>
        /// Stopwatch to track the game time.
        /// </summary>
        private Stopwatch _timer;

        /// <summary>
        /// Timer to track the elapsed time beween updates
        /// </summary>
        private float _gameTime;

        /// <summary>
        /// Boolean indicating if the game is currently running or not
        /// False when game has not been initiated or is waiting for disposal, true elsewise.
        /// </summary>
        private bool _isRunning;   

        /// <summary>
        /// Boolean indicating if the game is currently running or not
        /// False when game has not been initiated or is waiting for disposal, true elsewise.
        /// </summary>
        public bool isRunning { get { return _isRunning; } }


        /// <summary>
        /// Access to the render window to register events and drawing
        /// </summary>
        public RenderWindow Window { get { return _window; } }
        /// <summary>
        /// Initializes the game engine. Creates the RenderWindow and prepare the game loop.
        /// </summary>
        /// <param name="gameTitle">Title of our game</param>
        public void Init(String gameTitle)
        {
            _window = new RenderWindow(SFML.Window.VideoMode.DesktopMode, gameTitle, SFML.Window.Styles.Default);           
            _window.Closed += new EventHandler(window_Closed);
            _states = new Stack<IGameState>();            
            _timer = new Stopwatch();           
            _timer.Start();
            _isRunning = true;
            SpriteBatch.Init(15,15, 16, 16);
           //Add a new default state
            //this.PushState(new PickPrototypeState());
            this.PushState(new PrototypingFramework.protoypes.batched_protype.batched_protype());
        }

        void window_Closed(object sender, EventArgs e)
        {
            this._isRunning = false;
        }
        /// <summary>
        /// Clean up ressources used by game engine
        /// </summary>
        public void Cleanup()
        {

            foreach (IGameState state in _states)
            {
                state.Cleanup();
            }
           // _window.Dispose();
        }

        /// <summary>
        /// Push a new active state on the engine stack.
        /// </summary>
        /// <param name="state">State that we will switch to</param>
        public void PushState(IGameState state)
        {
            Console.Write("state pushed");
            if(_states.Count>0)
            _states.Peek().Pause();
            state.Init(this);
            _states.Push(state);
        }

        /// <summary>
        /// Pop the current state and clean it up
        /// </summary>
        public void PopState()
        {
            IGameState pState = _states.Pop();
            pState.Cleanup();
            if (_states.Count > 0)
            _states.Peek().Resume();
        }

        /// <summary>
        /// Update loop for the engine, updates the currently active state or exit the game if there is none
        /// </summary>
        public void Update()
        {
            if (!_isRunning)
                return;

            _window.DispatchEvents();
            if (_states.Count == 0)
            {
                this.Quit();
                return;
            }
            _gameTime = _timer.ElapsedMilliseconds;
            _timer.Reset();
            _timer.Start();

            _states.Peek().Update( _gameTime);

        }

        Stopwatch drawTimer = new Stopwatch();
        float av;
        float loops;
        /// <summary>
        /// Draw loop for the engine, draws the currently active state or exit the game if there is none
        /// </summary>
        public void Draw()
        {
            if (!_isRunning)
                return;

            if (_states.Count == 0)
            {
                this.Quit();
                return;
            }
            _window.Clear(new Color(100, 149, 237));
            drawTimer.Start();
            SpriteBatch.Begin();

            _states.Peek().Draw();

            SpriteBatch.End(_window);
            float time = drawTimer.ElapsedMilliseconds;
            drawTimer.Reset();
            _window.Display();
            loops++;
            av += time;
            Console.WriteLine(time+"   av: "+av/loops);
        }
        VertexArray array = new VertexArray();

        public void Draw(Drawable  value)
        {
            _window.Draw(value);
        }
        /// <summary>
        /// Quit the game engine
        /// </summary>
        public void Quit()
        {

            _isRunning = false;
        }

       

    }
}