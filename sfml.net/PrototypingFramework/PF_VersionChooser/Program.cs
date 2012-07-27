using System;

namespace PF_VersionChooser
{
    class Program
    {
        static void Main(string[] args)
        {

            //create menue
            Console.WriteLine("Version to activate?");
            Console.Write("Version ");
            string version = Console.ReadLine();
            if (!System.IO.Directory.Exists(@version))
            {
                version = "0" + version;
                if (!System.IO.Directory.Exists(@version))
                {
                    Console.WriteLine("Version not found: " + version);
                    return;
                }
            }
            String[] lines = System.IO.File.ReadAllLines(@"PrototypingFramework.csproj");
            for (int i = 0; i < lines.Length; ++i)
            {
                if (lines[i].Contains("Compile"))
                {
                    if (!lines[i].Contains(version))
                       lines[i]= lines[i].Replace("Compile", "None");
                    continue;
                }
                if (lines[i].Contains("None"))
                {
                    if (lines[i].Contains(version))
                        lines[i] = lines[i].Replace("None", "Compile");
                    continue;
                }
            }
            System.IO.File.WriteAllLines(@"PrototypingFramework.csproj", lines);
            Console.WriteLine("Version " + version + " activated. Press ENTER to quit");
            Console.ReadLine();

                      }
          
        }

}