using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Physics2DDotNet;

using SFML.Graphics;
using Physics2DDotNet.Shapes;
using Physics2DDotNet.PhysicsLogics;
using AdvanceMath;

namespace PrototypingFramework.protoypes.physic_prototype
{
    class physic_prototype : IPrototype
    {
        PhysicsEngine world;
        RectangleShape rs;
        RectangleShape floor;
        Body body2;
        public override string Name
        {
            get { return "Physic Prototype"; }
        }

        public override void Init(PrototypingFramework.engine.GameEngine game)
        {
            base.Init(game);
            world = new PhysicsEngine();
            world.BroadPhase = new Physics2DDotNet.Detectors.SelectiveSweepDetector();
            world.Solver = new Physics2DDotNet.Solvers.SequentialImpulsesSolver();
           

            PhysicsTimer timer = new PhysicsTimer(world.Update, .01f);
            timer.IsRunning = true;


            rs = new RectangleShape(new SFML.Window.Vector2f(10, 10));
            rs.FillColor = Color.Black;
            rs.Position = new SFML.Window.Vector2f(10, 5);

            Coefficients coffecients = new Coefficients(/*restitution*/1, /*friction*/.5f);
            IShape shape2 = new PolygonShape(VertexHelper.CreateRectangle(10, 20), 3);
             body2 = new Body(new PhysicsState(new ALVector2D(0,10,5)), shape2, 5, coffecients, new Lifespan());
           
            world.AddBody(body2);

            PhysicsLogic logGravity;

            logGravity = (PhysicsLogic)new GravityField(new Vector2D(0f, 200f), new Lifespan());
            //pretty basic, create a downward force

            world.AddLogic(logGravity);

            Body bdyFloor;
            PhysicsState flrState;
            PolygonShape flrShape;
            Coefficients flrCoff;
            Lifespan flrLife;

            flrState = new PhysicsState(new ALVector2D((float)0.0, 0, (float)_game.Window.Size.Y-64));
            //create the state, centering the x-axis on screen and bottom of the y-axis

            flrShape = new PolygonShape(VertexHelper.CreateRectangle(_game.Window.Size.X, 64), 2);
            //create form.widthX64 rectangle (sq) with grid spacing at 2

            flrCoff = new Coefficients(0.5f, 0.4f, 0.4f);
            //might require tuning to your liking...

            flrLife = new Lifespan();
            //forever and ever

            bdyFloor = new Body(flrState, flrShape, float.PositiveInfinity, flrCoff, flrLife);
            //never ending mass means it isn't going to move on impact

            bdyFloor.IgnoresGravity = true;
            //make sure the floor stays

            world.AddBody(bdyFloor);

            floor = new RectangleShape(new SFML.Window.Vector2f( _game.Window.Size.X,64));
            floor.Position = new SFML.Window.Vector2f(0, _game.Window.Size.Y - 64);
            floor.FillColor = Color.Red;

        }
        public override void BindEvents()
        {
            
        }

        public override void UnbindEvents()
        {
            
        }

        public override void Update(float gameTime)
        {
            rs.Position = new SFML.Window.Vector2f(body2.State.Position.Linear.X, body2.State.Position.Linear.Y);
            
        }

        public override void Draw()
        {
            _game.Draw(rs);
            _game.Draw(floor);
        }
    }
}