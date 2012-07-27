using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypingFramework.engine
{
    /// <summary>
    /// Class to provide a simple interface for game states
    /// </summary>
    public abstract class IGameState
    {
        /// <summary>
        /// Initialize the game state and assing the game, which handles it.
        /// </summary>
        /// <param name="game">Parent game</param>
        public virtual void Init(GameEngine game) { _game = game; BindEvents(); }

        /// <summary>
        /// Clear the state and unregister all the events
        /// </summary>
        public virtual void Cleanup() { UnbindEvents(); }

        /// <summary>
        /// Assign the events for this game state here
        /// </summary>
        public abstract void BindEvents();

        /// <summary>
        /// Unregister the events from BindEvents here
        /// </summary>
        public abstract void UnbindEvents();

        /// <summary>
        /// Pauses the game state and remove the event listener
        /// </summary>
        public virtual void Pause() { this.isRunning = false; UnbindEvents(); }

        /// <summary>
        /// Resumes the game state and registers the events defined in BindEvents
        /// </summary>
        public virtual void Resume() { this.isRunning = true; BindEvents(); }

        /// <summary>
        /// Update loop for the game state
        /// </summary>
        /// <param name="gameTime">Time since last Update call</param>
        public abstract void Update(float gameTime);

        /// <summary>
        /// Draw loop for the game state
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Boolean which indicates if this game state is currenly active and running
        /// </summary>
        protected bool isRunning;
        /// <summary>
        /// Game instance, which holds this game state
        /// </summary>
        protected GameEngine _game;

    }
}
