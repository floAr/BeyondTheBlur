using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace PrototypingFramework.engine
{
    /// <summary>
    /// Class to draw textures from an atlas and collect draw calls, so just one acces to the GPU is needed
    /// TODO:
    /// - Add multi pagin, to create a ned atlas page if the first one cant contain the texture
    /// - Support alpha masking when rendering to the atlas
    ///     Now a texture like this: 
    ///     
    ///     #
    ///     #
    ///     ##
    ///     
    ///     Will take up a space like this
    ///     
    ///     ##
    ///     ##
    ///     ##
    ///     
    ///     By checking which fields are actualley used we can get much more irregular texture into one atlas page
    /// - Think about mip mapping when rendering onto the atlas
    ///
    /// </summary>
    class SpriteBatch
    {
        /// <summary>
        /// Defines how many tiles we want to have horizontal
        /// </summary>
        static int _tileCountX;
        /// <summary>
        /// Defines how many tiles we want to have vertical
        /// </summary>
        static int _tileCountY;
        /// <summary>
        /// Width of a single tile in pixels
        /// </summary>
        static int _tileWidth;
        /// <summary>
        /// Height of a singel tile in pixels
        /// </summary>
        static int _tileHeight;

        /// <summary>
        /// The atlas we save our registered textures in
        /// </summary>
        static RenderTexture _atlas;
        /// <summary>
        /// A map to indicate if a given tile is already used (1) or if its free (0)
        /// </summary>
        static int[,] _index;
        /// <summary>
        /// The vertex array we use to draw everything
        /// </summary>
        static Vertex[] _batch;
        /// <summary>
        /// A list to save all the objects to draw in one call
        /// </summary>
        static List<BatchedDrawable> _drawList;
        /// <summary>
        /// The state we use to pass the texture to the gpu
        /// </summary>
        static RenderStates state;
        /// <summary>
        /// counter to indicate if we need to rescale our vertex array
        /// </summary>
        static int _lastCount=-1;

        /// <summary>
        /// Initialize the sprite batch, has to be called before drawing with it
        /// </summary>
        /// <param name="tileCountX">Number of horizontal tiles</param>
        /// <param name="tileCountY">Number of vertical tiles</param>
        /// <param name="tileWidth">Pixel based width of one tile</param>
        /// <param name="tileHeight">Pixel based height of one tile</param>
        public static void Init(int tileCountX,int tileCountY,int tileWidth, int tileHeight)
        {
           //create list for drawable objects
            _drawList = new List<BatchedDrawable>();
            //copy parameters
            _tileHeight = tileHeight;
            _tileWidth = tileWidth;
            _tileCountX = tileCountX;
            _tileCountY = tileCountY;
            //create index with one value for every tile
            _index=new int[_tileCountX,_tileCountY];
            //create the render texture
            _atlas = new RenderTexture((uint)(_tileCountX * _tileWidth), (uint)(_tileCountY * _tileHeight));
        }

        /// <summary>
        /// Call before draw. Set blend mode to alpha blend and clears our list of objects
        /// </summary>
        public static void Begin()
        {
            //set default blend mode
            state.BlendMode = BlendMode.Alpha;
            //reset list
            _drawList.Clear();
        }

        /// <summary>
        /// Call before draw. Set blend mode  and clears our list of objects
        /// </summary>
        /// <param name="blendMode">Blend mode for this draw call</param>
        public static void Begin(BlendMode blendMode)
        {
            //set blend mode
            state.BlendMode = blendMode;
            //reset list
            _drawList.Clear();
        }

        /// <summary>
        /// Queue the graphic component to draw it when SpriteBatch.End is called
        /// </summary>
        /// <param name="value">Graphic object we want to draw</param>
        public static void Draw(GraphicComponent value)
        {
            //just add the object to the list
            _drawList.Add(value.BatchInfo);
        }

        /// <summary>
        /// End the queueing of objects and draw to the render window
        /// </summary>
        /// <param name="_window">Renderwindow we draw on</param>
        public static void End(RenderWindow _window)
        {
            //set the atlas as used texture
                state.Texture = _atlas.Texture;
            //copy list (not neede now, we will use this later with multiple pages
                List<BatchedDrawable> _pageBatch = _drawList;
            //check if the object count has changed...
                if (_lastCount != _pageBatch.Count)
                {
                    //...and rescale the batch if so
                    _batch = new Vertex[_pageBatch.Count * 4];
                    _lastCount = _pageBatch.Count;
                }
            //loop through every object...
                for(uint i=0;i<_pageBatch.Count;++i)
                {
                    //... for each corner of the object..
                    for (uint count = 0; count < 4; ++count)
                    {
                        //create a vertex
                        Vertex v;
                        //set the corresponding position from the object
                        v.Position = _pageBatch[(int)i].Position[count];
                        //set default color
                        v.Color = Color.White;
                        //point the vertex to the texture location
                        v.TexCoords = _pageBatch[(int)i].TexCoords[count];

                        //write the vertex into our batch
                        _batch[i * 4+count] = v;
                    }
                }
            //draw the filled batch with our atlas as texture (through state)
                _window.Draw(_batch, PrimitiveType.Quads, state);
        }

        /// <summary>
        /// Copies a texture into the atlas and returns the textur coordinates in the atlas
        /// </summary>
        /// <param name="tex">Texture to add into the atlas</param>
        /// <returns>Texture coordinates in the texture atlas</returns>
        public static Vector2f[] PlaceTexture(Texture tex)
        {
            //vectors, which point to the edged of the texture in the atlas
            Vector2f[] result = new Vector2f[4];
            //calculate how many tiles the texture will occupy (only rectangle shapes right now)
            int tileW = (int)Math.Floor((double)tex.Size.X / _tileWidth);
            int tileH = (int)Math.Floor((double)tex.Size.Y / _tileHeight);
            //loop through all the tiles in the atlas
            for (int x = 0; x < _tileCountX; ++x)
            {
                for (int y = 0; y < _tileCountY; ++y)
                {
                    //if current tile is free
                    if (_index[x, y] == 0)
                    {
                        //tempory bool to see if we have enough space
                        bool fit = true;
                        //loop through the rectangle shape of the object
                        for (int oX = 0; oX < tileW; oX++)
                        {
                            for (int oY = 0; oY < tileH; oY++)
                            {
                                //if any of the tiles we want to use is already blocked...
                                if (_index[x + oX, y + oY] != 0)
                                    //set fit to false
                                    fit = false;
                            }
                        }
                        //if we found a place for our texture
                        if (fit)
                        {
                            //create a render
                            RectangleShape render = new RectangleShape();
                            //scale it according to the texture (maybe add mip mapping here)
                            render.Size = new Vector2f(tex.Size.X, tex.Size.Y);
                            //assing the texture
                            render.Texture = tex;
                            //locate it at the point we found
                            render.Position = new Vector2f(_tileWidth * x, _tileHeight * y);
                            //render it into the atlas
                            _atlas.Draw(render);
                            //finalize the rendering process
                            _atlas.Display();
                            //calculate the bound for the texture we just rendered
                            result[0] = new Vector2f(_tileWidth * x, _tileHeight * y);
                            result[1] = new Vector2f(_tileWidth * x, _tileHeight * y + tex.Size.Y);
                            result[2] = new Vector2f(_tileWidth * x + tex.Size.X, _tileHeight * y + tex.Size.Y);
                            result[3] = new Vector2f(_tileWidth * x + tex.Size.X, _tileHeight * y);
                            //for each tile we just used
                            for (int oX = 0; oX < tileW; oX++)
                            {
                                for (int oY = 0; oY < tileH; oY++)
                                {
                                    //mark this tile as occupied
                                    _index[x + oX, y + oY] = 1;
                                }
                            }
                            //return the vectors and break the for loops
                            return result;
                        }

                    }
                }
            }
            //return default results.
            return result;
        }
    }
}
