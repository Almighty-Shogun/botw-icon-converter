using System.Text;

namespace BotwIconConverter.Core;

public static class Yaz0Decoder
{
    public static void DecodeFile(string fileName)
    {
        var data = File.ReadAllBytes($"icons/{fileName}");

        if (Encoding.ASCII.GetString(data.Take(4).ToArray()).Equals("Yaz0"))
        {
            var decompressedSize = BitConverter.ToInt32(data.Skip(4).Take(4).Reverse().ToArray(), 0);
            data = data.Skip(16).ToArray();
            var decompressedData = new byte[decompressedSize];

            var readPosition = 0;
            var writePosition = 0;
            var validBitCount = 0;
            var currentByteCode = 0;

            while (writePosition < decompressedSize)
            {
                if (validBitCount == 0)
                {
                    currentByteCode = data[readPosition];
                    ++readPosition;
                    validBitCount = 8;
                }

                if ((currentByteCode & 0x80) != 0)
                {
                    decompressedData[writePosition] = data[readPosition];
                    writePosition++;
                    readPosition++;
                }
                else
                {
                    var byte1 = data[readPosition];
                    var byte2 = data[readPosition + 1];
                    readPosition += 2;

                    var dist = (uint) (((byte1 & 0xf) << 8) | byte2);
                    var copySource = (uint) (writePosition - (dist + 1));
                    var byteCount = (uint) (byte1 >> 4);

                    if (byteCount == 0)
                    {
                        byteCount = (uint) (data[readPosition] + 0x12);
                        readPosition++;
                    }
                    else
                    {
                        byteCount += 2;
                    }

                    for (var i = 0; i < byteCount; ++i)
                    {
                        decompressedData[writePosition] = decompressedData[copySource];
                        copySource++;
                        writePosition++;
                    }
                }

                currentByteCode <<= 1;
                validBitCount -= 1;
            }

            var newFileName = Path.GetFileName(fileName).Replace(".sbitemico", ".bfres");
                
            var decompressedFile = File.Create($"converted/bfres/{newFileName}", decompressedSize);
                
            decompressedFile.Write(decompressedData, 0, decompressedSize);
            decompressedFile.Flush();
            decompressedFile.Close();
                
            Console.WriteLine($"Successfully decompressed {newFileName}");
        }
        else
        {
            Console.WriteLine($"{fileName} is not a Yaz0 compressed file.");
        }
    }
}