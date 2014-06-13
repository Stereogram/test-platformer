using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World
{
    class Background : Drawable
    {
        private readonly List<Sprite> _sprites;
        public Background(params Texture[] t)
        {
            _sprites = new List<Sprite>();
            foreach(Texture s in t)
            {
                _sprites.Add(new Sprite(s));
                if(s.Size.X < 1366)
                {
                    Sprite a = new Sprite(s) {Position = new Vector2f(s.Size.X, 0)};
                    _sprites.Add(a);
                }
            }
        }

        public void Add(Texture t, int layer = int.MaxValue)
        {
            if (layer == int.MaxValue)
            {
                _sprites.Add(new Sprite(t));
            }
            else
            {
                _sprites.Insert(layer, new Sprite(t));
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach(Sprite s in _sprites)
            {
                target.Draw(s, states);
            }
        }
    }
}
