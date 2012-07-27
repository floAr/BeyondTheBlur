using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypingFramework.protoypes.simple_prototype
{
    class simple_prototype:IPrototype
    {
        public override string Name
        {
            get { return "Simple Prototype"; }
        }





        public override void Cleanup()
        {
            throw new NotImplementedException();
        }

        public override void Update(float gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void BindEvents()
        {
            throw new NotImplementedException();
        }

        public override void UnbindEvents()
        {
            throw new NotImplementedException();
        }
    }
}
