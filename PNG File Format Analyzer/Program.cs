using System;
using System.IO;
using System.Text;

namespace PNG_File_Format_Analyzer
{
    class Program
    {

        static void Main(string[] args)
        {
            string fileName = "C:\\Users\\***\\***\\*.png";


            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    // collect magic number
                    string magicNumber = "";
                    for (int i = 0; i < 8; i++)
                    {
                        magicNumber += reader.ReadByte().ToString();
                    }

                    // check magic number
                    if (magicNumber == "13780787113102610") {

                        Console.WriteLine("----- File Header Start ----- \n");

                        Console.WriteLine("PNG magic number: " + magicNumber);

                        Console.WriteLine(); // break line

                        Console.WriteLine("----- Chunks ----- \n");

                        PNGReader pr = new PNGReader(new ReadeableChunk[] { new IHDRReader(), new IENDReader() });

                        // IHDR
                        Chunk c = pr.readChunck(reader);

                        Console.WriteLine("Chunck type: " + c.Type);

                        Console.WriteLine("Chunck Length: " + c.Length + " byte");

                        // image Width
                        uint width = 0;
                        for ( int i = 0; i < 4; i++)
                        {
                            width = width << 8;
                            width = width | c.ChunkData[i];
                        }

                        Console.WriteLine("width: " + width + " pixels");

                        // image Height
                        uint height = 0;
                        for (int i = 4; i < 8; i++)
                        {
                            height = height << 8;
                            height = height | c.ChunkData[i];
                        }

                        Console.WriteLine("height: " + height + " pixels");

                        // Bit Depth
                        Console.WriteLine("Bit Depth: " + c.ChunkData[8]);

                        // Color Type
                        Console.WriteLine("Color Type: " + c.ChunkData[9]);

                        // Compression Method
                        Console.WriteLine("Compression Method: " + c.ChunkData[10]);

                        // Filter Method
                        Console.WriteLine("Filter Method: " + c.ChunkData[11]);

                        // Interlace Method
                        Console.WriteLine("Interlace Method: " + c.ChunkData[12]);

                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                }

                
            }

        }
    }
}
