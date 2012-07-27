using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace PrototypingFramework.engine
{
    static class ContentManager
    {
        static Dictionary<String, Texture> _textures;
        

        static Texture LoadTexture(string filePath)
        {
            if (_textures.Keys.Contains(filePath))
                return _textures[filePath];
            Texture t = new Texture(filePath);
            _textures.Add(filePath, t);
            return t;
        }

        static void PreLoadTexture(string filePath)
        {
            if (_textures.Keys.Contains(filePath))
                return;
            Texture t = new Texture(filePath);
            _textures.Add(filePath, t);
            return;
        }




    }
}
