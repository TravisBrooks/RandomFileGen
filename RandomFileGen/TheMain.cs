using System;
using System.IO;

namespace RandomFileGen
{
    internal class TheMain
    {
        static int Main(string[] args)
        {
            const string usageStr = "Usage: RandomFileGen <fileName> <fileSizeInBytes>";

            if (args.Length != 2)
            {
                Console.Error.WriteLine("Error: requires exactly 2 parameters.");
                Console.Error.WriteLine(usageStr);
                return 1;
            }

            var fileName = args[0];
            if (!long.TryParse(args[1], out long fileSizeInBytes))
            {
                Console.Error.WriteLine("Error: the second parameter must parse as a long numeric type.");
                Console.Error.WriteLine(usageStr);
                return 1;
            }

            if (fileSizeInBytes <= 0)
            {
                Console.Error.WriteLine("Error: the fileSizeInBytes parameter must be a positive number greater than 0.");
                Console.Error.WriteLine(usageStr);
                return 1;
            }

            long bytesWritten = 0;
            var rand = new Random();
            var bite = new byte[1];
            using (var fstream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var bufStream = new BufferedStream(fstream, 1024))
                {
                    while (bytesWritten < fileSizeInBytes)
                    {
                        rand.NextBytes(bite);
                        bufStream.Write(bite);
                        bytesWritten += bite.Length;
                    }
                    bufStream.Flush();
                }
            }

            return 0;
        }
    }
}
