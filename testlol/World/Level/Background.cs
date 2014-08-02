using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World.Level
{
    class Background : Drawable
    {
        private readonly List<Sprite> _sprites;
        public Background(Texture t)
        {
            _sprites = new List<Sprite>();
            t.Repeated = true;
            var s = new Sprite(t) {TextureRect = new IntRect(0,0,2000,1000)};
            _sprites.Add(s);

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
