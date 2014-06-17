using SFML.Graphics;
using SFML.Window;

namespace testlol.World
{
    public class TileMap : Transformable, Drawable
    {
        private readonly VertexArray _vertexArray = new VertexArray(PrimitiveType.Quads);
        private readonly Texture _texture;
        private readonly Vector2u _tileSize;
        private readonly Map _map;

        public TileMap(Texture texture, Vector2u tileSize, Map map)
        {
            _texture = texture;
            _tileSize = tileSize;
            _map = map;
            Construct();
        }

        private void Construct()
        {
            for (int i = 0; i < _map.Y; i++)
            {
                for (int j = 0; j < _map.X; j++)
                {
                    int tile = _map[i, j];
                    int tu = tile % ((int)(_texture.Size.X / _tileSize.X));
                    int tv = tile / ((int)(_texture.Size.X / _tileSize.X));

                    Vertex quad0 = new Vertex(new Vector2f( j * _tileSize.X,       i * _tileSize.Y));
                    Vertex quad1 = new Vertex(new Vector2f((j + 1) * _tileSize.X,  i * _tileSize.Y));
                    Vertex quad2 = new Vertex(new Vector2f((j + 1) * _tileSize.X, (i + 1) * _tileSize.Y));
                    Vertex quad3 = new Vertex(new Vector2f( j * _tileSize.X,      (i + 1) * _tileSize.Y));

                    quad0.TexCoords = new Vector2f(tu * _tileSize.X, tv * _tileSize.Y);
                    quad1.TexCoords = new Vector2f((tu + 1) * _tileSize.X, tv * _tileSize.Y);
                    quad2.TexCoords = new Vector2f((tu + 1) * _tileSize.X, (tv + 1) * _tileSize.Y);
                    quad3.TexCoords = new Vector2f(tu * _tileSize.X, (tv + 1) * _tileSize.Y);

                    _vertexArray.Append(quad0);
                    _vertexArray.Append(quad1);
                    _vertexArray.Append(quad2);
                    _vertexArray.Append(quad3);
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform = Transform;
            states.Texture = _texture;
            target.Draw(_vertexArray, states);
        }
    }
}
