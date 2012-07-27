using System;
using SFML.Graphics;
using SFML.Window;

namespace PrototypingFramework.engine
{
    /// <summary>
    /// Structure, which contains the information for an object to be drawn in a batch
    /// </summary>
    struct  BatchedDrawable
    {
        /// <summary>
        /// Id for the texture to use (the patch)
        /// </summary>
        public String BatchTextureID;
        /// <summary>
        /// Positions of the four corners
        /// </summary>
        public Vector2f[] Position;
        /// <summary>
        /// Coordinates of the corners to lokate the texture in the atlas
        /// </summary>
        public Vector2f[] TexCoords;
        /// <summary>
        /// Default corners which we use to translate (maybe store them elsewhere later)
        /// </summary>
        static  Vector2f[] Corners=new Vector2f[]{new Vector2f(0,0),new Vector2f(0,1),new Vector2f(1,1),new Vector2f(1,0)};

        /// <summary>
        /// Calculate the corner positions based on the transform of the parent object
        /// </summary>
        /// <param name="parentTransform">Transformation of the parent</param>
        public void UpdateTransform(Transform parentTransform)
        {
            //init array if its still uninitialized
            if (Position == null)
                Position = new Vector2f[4];
            //for every corner
            for(int i=0;i<4;++i)
            {
                //translate the base corner with the parents transform
                Position[i]=parentTransform.TransformPoint(BatchedDrawable.Corners[i]);
            }
        }
        /// <summary>
        /// Genererate the texture coordinates from based on the texture ID.
        /// </summary>
        public void UpdateTexture()
        {
            //ask the content manager for the texture coordinates and load the texture if it is new
            TexCoords = ContentManager.LoadBatchedTexture(BatchTextureID);
        }

    }
}
