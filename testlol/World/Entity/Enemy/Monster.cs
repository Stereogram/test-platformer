using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using testlol.Util;

namespace testlol.World.Entity.Enemy
{
    public sealed class Monster : Entity, IUpdatable
    {
        public override FloatRect HitBox { get; protected set; }
        protected override Vector2i OffSet { get; set; }
        public override Vector2u Size { get; protected set; }

        public Monster(Texture t, Vector2u size, List<Animation> aList) : base(t, size, aList)//todo fix this shit.
        {
            Size = new Vector2u(32, 64);
            HitBox = new FloatRect(Position.X - 16, Position.Y, Size.X, Size.Y);
            OffSet = new Vector2i(16, 0);
            Position = new Vector2f(0, 0);
            Velocity = new Vector2f(250, 0);
        }

        
        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Sprite);
            target.Draw(HitBox.ToRectangleShape());
        }

        public void Update(Time dt)
        {
            
        }
    }
}
