<h1 align="center">
  <img src='https://cdn.shogunate.tools/assets/logo/png/Almighty_Shogun_Stars_Thick_Small.png'>
  <br>botw-icon-converter</br>
</h1>

A simple script in C# that converts the icons from The Legend of Zelda - Breath of the Wild to PNG files.

# Note
This is an old project that I wrote a long time ago. I have converted the code from using .NET 5 to .NET 6 and published the code here on GitHub. I have no use for the code anymore and it might be useful so someone in the future. This is one of my first C# application that I wrote. If the code doesn't work or you need help with it feel free to contact me.

# Information
This is an C# console application that simply uses other tools to make this conversion go faster then doing them all manually. All original tool creator(s) have been given proper credits in the credits section.

This application does the following:
- Converts `.sbitemico` to `.bfres`
- Converts `.bfres` to `.gtx`
- Converts `.gtx` to `.dds`
- Converts `.dds` to `.png`

It keeps all 4 file types seperated in the `converted` folder. In the `config.json` file you can specify which folder needs to be deleted after the conversion process.

```json
{
  "remove_bfres": false,
  "remove_gtx": false,
  "remove_dds": false
}
```

When a value has been set to `false` it will keep the folder, when it has been set to `true` it will delete the folder at the end of the conversion process.

# How to use/build
**How to use**
- Download .NET 6 [here](//dotnet.microsoft.com/download/dotnet/6.0).
- Download the latest release [here](//github.com/Almighty-Shogun/botw-icon-converter/releases/latest).
- Get all `.sbitemico` files and place them in the `icons` folder.
    - All the `.sbitemico` files are usually located in the `\content\UI\StockItem` folder of the game.
- Check the `config.json` file to see if that are also the options you want to use.
- Start the application.

**How to build**
- Download .NET 6 [here](//dotnet.microsoft.com/download/dotnet/6.0).
- Download/fork the code and open it with any C# IDE (Visual Studio or JetBrains Rider).
- Extract the `resources.zip` file and paste it in the debug or release folder.
- Get all `.sbitemico` files and place them in the `icons` folder.
    - All the `.sbitemico` files are usually located in the `\content\UI\StockItem` folder of the game.
- Check the `config.json` file to see if that are also the options you want to use.
- Check if `Newtonsoft.Json` version `13.0.1` or higher is installed (normally the IDE will do this automatically).

# Credits
- The original owner of the `Yaz0Decoder.cs` (which I forgot where I found it)
- [@Gragog](//github.com/Gragog) for the `.bfres` to `.gtx` converter.
- [@aboood40091](//github.com/aboood40091) for the GTX-extractor.
- [@scorpdx](//github.com/scorpdx) for the `.dds` to `.png` converter.
