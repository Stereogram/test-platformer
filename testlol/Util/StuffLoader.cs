using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Ionic.Zip;
using SFML.Graphics;

namespace testlol.Util
{
    public class StuffLoader
    {
        private readonly string _working = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        private readonly string[] _files;

        public StuffLoader()
        {
            _files = Directory.GetFiles(_working + @"\assets", "*", SearchOption.AllDirectories);
            Load();
        }

        public void Zip()
        {
            using (ZipFile zip = new ZipFile("assets.stuff"))
            {
                foreach (string file in _files)
                {
                    zip.AddFile(file.Substring(_working.Length + 1));
                }
                zip.Save();
            }
        }

        public void Load()
        {
            foreach (var file in _files)
            {
                switch (Path.GetExtension(file))
                {
                    case ".png":
                        Game.Textures[file.Substring(_working.Length+1)] = new Texture(file);
                        break;
                    case ".ani":
                        Game.Animations[file.Substring(_working.Length+1)] = AnimatedSprite.ReadAnimations(file);
                        break;
                }
            }
        }



    }
}
