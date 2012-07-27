using System;
using SFML.Graphics;
using SFML.Window;

namespace PrototypingFramework.engine
{
    class GraphicComponent
    {
        
        private BatchedDrawable _batchInfo;
        private Transform _transform;

        public BatchedDrawable BatchInfo { get { return _batchInfo; } }

        public Vector2f Position
        {
            get
            {
                return _transform.TransformPoint(new Vector2f(0,0));
            }
        }
        internal void Move(Vector2f value)
        {
            this.Move(value.X, value.Y);
        }
        public void Move(float x, float y)
        {
            _transform.Translate(x, y);
        }
        public void Rotate(float angle)
        {
            _transform.Rotate(angle);
        }
        public void Scale(Vector2f factors)
        {
            _transform.Scale(factors);
        }
        public GraphicComponent(String texturePath)
        {
            _transform = new Transform();
            
            _batchInfo = new BatchedDrawable() { BatchTextureID = texturePath };
            _batchInfo.UpdateTexture();
         

        }

        public void UpdateTransform()
        {
            
            _batchInfo.UpdateTransform(_transform);
        }



       
    }
}
