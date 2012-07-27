using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using PrototypingFramework.engine;
using SFML.Window;

namespace PrototypingFramework.protoypes.batched_protype
{
    class batched_protype:IPrototype
    {
        int count = 10000;
        Random r = new Random();
        GraphicComponent[] c;
        public override string Name
        {
            get {return  "batched_prototype"; }
        }
        public override void Init(PrototypingFramework.engine.GameEngine game)
        {
            base.Init(game);
            Texture tex = new Texture("content/greenTile.png");
            c = new GraphicComponent[count];
            Vector2f mid=new Vector2f(_game.Window.Size.X/2,_game.Window.Size.Y/2);
            for (int i = 0; i < count-1; i++)
            {
                c[i] = new GraphicComponent("content/greenTile.png",tex);
                c[i].Move(mid);
                c[i].Scale(new Vector2f(32,32));
            }
            c[count - 1] = new GraphicComponent("content/object.png",new Texture("content/object.png"));
            c[count - 1].Move(mid);
            c[count - 1].Scale(new Vector2f(32, 32));
        }
        public override void BindEvents()
        {
            
        }

        public override void UnbindEvents()
        {
           
        }

        public override void Update(float gameTime)
        {
            for (int i = 0; i < count; i++)
            {
                c[i].Move((r.Next(4) - 2) * (gameTime / 1000), (r.Next(4) - 2) * (gameTime / 1000));
                c[i].Move(1*(gameTime/1000),0);
                c[i].Rotate(1);
                c[i].UpdateTransform();
            }
        }

        public override void Draw()
        {
            for (int i = 0; i < count; i++)
            {
                SpriteBatch.Draw(c[i]);
            }
        }
    }
}
