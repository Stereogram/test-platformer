using SFML.Graphics;
using SFML.Window;

namespace testlol.World.Level
{
    class Platform : Entity.Entity
    {

        public Platform(Texture t, Vector2f p):base(t, null)
        {
            Position = p;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Sprite, states);
        }

    }
}
