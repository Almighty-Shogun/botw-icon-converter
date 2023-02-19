using System.Diagnostics;

namespace BotwIconConverter.Core;

public static class Converter
{
    private static string _output = "";
        
    public static void Bfres2Gtx(string fileName)
    {
        StartOtherProcess(@"resources\bfres-to-gtx\quickbms.exe", @$"resources\bfres-to-gtx\BFRES_Textures.bms converted\bfres\{fileName}");
            
        Console.WriteLine($"Converted {fileName} to {fileName.Replace(".bfres", ".gtx")}");
    }

    public static void GtxToDds(string fileName)
    {
        StartOtherProcess(@"resources\gtx-to-dds\gtx_extract.exe", @$"converted\gtx\{fileName}");
            
        Console.WriteLine($"Converted {fileName} to {fileName.Replace(".gtx", ".dds")}");
    }

    public static void DdsToPng(string fileName)
    {
        StartOtherProcess(@"resources\dds-to-png\DDStronk.exe", @$"converted\dds\{fileName}");
            
        Console.WriteLine($"Converted {fileName} to {fileName.Replace(".dds", ".png")}");
    }

    private static void StartOtherProcess(string fileName, string arguments)
    {
        var process = new Process();
        process.StartInfo.FileName = fileName;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
            
        process.Start();
            
        process.ErrorDataReceived += ConsoleDataReceived;
        process.OutputDataReceived += ConsoleDataReceived;
            
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
    }

    private static void ConsoleDataReceived(object sender, DataReceivedEventArgs e)
    {
        if (e.Data != null)
        {
            _output += e.Data;
        }
    }
}