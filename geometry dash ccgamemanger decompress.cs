using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Text;
using System.Xml.Serialization;

class Program
{
    static void Main()
    {
        string InputPath = "CCGameManager.dat";
        string OutputPath = "output.xml";

        byte[] rawBytes = File.ReadAllBytes(InputPath);
        for (int i = 0; i < rawBytes.Length; i++)
        {
            rawBytes[i] ^= 11;
        }

        string base64 = Encoding.ASCII.GetString(rawBytes).TrimEnd('\0');
        base64 = base64.Replace("-", "+").Replace("_", "/");
        byte[] compressedBytes = Convert.FromBase64String(base64);

        string xml = GZipDecompress(compressedBytes);

        File.WriteAllText(OutputPath, xml);
        Console.WriteLine("Over Finally!");
    }

    static string GZipDecompress(byte[] data)
    {
        using (MemoryStream input = new MemoryStream(data))
        using (GZipStream gzip = new GZipStream(input, CompressionMode.Decompress))
        using (StreamReader reader = new StreamReader(gzip, Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
    }
}


