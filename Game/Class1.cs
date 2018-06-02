using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Game
{
    class Game : GameWindow
    {
        #region Temporary Rotation Test

        float n;

        float[] C = new float[3] {0.0f, 0.0f, 10.0f}; //x1

        float x1; float y1; float z1;
        float x2; float y2; float z2;
        float x3; float y3; float z3;
        float x4; float y4; float z4;
        float x5; float y5; float z5;
        float x6; float y6; float z6;
        float x7; float y7; float z7;
        float x8; float y8; float z8;

        float yaw; float pitch;

        Vector3 PositionVector = new Vector3(0f,0f,0f);
        Vector3 DirectionVector;
        #endregion

        public Game()
            : base(800, 600, GraphicsMode.Default, "My Game")
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            this.MouseMove += MouseMover;

            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        private void MouseMover(object sender, MouseMoveEventArgs e)
        {
            yaw -= e.XDelta / 100f;
            pitch += e.YDelta / 100f;
            //OpenTK.Input.Mouse.SetPosition(400, 300);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 3, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            n += (float)(Math.PI / 36);
            n %= (float)(2 * Math.PI);

            DirectionVector = new Vector3((float)Math.Sin(yaw) * (float)Math.Cos(pitch), (float)Math.Sin(pitch), (float)Math.Cos(yaw) * (float)Math.Cos(pitch));

            if (Keyboard[Key.Escape])
                Exit();

            if (Keyboard[Key.W])
                PositionVector += DirectionVector;

            if (Keyboard[Key.S])
                PositionVector -= DirectionVector;

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelview = Matrix4.LookAt(PositionVector, PositionVector + DirectionVector, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            GL.Begin(BeginMode.Quads);

            //1 = (0,0,0)
            x1 = C[0] + (float)(Math.Sin(n));
            y1 = C[1];
            z1 = C[2] + (float)(Math.Cos(n));
            //2 = (0,1,0)
            x2 = C[0] + (float)(Math.Sin(n));
            y2 = C[1] + 1;
            z2 = C[2] + (float)(Math.Cos(n));
            //3 = (1,1,0)
            x3 = C[0] - (float)(Math.Cos(n));
            y3 = C[1] + 1;
            z3 = C[2] + (float)(Math.Sin(n));
            //4 = (1,0,0)
            x4 = C[0] - (float)(Math.Cos(n));
            y4 = C[1];
            z4 = C[2] + (float)(Math.Sin(n));
            //5 = (0,0,1)
            x5 = C[0] + (float)(Math.Cos(n));
            y5 = C[1];
            z5 = C[2] - (float)(Math.Sin(n));
            //6 = (0,1,1)
            x6 = C[0] + (float)(Math.Cos(n));
            y6 = C[1] + 1;
            z6 = C[2] - (float)(Math.Sin(n));
            //7 = (1,1,1)
            x7 = C[0] - (float)(Math.Sin(n));
            y7 = C[1] + 1;
            z7 = C[2] - (float)(Math.Cos(n));
            //8 = (1,0,1)
            x8 = C[0] - (float)(Math.Sin(n));
            y8 = C[1];
            z8 = C[2] - (float)(Math.Cos(n));

            //1,2,3,4 RED
            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(x1, y1, z1);
            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(x2, y2, z2);
            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(x3, y3, z3);
            GL.Color3(1.0f, 0.0f, 0.0f); GL.Vertex3(x4, y4, z4);
            //5,6,7,8 GREEN
            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex3(x5, y5, z5);
            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex3(x6, y6, z6);
            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex3(x7, y7, z7);
            GL.Color3(0.0f, 1.0f, 0.0f); GL.Vertex3(x8, y8, z8);
            //1,2,6,5 BLUE
            GL.Color3(0.0f, 0.0f, 1.0f); GL.Vertex3(x1, y1, z1);
            GL.Color3(0.0f, 0.0f, 1.0f); GL.Vertex3(x2, y2, z2);
            GL.Color3(0.0f, 0.0f, 1.0f); GL.Vertex3(x6, y6, z6);
            GL.Color3(0.0f, 0.0f, 1.0f); GL.Vertex3(x5, y5, z5);
            //1,3,7,8 YELLOW
            GL.Color3(1.0f, 1.0f, 0.0f); GL.Vertex3(x4, y4, z4);
            GL.Color3(1.0f, 1.0f, 0.0f); GL.Vertex3(x3, y3, z3);
            GL.Color3(1.0f, 1.0f, 0.0f); GL.Vertex3(x7, y7, z7);
            GL.Color3(1.0f, 1.0f, 0.0f); GL.Vertex3(x8, y8, z8);
            //1,5,8,4 MAGENTA
            GL.Color3(1.0f, 0.0f, 1.0f); GL.Vertex3(x1, y1, z1);
            GL.Color3(1.0f, 0.0f, 1.0f); GL.Vertex3(x5, y5, z5);
            GL.Color3(1.0f, 0.0f, 1.0f); GL.Vertex3(x8, y8, z8);
            GL.Color3(1.0f, 0.0f, 1.0f); GL.Vertex3(x4, y4, z4);
            //2,6,7,3 CYAN
            GL.Color3(0.0f, 1.0f, 1.0f); GL.Vertex3(x2, y2, z2);
            GL.Color3(1.0f, 1.0f, 1.0f); GL.Vertex3(x6, y6, z6);
            GL.Color3(0.0f, 1.0f, 1.0f); GL.Vertex3(x7, y7, z7);
            GL.Color3(0.0f, 1.0f, 1.0f); GL.Vertex3(x3, y3, z3);

            GL.End();

            SwapBuffers();
        }

        [STAThread]
        static void Main()
        {
            // The 'using' idiom guarantees proper resource cleanup.
            // We request 30 UpdateFrame events per second, and unlimited
            // RenderFrame events (as fast as the computer can handle).


            using (Game game = new Game())
            {
                game.Run(30.0);
            }
        }
    }
}