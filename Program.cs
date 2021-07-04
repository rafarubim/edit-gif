using System;
using System.IO;

namespace EditGif
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Beggining:");
      using (var gif = File.OpenRead("test.gif"))
      {
        var formatBuff = new byte[6];
        var widthBuff = new byte[2];
        var heightBuff = new byte[2];
        var descriptorBuff = new byte[3];
        var byteBuff = new byte[1];
        var northWestBuff = new byte[4];
        var width2Buff = new byte[2];
        var height2Buff = new byte[2];
        var LCTDescriptorBuff = new byte[1];

        gif.Read(formatBuff);
        gif.Read(widthBuff);
        gif.Read(heightBuff);
        gif.Read(descriptorBuff);

        var bitsPerPixel = (0x0F & descriptorBuff[0]) + 1;

        var GCTBuff = new byte[2 ^ (bitsPerPixel) * 3];
        gif.Read(GCTBuff);

        do
        {
          gif.Read(byteBuff);
        } while (byteBuff[0] != 0x2C);

        gif.Read(northWestBuff);
        gif.Read(width2Buff);
        gif.Read(height2Buff);
        gif.Read(LCTDescriptorBuff);

        var format = System.Text.Encoding.ASCII.GetString(formatBuff);
        short width = BitConverter.ToInt16(widthBuff);
        short height = BitConverter.ToInt16(heightBuff);
        short width2 = BitConverter.ToInt16(width2Buff);
        short height2 = BitConverter.ToInt16(height2Buff);
        Console.WriteLine(format);
        Console.WriteLine(width);
        Console.WriteLine(height);
        Console.WriteLine(width2);
        Console.WriteLine(height2);
      };
    }
  }
}
