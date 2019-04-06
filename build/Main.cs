using System;
using System.IO;
using System.IO.Compression;

namespace ReadyForRelease
{
    class ReadyForRelease
    {

        static string BasePath = "release/";
        static string RoR2Path = BasePath + "release/";
        public static void Main()
        {
            if (Directory.Exists("release"))
                Directory.Delete("release", true);

            Directory.CreateDirectory(RoR2Path);
            File.Copy("icon.png", BasePath + "icon.png");
            File.Copy("manifest.json", BasePath + "manifest.json");
            File.Copy("LICENSE", BasePath + "LICENSE");
            File.Copy("README.md", BasePath + "README.md");
            File.Copy("HOW TO INSTALL.txt", BasePath + "HOW TO INSTALL.txt");

            foreach (var file in File.ReadAllLines("to_release.txt"))
            {
                var destfile = file;
                if (file.StartsWith("bin/"))
                    destfile = file.Substring(4);
                Directory.CreateDirectory(RoR2Path + Path.GetDirectoryName(destfile));
                File.Copy(file, RoR2Path + destfile);
            }

            if (File.Exists("release.zip"))
                File.Delete("release.zip");

            ZipFile.CreateFromDirectory("release", "release.zip");

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}