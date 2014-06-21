using SFML.Graphics;
using SFML.Window;

namespace testlol.World
{
    class Platform : Entity
    {

        public Platform(Texture t, Vector2f p):base(t, null)
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
