using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace Game
{
    public class World
    {
        public Entity Player;
        Dictionary<string, Region> Regions;

        public World()
        {
            Player = new Entity() { Position = new Vector3() };
            Regions = new Dictionary<string, Region>();
        }

        public void LoadChunk(Pos.ChunkPos pos)
        {
            Pos.ChunkPos regionPos = new Pos.BlockPos(pos.X, pos.Y, pos.Z).ToChunk();
            string region = regionPos.X.ToString() + "." + regionPos.Y.ToString() + "." + regionPos.Z.ToString();
            if (!Regions.ContainsKey(region))
            {
                LoadRegion(region);
            }
            if (Regions[region].GetChunk(pos) == null)
            {
                Regions[region].SetChunk(pos, GenerateChunk(pos));
            }
        }

        public void LoadRegion(string r)
        {
            Region region = new Region();
            string f = Path.Combine("World", r + ".dat");
            if (File.Exists(f))
            {
                using (var fs = new FileStream(f, FileMode.Open))
                using (var read = new BinaryReader(fs))
                {
                    region.Read(read);
                }
            }
            Regions[r] = region;
        }

        public Chunk GenerateChunk(Pos.ChunkPos pos)
        {
            Chunk c = new Chunk();
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        if (pos.Y < 0)
                        {
                            c.SetBlock(new Pos.BlockPos(x, y, z), new Block() { id = 1 });
                        }
                    }
                }
            }
            return c;
        }

        public Block GetBlock(Pos.BlockPos pos)
        {
            Pos.ChunkPos p = pos.ToChunk();
            Pos.ChunkPos regionPos = new Pos.BlockPos(p.X, p.Y, p.Z).ToChunk();
            string region = regionPos.X.ToString() + "." + regionPos.Y.ToString() + "." + regionPos.Z.ToString();
            if (Regions.ContainsKey(region))
            {
                Chunk c = Regions[region].GetChunk(p.Normalize());
                if (c != null)
                {
                    return c.GetBlock(pos.Normalize());
                }
            }
            return null;
        }

        public void Render()
        {
            foreach (Region r in Regions.Values)
            {
                foreach (Pos.ChunkPos cp in Pos.ChunkPos.Iter())
                {
                    Chunk c = r.GetChunk(cp);
                    if (c != null)
                    {
                        foreach (Pos.BlockPos bp in Pos.BlockPos.Iter())
                        {
                            if (c.GetBlock(bp) != null)
                            {
                                DrawBlock(cp + bp);
                            }
                        }
                    }
                }
            }
        }

        public void DrawBlock(Pos.BlockPos p)
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Translate(p.X, p.Y, p.Z);
            for (int i = 0; i < 6; i++)
            {
                if (GetBlock(p + Pos.DIRECTIONS[i]) == null)
                {
                    Draw.DrawFace(i);
                }
            }
            GL.PopMatrix();
        }
    }
}
