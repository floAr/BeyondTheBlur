using System;
using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;
using SFML.Window;

namespace PrototypingFramework.engine
{
    public static class ContentManager
    {


        public static Dictionary<String, Texture> _textures = new Dictionary<string, Texture>();
        public static Dictionary<String, Vector2f[]> _batchedTextures = new Dictionary<string, Vector2f[]>();

        public static Texture LoadTexture(string filePath)
        {
            if (_textures.Keys.Contains(filePath))
                return _textures[filePath];
            Texture t = new Texture(filePath);
            _textures.Add(filePath, t);
            return t;
        }

        public static void PreLoadTexture(string filePath)
        {
            if (_textures.Keys.Contains(filePath))
                return;
            Texture t = new Texture(filePath);
            _textures.Add(filePath, t);
            return;
        }

        public static Vector2f[] LoadBatchedTexture(string filePath)
        {
            if(_batchedTextures.Keys.Contains(filePath))
                return _batchedTextures[filePath];
            Vector2f[] coords;

            if (_textures.Keys.Contains(filePath))
            {
                coords= SpriteBatch.PlaceTexture(_textures[filePath]);
            }
                
            Texture t = new Texture(filePath);
            _textures.Add(filePath, t);

            coords = SpriteBatch.PlaceTexture(t);

            _batchedTextures.Add(filePath, coords);

            return coords;
            
        }

        public static void PreLoadBatchedTexture(string filePath)
        {
            if (_batchedTextures.Keys.Contains(filePath))
                return ;
            Vector2f[] coords;

            if (_textures.Keys.Contains(filePath))
            {
                coords = SpriteBatch.PlaceTexture(_textures[filePath]);
            }

            Texture t = new Texture(filePath);
            _textures.Add(filePath, t);

            coords = SpriteBatch.PlaceTexture(t);

            _batchedTextures.Add(filePath, coords);

            return;

        }


    }
}
