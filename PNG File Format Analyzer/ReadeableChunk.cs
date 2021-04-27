using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PNG_File_Format_Analyzer
{
    class Chunk
    {
        public Chunk(int length, string type, byte[] chunkData)
        {
            Length = length;
            Type = type;
            ChunkData = chunkData;
        }
        public int Length { get; }
        public string Type { get; }
        public byte[] ChunkData { get; }
    }

    abstract class ReadeableChunk
    {
        public abstract bool isReadeableChunk(string type);
        public abstract Chunk readChunck(BinaryReader reader);

        public static int getChunkLength(BinaryReader reader)
        {
            int chunckLength = 0;

            for (int i = 0; i < 4; i++)
            {
                chunckLength += reader.ReadByte();
            }

            return chunckLength;
        }

        public static string getChunkType(BinaryReader reader)
        {
            string type = "";
            for (int i = 0; i < 4; i++)
            {
                type += Convert.ToChar(reader.ReadByte());
            }

            return type;
        }

        public byte[] collectData(BinaryReader reader, int size)
        {
            byte[] data = new byte[size];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = reader.ReadByte();
            }

            return data;
        }
    }

    class IHDRReader : ReadeableChunk
    {

        public override bool isReadeableChunk(string type)
        {
            return type == "IHDR";
        }
        public override Chunk readChunck(BinaryReader reader)
        {
            byte[] data = collectData(reader, 13);
            return new Chunk(13, "IHDR", data);
        }
    }

    class IENDReader : ReadeableChunk
    {

        public override bool isReadeableChunk(string type)
        {
            return type == "IEND";
        }
        public override Chunk readChunck(BinaryReader reader)
        {
            byte[] data = collectData(reader, 0);
            return new Chunk(0, "IEND", data);
        }
    }

}
