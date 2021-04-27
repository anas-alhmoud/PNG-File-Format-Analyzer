using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace PNG_File_Format_Analyzer
{
    class PNGReader
    {
        public ReadeableChunk[] ChunkReaders { get; }
        public PNGReader(ReadeableChunk[] chunkReaders)
        {
            ChunkReaders = chunkReaders;
        }

        public Chunk readChunck(BinaryReader reader)
        {
            int length = ReadeableChunk.getChunkLength(reader);
            string type = ReadeableChunk.getChunkType(reader);

            foreach (var cr in ChunkReaders)
            {
                if (cr.isReadeableChunk(type))
                {

                    return cr.readChunck(reader); ;
                }

            }

            // to skip data and CRC
            for (int i = 0; i < length + 4; i++)
            {
                reader.ReadByte();
            }

            return null;
        }
    }
}
