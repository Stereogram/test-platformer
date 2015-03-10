using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetEXT.Animation;
using SFML.Graphics;
using SFML.System;
using testlol.World.Entity;

namespace testlol.Util
{
    public class AnimatedSprite: IUpdatable, Drawable
    {
        public bool Facing { get; set; }

        private readonly List<Animation> _animationList;
        private readonly Animator<Sprite, string> _animator;
        private readonly AnimatedObject<Sprite> _animatedObject;
        private readonly Sprite _sprite;
        private readonly Vector2u _tSize;
        private readonly Vector2u _size;

        public Vector2f Position
        {
            get { return _sprite.Position; }
            set { _sprite.Position = value; }
        }

        public AnimatedSprite(Texture texture, Vector2u size, List<Animation> aList = null)
        {
            _sprite = new Sprite(texture);
            _size = size;
            _sprite.Origin = new Vector2f(32,32);
            if (aList == null) return;
            _animationList = aList;
            
            _animatedObject = new AnimatedObject<Sprite>(_sprite);
            _tSize = texture.Size;
            _animator = new Animator<Sprite, string>();
            AddFrames();
            _animator.PlayAnimation(aList[0].Name, true);
        }

        private void AddFrames()
        {
            int total = 0;
            foreach (Animation anim in _animationList)
            {
                FrameAnimation<Sprite> temp = new FrameAnimation<Sprite>();
                for (int i = 0; i < anim.Frames; i++, total++)
                {
                    int tu = (int) ((total % (_tSize.X / _size.Y)) * _size.X);
                    int tv = (int) ((total / (_tSize.Y / _size.Y)) * _size.Y);
                    temp.AddFrame(1, new IntRect(tu, tv, (int)_size.X, (int)_size.Y));
                }
                _animator.AddAnimation(anim.Name,temp,Time.FromSeconds(0.5f));
            }
        }

        public void Play(string s, bool b)
        {
            _animator.PlayAnimation(s, b);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if(_animatedObject != null)
                _animator.Animate(_animatedObject);
            target.Draw(_sprite);
        }

        public void Update(Time dt)
        {
            if(_animatedObject != null)
                _animator.Update(dt);
            _sprite.Scale = new Vector2f(Facing ? 1 : -1, 1);
        }

        public void WriteAnimations(string s)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(s, FileMode.Create)))
            {
                foreach (Animation t in _animationList)
                {
                    sw.Write(t.Name + " " + t.Frames + '\n');
                }
                sw.Flush();
            }
        }

        public static List<Animation> ReadAnimations(string s)
        {
            return File.ReadAllLines(s).Select(line => line.Split(' ')).Select(a => new Animation(a[0], int.Parse(a[1]))).ToList();
        }

        public static List<Animation> ReadAnimations(Stream s)
        {
            var list = new List<Animation>();
            s.Position = 0;
            using (StreamReader sr = new StreamReader(s))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var a = line.Split(' ');
                    list.Add(new Animation(a[0],int.Parse(a[1])));
                }
            }
            return list;
        }
    }

    public struct Animation
    {
        public string Name { get; set; }
        public int Frames { get; set; }
        public Animation(string name, int frames) : this()
        {
            Name = name;
			Frames = frames;
        }
    }

}
