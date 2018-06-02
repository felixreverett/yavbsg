using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Game
{
    public class Pos
    {
        public static readonly BlockPos[] DIRECTIONS = new[] {
            new BlockPos(0, 0, -1), //west
            new BlockPos(-1, 0, 0), //south
            new BlockPos(0, 0, 1),  //east
            new BlockPos(1, 0, 0),  //north
            new BlockPos(0, 1, 0),  //up
            new BlockPos(0, -1, 0), //down
        };

        public struct BlockPos //the location of a block?
        {
            public int X;
            public int Y;
            public int Z;

            public BlockPos(int x, int y, int z)
            {
                X = x; Y = y; Z = z;
            }

            public BlockPos Normalize()
            {
                this += new BlockPos(16, 16, 16);
                return new BlockPos(X % 16, Y % 16, Z % 16);
            }
            
            public ChunkPos ToChunk() //finds chunk from block?
            {
                return new ChunkPos((int)Math.Floor(X / 16f), (int)Math.Floor(Y / 16f), (int)Math.Floor(Z / 16f));
            }

            public static BlockPos operator +(BlockPos a, BlockPos b) //when you use + with two BlockPos structs it does this
            {
                return new BlockPos(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
            }

            public static IEnumerable<BlockPos> Iter() //this gets every block in a chunk I think?
            {
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        for (int z = 0; z < 16; z++)
                        {
                            yield return new BlockPos(x, y, z);
                        }
                    }
                }
            }
        }

        public struct ChunkPos
        {
            public int X;
            public int Y;
            public int Z;

            public ChunkPos(int x, int y, int z)
            {
                X = x; Y = y; Z = z;
            }

            public ChunkPos Normalize() //grabs your relative chunk ID?
            {
                this += new ChunkPos(16, 16, 16);
                return new ChunkPos(X % 16, Y % 16, Z % 16);
            }

            public static ChunkPos operator +(ChunkPos a, ChunkPos b)
            {
                return new ChunkPos(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
            }

            public static IEnumerable<ChunkPos> Iter()
            {
                for (int x = 0; x < 16; x++)
                {
                    for (int y = 0; y < 16; y++)
                    {
                        for (int z = 0; z < 16; z++)
                        {
                            yield return new ChunkPos(x, y, z);
                        }
                    }
                }
            }

            public static BlockPos operator +(ChunkPos a, BlockPos b)
            {
                return new BlockPos(a.X * 16 + b.X, a.Y * 16 + b.Y, a.Z * 16 + b.Z);
            }
        }
    }
}
