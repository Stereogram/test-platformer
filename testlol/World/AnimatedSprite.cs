using System;
using NetEXT.Animation;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;

namespace testlol.World
{
    public class AnimatedSprite: IUpdatable, Drawable
    {
        public enum State
        {
            Idle,
            Walking,
            Jumping
        }

        readonly Animator<Sprite, State> _animator;
        readonly AnimatedObject<Sprite> _animatedObject;
        private readonly Sprite _sprite;

        public AnimatedSprite(Texture texture)
        {
            _sprite = new Sprite(texture);
            _animatedObject = new AnimatedObject<Sprite>(_sprite);

            _animator = new Animator<Sprite, State>();
            FrameAnimation<Sprite> idleAnimation = new FrameAnimation<Sprite>();
            for (int i = 0; i < 8; i++)
            {
                idleAnimation.AddFrame(1, new IntRect(i*64, 0, 64, 64));
            }
            idleAnimation.AddFrame(1, new IntRect(0 * 64, 64, 64, 64));
            idleAnimation.AddFrame(1, new IntRect(1 * 64, 64, 64, 64));
            _animator.AddAnimation(State.Idle,idleAnimation,Time.FromSeconds(1));
            _animator.PlayAnimation(State.Idle, true);

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            _animator.Animate(_animatedObject);
            target.Draw(_sprite);
        }

        public void Update(Time dt)
        {
            _animator.Update(dt);
        }

        public Vector2f Position
        {
            get { return _sprite.Position; }
            set { _sprite.Position = value; }
        }
    }
}
