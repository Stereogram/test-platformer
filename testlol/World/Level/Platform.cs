using SFML.Graphics;
using SFML.Window;

namespace testlol.World.Level
{
    class Platform : Entity.Entity
    {

        public Platform(Texture t, Vector2f p):base(t,new Vector2u(100,50),null)
        {
            Position = p;
        }

        public override FloatRect HitBox { get; protected set; }
        protected override Vector2i OffSet { get; set; }

        public override Vector2u Size { get; protected set; }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Sprite, states);
        }

    }
}
