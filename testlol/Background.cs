using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;


namespace testlol
{
    class Background : Drawable
    {
        private List<Sprite> sprites;
        public Background(params Texture[] t)
        {
            sprites = new List<Sprite>();
            foreach(Texture s in t)
            {
                sprites.Add(new Sprite(s));
                if(s.Size.X < 1366)
                {
                    Sprite a = new Sprite(s);
                    a.Position = new Vector2f(s.Size.X, 0);
                    sprites.Add(a);
                }
            }
        }

        public void Add(Texture t, int layer = int.MaxValue)
        {
            if (layer == int.MaxValue)
            {
                sprites.Add(new Sprite(t));
            }
            else
            {
                sprites.Insert(layer, new Sprite(t));
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach(Sprite s in sprites)
            {
                target.Draw(s, states);
            }
        }
    }
}
