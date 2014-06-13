using System;
using NetEXT.TimeFunctions;
using SFML.Window;
using SFML.Graphics;

namespace testlol
{
    class Platform : Entity
    {

        public Platform(Texture t, Vector2f p):base(t)
        {
            Position = p;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Sprite, states);
            if (DrawBoundingBox)
            {
                target.Draw(BoundingBox, states);
            }
        }

    }
}
