using System;
using System.Collections.Generic;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World
{
    class Player : Entity, IUpdatable
    {
        private const float _gravity = 1;
        private readonly CallbackTimer test;

        public Player(Texture t, List<Tuple<string, int>> a):base(t,a)
        {
            //DrawBoundingBox = true;
            Position = new Vector2f(500, 500);
            Velocity = new Vector2f(250, 200);

            test = new CallbackTimer();

            test.Connect(c =>
            {
                Vector2f vel = Velocity;
                vel.Y = 200;
                Velocity = vel;
            });
            
            

        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Sprite, states);
            if(DrawBoundingBox)
            {
                target.Draw(BoundingBox, states);
            }
        }

        public void Update(Time dt)
        {
            Sprite.Update(dt);//animation
            test.Update();
            Vector2f pos = Position;

            pos.X += Direction * Velocity.X * (float) dt.Seconds;
            pos.Y += _gravity  * Velocity.Y * (float) dt.Seconds;

            Position = pos;

            if(Position.Y >= 768-Size.Y)
            {
                Position = new Vector2f(Position.X, 768-Size.Y);
                Velocity = new Vector2f(Velocity.X, 0);
                Jumping = false;
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
                vel.Y = -200;
                Velocity = vel;
                test.Reset(Time.FromSeconds(1));
                test.Start();
            }
            
        }

        public void Shoot()
        {
            
        }

    }
}
