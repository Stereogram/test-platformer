using System;
using SFML.Graphics;
using SFML.Window;

namespace testlol.Util
{
    /// <summary>
    /// Functions that provides color/texture rectangle data from tile map (or other source)
    /// 
    /// from https://github.com/LaurentGomila/SFML/wiki/Source:-TileMap-Render
    /// </summary>
    public delegate void TileProvider(int x, int y, out Color color, out IntRect rec);

    /// <summary>
    /// Fast and universal renderer of tile maps
    /// </summary>
    public class MapRenderer : Drawable
    {
        private readonly float _tileSize;

        private int _height;
        private int _width;

        /// <summary>
        /// Points to the tile in the top left corner
        /// </summary>
        private Vector2i _offset;
        private Vertex[] _vertices;

        /// <summary>
        /// Provides Color and Texture Source from tile map
        /// </summary>
        private readonly TileProvider _provider;

        /// <summary>
        /// Holds spritesheet
        /// </summary>
        private readonly Texture _texture;

        /// <param name="texture">Spritesheet</param>
        /// <param name="provider">Accesor to tilemap data</param>
        /// <param name="tileSize">Size of one tile</param>
        public MapRenderer(Texture texture, TileProvider provider, float tileSize = 32)
        {
            _provider = provider;

            _tileSize = tileSize;

            _vertices = new Vertex[0];
            _texture = texture;

        }

        /// <summary>
        /// Redraws whole screen
        /// </summary>
        public void RefreshScreen()
        {
            RefreshLocal(0, 0, _width, _height);
        }

        private void RefreshLocal(int left, int top, int right, int bottom)
        {
            for (var y = top; y < bottom; y++)
                for (var x = left; x < right; x++)
                {
                    Refresh(x + _offset.X, y + _offset.Y);
                }
        }

        /// <summary>
        /// Ensures that vertex array has enough space
        /// </summary>
        /// <param name="v">Size of the visible area</param>
        private void SetSize(Vector2f v)
        {
            var w = (int)(v.X / _tileSize) + 2;
            var h = (int)(v.Y / _tileSize) + 2;
            if (w == _width && h == _height) return;
            
            _width = w;
            _height = h;
            
            _vertices = new Vertex[_width * _height * 4];
            RefreshScreen();
        }

        /// <summary>
        /// Sets offset
        /// </summary>
        /// <param name="v">World position of top left corner of the screen</param>
        private void SetCorner(Vector2f v)
        {
            var tile = GetTile(v);
            var dif = tile - _offset;
            if (dif.X == 0 && dif.Y == 0) return;
            _offset = tile;

            if (Math.Abs(dif.X) > _width / 4 || Math.Abs(dif.Y) > _height / 4)
            {
                //Refresh everything if difference is too big
                RefreshScreen();
                return;
            }

            //Refresh only tiles that appeared since last update

            if (dif.X > 0) RefreshLocal(_width - dif.X, 0, _width, _height);
            else RefreshLocal(0, 0, -dif.X, _height);

            if (dif.Y > 0) RefreshLocal(0, _height - dif.Y, _width, _height);
            else RefreshLocal(0, 0, _width, -dif.Y);
        }

        /// <summary>
        /// Transforms from world size to to tile that is under that position
        /// </summary>
        private Vector2i GetTile(Vector2f pos)
        {
            var x = (int)(pos.X / _tileSize);
            var y = (int)(pos.Y / _tileSize);
            //if (pos.X < 0) x--;
            //if (pos.Y < 0) y--;
            return new Vector2i(x, y);
        }

        /// <summary>
        /// Redraws one tile
        /// </summary>
        /// <param name="x">X coord of the tile</param>
        /// <param name="y">Y coord of the tile</param>
        public void Refresh(int x, int y)
        {
            if (x < _offset.X || x >= _offset.X + _width || y < _offset.Y || y >= _offset.Y + _height)
                return; //check if tile is visible
            if (x < 0 || y < 0)
                return;

            //vertices works like 2d ring buffer
            var vx = x % _width;
            var vy = y % _height;
            if (vx < 0) vx += _width;
            if (vy < 0) vy += _height;

            var index = (vx + vy * _width) * 4;
            var rec = new FloatRect(x * _tileSize, y * _tileSize, _tileSize, _tileSize);

            Color color;
            IntRect src;
            _provider(x, y, out color, out src);

            UpdateTexture(index, rec, src, color);
            
        }

        /// <summary>
        /// Inserts color and texture data into vertex array
        /// </summary>
        private unsafe void UpdateTexture(int index, FloatRect rec, IntRect src, Color color)
        {
            fixed (Vertex* fptr = _vertices)
            {
                //use pointers to avoid array bound checks (optimization)

                var ptr = fptr + index;

                ptr->Position.X = rec.Left;
                ptr->Position.Y = rec.Top;
                ptr->TexCoords.X = src.Left;
                ptr->TexCoords.Y = src.Top;
                ptr->Color = color;
                ptr++;

                ptr->Position.X = rec.Left + rec.Width;
                ptr->Position.Y = rec.Top;
                ptr->TexCoords.X = src.Left + src.Width;
                ptr->TexCoords.Y = src.Top;
                ptr->Color = color;
                ptr++;

                ptr->Position.X = rec.Left + rec.Width;
                ptr->Position.Y = rec.Top + rec.Height;
                ptr->TexCoords.X = src.Left + src.Width;
                ptr->TexCoords.Y = src.Top + src.Height;
                ptr->Color = color;
                ptr++;

                ptr->Position.X = rec.Left;
                ptr->Position.Y = rec.Top + rec.Height;
                ptr->TexCoords.X = src.Left;
                ptr->TexCoords.Y = src.Top + src.Height;
                ptr->Color = color;
            }
        }

        /// <summary>
        /// Update offset (based on Target's position) and draw it
        /// </summary>
        public void Draw(RenderTarget rt, RenderStates states)
        {
            var view = rt.GetView();
            states.Texture = _texture;
            SetSize(view.Size);
            SetCorner(view.Center - (Vector2f)(Game.Size/2));

            rt.Draw(_vertices, PrimitiveType.Quads, states);
        }
    }
}