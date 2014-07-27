using System;
using System.Collections.Generic;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.Util;
using testlol.World.Entity.Projectile;

namespace testlol.World.Entity
{
    sealed class Player : Entity, IUpdatable, IShooter
    {
        private readonly Time _shootMax = Time.FromSeconds(0.5f);
        private Time _shootTime = Time.Zero;
        private const float _gravity = 10;
        public Projectiles Projectiles { get; private set; }
        public override FloatRect HitBox { get; protected set; }
        public override Vector2u Size { get; protected set; }
        protected override Vector2i OffSet { get; set; }
        private static readonly Texture _texure = Game.Textures[@"assets\player\megaman.png"];
        private static readonly List<Animation> _animations = Game.Animations[@"assets\player\megaman.ani"];
        public Player():base(_texure, new Vector2u(64,64), _animations)
        {
            Projectiles = new Projectiles();
            Size = new Vector2u(32,64);
            HitBox = new FloatRect(Position.X - 16, Position.Y, Size.X, Size.Y);
            OffSet = new Vector2i(16,0);
            Position = new Vector2f(0, 0);
            Velocity = new Vector2f(250, 0);
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Sprite, states);
            target.Draw(Projectiles);
#if DEBUG
            RectangleShape test = HitBox.ToRectangleShape();
            test.FillColor = Jumping ? new Color(255, 100, 100, 150) : new Color(255, 0, 0, 150);
            target.Draw(test);
#endif
        }

        public void Update(Time dt)
        {
            Sprite.Update(dt);//animation

            Projectiles.Update(dt);
            _shootTime += dt;

            Vector2f pos = Position;
            Vector2f vel = Velocity;

            vel.Y += _gravity;
            Velocity = vel;

            pos.X += Direction * Velocity.X * (float) dt.Seconds;
            pos.Y += Velocity.Y * (float) dt.Seconds;

            Position = pos;

            if(Position.Y >= 768-Size.Y)
            {
                Position = new Vector2f(Position.X, 768-Size.Y);
                Velocity = new Vector2f(Velocity.X, 0);
                if (Jumping)
                {
                    Velocity = new Vector2f(Velocity.X, 0);
                    Jumping = false;
                }
            }
            if(Position.X < 0)
            {
                Position = new Vector2f(0, Position.Y);
                //Velocity = new Vector2f(0, Velocity.Y);
            }
            else if(Position.X >= 1366 + Size.X*3)
            {
                Position = new Vector2f(1366 + Size.X*3, Position.Y);
                //Velocity = new Vector2f(0, Velocity.Y);
            }
            
        }

        public void Move(int dir)
        {
            Direction = dir;
        }

        public void Jump()
        {
            if (!Jumping)
            {
                Jumping = true;
                Vector2f vel = Velocity;
                vel.Y = -400;
                Velocity = vel;
            }
            
        }

        public void Shoot()
        {
            if (_shootTime >= _shootMax)
            {
                _shootTime = Time.Zero;
                Projectiles.Shoot<Bullet>(Position);
            }
        }

        
    }
}
