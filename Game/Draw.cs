using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Game
{
    public class Draw
    {
        public static void DrawCube()
        {
            for (int i = 0; i < 6; i++)
            {
                DrawFace(i);
            }
        }
 
        public static void DrawFace(int f)
        {
            switch (f)
            {
                case 0: //north
                    {
                        GL.Color3(0.9f, 0.9f, 0.9f);
                        GL.Vertex3(-1, -1, -1);
                        GL.Vertex3(-1, 1, -1);
                        GL.Vertex3(1, 1, -1);
                        GL.Vertex3(1, -1, -1);
                        return;
                    }
                case 2: //south
                    {
                        GL.Color3(0.85f, 0.85f, 0.85f);
                        GL.Vertex3(-1, -1, 1);
                        GL.Vertex3(-1, 1, 1);
                        GL.Vertex3(1, 1, 1);
                        GL.Vertex3(1, -1, 1);
                        return;
                    }
                case 1: //east
                    {
                        GL.Color3(0.8f, 0.8f, 0.8f);
                        GL.Vertex3(-1, -1, -1);
                        GL.Vertex3(-1, -1, 1);
                        GL.Vertex3(-1, 1, 1);
                        GL.Vertex3(-1, 1, -1);
                        return;
                    }
                case 3: //west
                    {
                        GL.Color3(0.75f, 0.75f, 0.75f);
                        GL.Vertex3(1, -1, -1);
                        GL.Vertex3(1, -1, 1);
                        GL.Vertex3(1, 1, 1);
                        GL.Vertex3(1, 1, -1);
                        return;
                    }
                case 4: //bottom
                    {
                        GL.Color3(0.7f, 0.7f, 0.7f);
                        GL.Vertex3(-1, -1, -1);
                        GL.Vertex3(-1, -1, 1);
                        GL.Vertex3(1, -1, 1);
                        GL.Vertex3(1, -1, -1);
                        return;
                    }
                case 5: //top
                    {
                        GL.Color3(1f, 1f, 1f);
                        GL.Vertex3(-1, 1, -1);
                        GL.Vertex3(-1, 1, 1);
                        GL.Vertex3(1, 1, 1);
                        GL.Vertex3(1, 1, -1);
                        return;
                    }

            }
        }
    }
}
