using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrototypingFramework.engine;

namespace PrototypingFramework.protoypes
{
   public abstract class IPrototype:IGameState
    {

        public abstract String Name { get; }

    }
}
