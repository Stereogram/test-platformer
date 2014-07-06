using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using testlol.Util;

namespace testlol.World.Level
{
    public class TileMap : Drawable
    {
        //private readonly VertexArray _vertexArray = new VertexArray(PrimitiveType.Quads);
        private readonly Texture _texture;
        private readonly int _tileSize;
        private Map _map;
        private readonly MapRenderer _mapRenderer;
        private Vector2f _topLeft;

        private readonly List<FloatRect> _collidables = new List<FloatRect>();
        public List<FloatRect> Collidables { get { return _collidables; } } 

        public TileMap(Texture texture, int tileSize, Map map)
        {
            _mapRenderer = new MapRenderer(texture, Provider, tileSize);
            _texture = texture;
            _tileSize = tileSize;
            _map = map;
            //Construct();
        }

        public int this[int a, int b]
        {
            get { return _map[a, b]; }
            set { _map[a, b] = value; _mapRenderer.RefreshScreen(); }
        }

        private void Provider(int x, int y, out Color color, out IntRect rec)
        {
            if (x >= _map.X || y >= _map.Y)
            {
                color = Color.White;
                rec = new IntRect(0,0,32,32);
                return;
            }
            int tu = _map[y,x] % ((int)(_texture.Size.X / _tileSize));
            int tv = _map[y,x] / ((int)(_texture.Size.X / _tileSize));
            color = Color.White;
            rec.Top = tv*_tileSize;
            rec.Left = tu*_tileSize;
            rec.Width = _tileSize;
            rec.Height = _tileSize;
        }

        //private void Construct()
        //{
        //    for (int i = 0; i < _map.Y; i++)
        //    {
        //        for (int j = 0; j < _map.X; j++)
        //        {
        //            int tile = _map[i, j];
        //            int tu = tile % ((int)(_texture.Size.X / _tileSize));
        //            int tv = tile / ((int)(_texture.Size.X / _tileSize));

        //            Vertex quad0 = new Vertex(new Vector2f( j * _tileSize,       i * _tileSize));
        //            Vertex quad1 = new Vertex(new Vector2f((j + 1) * _tileSize, i * _tileSize));
        //            Vertex quad2 = new Vertex(new Vector2f((j + 1) * _tileSize, (i + 1) * _tileSize));
        //            Vertex quad3 = new Vertex(new Vector2f( j * _tileSize,      (i + 1) * _tileSize));

        //            quad0.TexCoords = new Vector2f(tu * _tileSize, tv * _tileSize);
        //            quad1.TexCoords = new Vector2f((tu + 1) * _tileSize, tv * _tileSize);
        //            quad2.TexCoords = new Vector2f((tu + 1) * _tileSize, (tv + 1) * _tileSize);
        //            quad3.TexCoords = new Vector2f(tu * _tileSize, (tv + 1) * _tileSize);

        //            _vertexArray.Append(quad0);
        //            _vertexArray.Append(quad1);
        //            _vertexArray.Append(quad2);
        //            _vertexArray.Append(quad3);
        //        }
        //    }
        //}

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Texture = _texture;
            target.Draw(_mapRenderer, states);
            View view = target.GetView();
            Vector2f t = view.Center - (Vector2f) (Game.Size/2);
            if ((Vector2i)_topLeft/32 != (Vector2i)t/32)
            {
                _topLeft = t;
                _collidables.Clear();
                for (int i = (int) (_topLeft.Y/32); i < (int) (_topLeft.Y + Game.Size.Y)/32; i++)
                {
                    for (int j = (int)(_topLeft.X / 32); j < (int)(_topLeft.X + Game.Size.X) / 32; j++)
                    {
                        if (i < 0 || j < 0 || i>22)
                            continue;
                        if (_map[i, j] != 0)
                        {
                            _collidables.Add(new FloatRect(j*32,i*32,32,32));
                        }
                    }
                }
                //Console.WriteLine(_collidables.Count);
            }
        }
    }
}
