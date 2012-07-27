using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypingFramework.engine
{
    public abstract class IGameState
    {

        public virtual void Init(GameEngine game) { _game = game; BindEvents(); }
        public virtual void Cleanup() { UnbindEvents(); }

         public abstract void BindEvents();
        public  abstract void UnbindEvents();

        public virtual void Pause() { this.isRunning = false; UnbindEvents(); }
        public virtual void Resume() { this.isRunning = true; BindEvents(); }

        public abstract void Update( float gameTime);
        public abstract void Draw();

        protected bool isRunning;
        protected GameEngine _game;

    }
}
