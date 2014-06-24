using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Ionic.Zip;

namespace testlol.Util
{
    public class StuffLoader
    {
        private readonly string _working = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        private readonly string[] _files;

        public StuffLoader()
        {
            _files = Directory.GetFiles(_working + @"\assets", "*", SearchOption.AllDirectories);
        }

        public void Zip()
        {
            

            using (ZipFile zip = new ZipFile("assets.zip"))
            {
                foreach (string file in _files)
                {
                    zip.AddFile(file.Substring(_working.Length + 1));
                }
                zip.Save();
            }
        }


    }
}
