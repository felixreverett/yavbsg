using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game
{
    public class Region
    {
        public Region()
        {
            Chunks = new Chunk[16,16,16];
        }

        public Chunk[,,] Chunks;

        public Chunk GetChunk(Pos.ChunkPos pos)
        {
            pos = pos.Normalize();
            return Chunks[pos.X, pos.Y, pos.Z];
        }

        public void SetChunk(Pos.ChunkPos pos, Chunk c)
        {
            pos = pos.Normalize();
            Chunks[pos.X, pos.Y, pos.Z] = c;
        }

        public void Read(BinaryReader r)
        {
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        if (r.ReadByte() == 1)
                        {
                            Chunks[x, y, z] = new Chunk();
                            Chunks[x, y, z].Read(r);
                        }
                    }
                }
            }
        }

        public void Write(BinaryWriter w)
        {
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        if (Chunks[x, y, z] != null)
                        {
                            w.Write((byte)1);
                            Chunks[x, y, z].Write(w);
                        }
                        else
                        {
                            w.Write((byte)0);
                        }
                    }
                }
            }
        }
    }
}
