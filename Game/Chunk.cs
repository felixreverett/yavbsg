using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game
{
    public class Chunk
    {
        Block[,,] blocks;
        bool empty;

        public Chunk()
        {
            blocks = new Block[16, 16, 16];
        }

        public Block GetBlock(Pos.BlockPos pos) //returns a relative block from a chunk
        {
            if (empty)
            {
                return null;
            }
            pos = pos.Normalize();
            return blocks[pos.X, pos.Y, pos.Z];
        }

        public void SetBlock(Pos.BlockPos pos, Block b) //sets a block relative to a chunk
        {
            if (empty)
            {
                return;
            }
            pos = pos.Normalize();
            blocks[pos.X, pos.Y, pos.Z] = b;
        }

        public void Read(BinaryReader r) //reads (and loads?) from a file
        {
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        if (r.ReadByte() == 1)
                        {
                            blocks[x, y, z] = new Block();
                            blocks[x, y, z].Read(r);
                        }
                    }
                }
            }
        }

        public void Write(BinaryWriter w)//writes a chunk
        {
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        if (blocks[x, y, z] != null)
                        {
                            w.Write((byte)1);
                            blocks[x, y, z].Write(w);
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
