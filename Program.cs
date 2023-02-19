using BotwIconConverter.Core;

namespace BotwIconConverter;

internal static class Program
{
    private static void Main()
    {
        WriteAsciiArt();
        ClearConverted();
            
        // Check if the icons folder exists.
        if (!Directory.Exists("icons"))
        {
            Console.WriteLine("icons directory does not exists. Press any key to close application...");
            Console.ReadKey();
            Environment.Exit(0);
        }
            
        // Decompress all the .sbitemico files.
        ConvertFiles("icons", "yaz0");

        // Add log that .sbitemico files have been converted
        // to .bfres and the conversion process of .bfres to .gtx will begin.
        ConversionLog("sbitemico", "bfres", "gtx");

        // Convert all .bfres files to .gtx files.
        ConvertFiles("converted/bfres", "bfres");

        // Add log that all files will be moved to the .gtx folder.
        MovingLog("gtx");
            
        // Moving all .gtx files to the correct folder.
        foreach (var file in Directory.GetFiles("converted/bfres"))
        {
            var fileName = Path.GetFileName(file);
            var gtxFile = fileName.Replace(".bfres", ".gtx");
                
            // Check if the file exists. (it gave errors sometimes)
            if (!File.Exists($"{fileName}/{gtxFile}")) continue;
            
            // Move the .gtx file to converted/gtx and delete the {fileName} folder.
            File.Move($"{fileName}/{gtxFile}", $"converted/gtx/{gtxFile}");
            Directory.Delete(fileName, true);
        }
            
        // Add log that .bfres files have been converted
        // to .gtx and the conversion process of .gtx to .dds will begin.
        ConversionLog("bfres", "gtx", "dds");

        // Convert all .gtx files to .dds files.
        ConvertFiles("converted/gtx", "gtx");

        // Add log that all files will be moved to the .dds folder.
        MovingLog("dds");

        // Moving all .dds files to the correct folder.
        foreach (var file in Directory.GetFiles("converted/gtx"))
        {
            var fileName = Path.GetFileName(file);
            var ddsFile = fileName.Replace(".gtx", ".dds");
                
            // Check if the file exists. (it gave errors sometimes)
            if (!File.Exists($"converted/gtx/{ddsFile}")) continue;
            
            // Copy the .gtx file to converted/dds and delete the .gtx file.
            File.Copy($"converted/gtx/{ddsFile}", $"converted/dds/{ddsFile}");
            File.Delete($"converted/gtx/{ddsFile}");
        }
            
        // Add log that .gtx files have been converted
        // to .dds and the conversion process of .dds to .png will begin.
        ConversionLog("gtx", "dds", "png");
            
        // Convert all .dds files to .png files.
        ConvertFiles("converted/dds", "dds");
            
        // Add log that all files will be moved to the .png folder.
        MovingLog("png");
            
        // Moving all .png files to the correct folder.
        foreach (var file in Directory.GetFiles("converted/dds"))
        {
            var fileName = Path.GetFileName(file);
            var pngFile = fileName.Replace(".dds", ".png");
                
            // Check if the file exists. (it gave errors sometimes)
            if (!File.Exists($"converted/dds/{pngFile}")) continue;
            
            // Copy the .png file to converted/dds and delete the .dds file.
            File.Copy($"converted/dds/{pngFile}", $"converted/png/{pngFile}");
            File.Delete($"converted/dds/{pngFile}");
        }

        // If the remove_bfres value in the config is set to true, delete the bfres folder.
        if(Config.GetConfig("remove_bfres")) RemoveFileType("bfres");

        // If the remove_gtx value in the config is set to true, delete the gtx folder.
        if(Config.GetConfig("remove_gtx")) RemoveFileType("gtx");

        // If the remove_dds value in the config is set to true, delete the dds folder.
        if(Config.GetConfig("remove_dds")) RemoveFileType("dds");
            
        // Shutdown the application.
        Shutdown();
    }

    private static void ConvertFiles(string folder, string tool)
    {
        // Get all the files in the given folder.
        foreach (var file in Directory.GetFiles(folder))
        {
            var fileName = Path.GetFileName(file);
                
            // Check which conversion tool needs to be used.
            switch(tool)
            {
                // If yaz0, decode all files and convert them to .bfres files.
                case "yaz0":
                    Yaz0Decoder.DecodeFile(fileName);
                    break;
                // If bfres, convert .bfres files to .gtx files.
                case "bfres":
                    Converter.Bfres2Gtx(fileName);
                    break;
                // If gtx, convert .gtx files to .dss files.
                case "gtx":
                    Converter.GtxToDds(fileName);
                    break;
                // If dds, convert .dds files to .png files.
                case "dds":
                    Converter.DdsToPng(fileName);
                    break;
            }
        }
    }
        
    private static void ConversionLog(string oldType, string currentType, string newType)
    {
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine($"Decompressed all .{oldType} files!");
        Console.WriteLine($"Starting to convert .{currentType} files to .{newType}!");
        Console.WriteLine("----------------------------------------------");
    }

    private static void MovingLog(string fileType)
    {
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine($"Moving all .{fileType} files to the correct folder.");
    }
        
    private static void RemoveFileType(string type)
    {
        Directory.Delete($"converted/{type}", true);
            
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine($"Removed the {type} folder from [converted/{type}]");
    }
        
    private static void Shutdown()
    {
        Console.WriteLine();
        Console.WriteLine("BotwIconConverter is completed. Press any key to close application...");
        Console.ReadKey();
    }

    private static void ClearConverted()
    {
        // Check if the converted folder exists if it does, delete the converted folder every time the program starts.
        if(Directory.Exists("converted")) Directory.Delete("converted", true);
            
        // Create all the folders.
        Directory.CreateDirectory("converted/bfres");
        Directory.CreateDirectory("converted/gtx");
        Directory.CreateDirectory("converted/dds");
        Directory.CreateDirectory("converted/png");
    }
        
    private static void WriteAsciiArt()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(@"  ____        _              _____                   _____                          _            ");
        Console.WriteLine(@" |  _ \      | |            |_   _|                 / ____|                        | |           ");
        Console.WriteLine(@" | |_) | ___ | |___      __   | |  ___ ___  _ __   | |     ___  _ ____   _____ _ __| |_ ___ _ __ ");
        Console.WriteLine(@" |  _ < / _ \| __\ \ /\ / /   | | / __/ _ \| '_ \  | |    / _ \| '_ \ \ / / _ \ '__| __/ _ \ '__|");
        Console.WriteLine(@" | |_) | (_) | |_ \ V  V /   _| || (_| (_) | | | | | |___| (_) | | | \ V /  __/ |  | ||  __/ |   ");
        Console.WriteLine(@" |____/ \___/ \__| \_/\_/   |_____\___\___/|_| |_|  \_____\___/|_| |_|\_/ \___|_|   \__\___|_|   ");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("BotwIconConverter version: v1.0");
        Console.WriteLine("Created by Almighty-Shogun");
        Console.WriteLine("GitHub: https://github.com/Almighty-Shogun/botw-icon-converter");
        Console.WriteLine();
    }
}