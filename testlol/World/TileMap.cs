using System;
using SFML.Graphics;
using SFML.Window;
using testlol.Util;

namespace testlol.World
{
    public class TileMap : Transformable, Drawable
    {
        //private readonly VertexArray _vertexArray = new VertexArray(PrimitiveType.Quads);
        private readonly Texture _texture;
        private readonly int _tileSize;
        private readonly Map _map;
        private readonly MapRenderer _mapRenderer;

        public TileMap(Texture texture, int tileSize, Map map)
        {
            _mapRenderer = new MapRenderer(texture, Provider, tileSize);
            _texture = texture;
            _tileSize = tileSize;
            _map = map;
            //Construct();
        }

        private void Provider(int x, int y, out Color color, out IntRect rec)
        {
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
            states.Transform = Transform;
            states.Texture = _texture;
            target.Draw(_mapRenderer, states);
        }
    }
}
