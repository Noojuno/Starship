using System;
using System.Drawing;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Starship;
using Starship.Event;
using Starship.Event.Input;
using Starship.Graphics;
using Starship.Graphics.Geometry;
using Starship.Input;
using Starship.Network;
using Starship.Network.Packets;
using Starship.Registry;
using Starship.Util;

namespace DvZ
{
    /// <summary>
    ///     This is the main type for your game.
    /// </summary>
    public class VoxelGame 
    {

        /* 
        private Camera camera;
        private NetClient client;

        private readonly Mesh m = new CubeMesh(1f);
        public Server Server;
        private int texture;

        public VoxelGame(int width, int height, string title) : base(width, height, title)
        {
            EVENT_BUS.Subscribe<PreInitializationEvent>(this.OnPreInit);
            EVENT_BUS.Subscribe<InitializationEvent>(this.OnInit);
            EVENT_BUS.Subscribe<PostInitializationEvent>(this.OnPostInit);
            EVENT_BUS.Subscribe<RegistryRegisterEvent<Starship.World.BlocksDefinition.BlockDefinition>>(this.BlockRegistryEvent);
            EVENT_BUS.Subscribe<KeyReleaseEvent>(this.OnKeyRelease);
        }

        private void BlockRegistryEvent(RegistryRegisterEvent<Starship.World.BlocksDefinition.BlockDefinition> e)
        {
            e.GetRegistry().Register(new BlockDefinitionTest());
        }

        private void OnPreInit(PreInitializationEvent e)
        {
            Logger.GetLogger().Info("Beginning Pre-Initialization Phase");

            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);

            ///BlockRegistry.Register();

            this.texture = this.LoadTexture("assets/textures/blockDefinition/stone.png");

            this.Server = new Server();
            this.Server.Start();
        }

        private void OnInit(InitializationEvent e)
        {
            Logger.GetLogger().Info("Beginning Initialization Phase");

            this.camera = new Camera();

            foreach (var blockDefinition in RegistryManager.BLOCKS.Get())
            {
                Logger.GetLogger().Debug(blockDefinition.Value.GetUnlocalizedName());
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width,
                this.ClientRectangle.Height);

            var projection = this.camera.Projection;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        private void OnPostInit(PostInitializationEvent e)
        {
            Logger.GetLogger().Info("Beginning Post-Initialization Phase");
            var con = new NetPeerConfiguration("starship");
            //con.AutoFlushSendQueue = false;

            this.client = new NetClient(con);
            this.client.Start();

            this.client.Connect("127.0.0.1", 63367);
        }

        public void OnKeyRelease(KeyReleaseEvent e)
        {
            switch (e.Key)
            {
                case Keys.Number1:
                    var msg = this.client.CreateMessage();
                    var packet = new PacketTest(new Random().Next(100));
                    msg.Write(packet.GetId());
                    packet.ToMessage(msg);
                    this.client.SendMessage(msg, NetDeliveryMethod.ReliableOrdered);
                    break;

                case Keys.Number2:
                    Logger.GetLogger().DebugFormat("Client Connections: {0}", this.client.ConnectionsCount);
                    break;

                case Keys.Number3:
                    InputManager.ToggleMouseLocked();
                    break;

                case Keys.Escape:
                    this.Exit();
                    break;
            }
        }

        protected override void Update(double deltaTime)
        {
            base.Update(deltaTime);

            this.camera.Update(deltaTime);

            var ks = this.Keyboard.GetState();
            if (ks.IsKeyDown(Key.Left))
            {
                //worldTranslation *= Matrix.CreateTranslation(-.03f, 0, 0);
            }
            if (ks.IsKeyDown(Key.Right))
            {
                //worldTranslation *= Matrix.CreateTranslation(.03f, 0, 0);
            }
        }

        public int LoadTexture(string file)
        {
            var bitmap = new Bitmap(file);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out int tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int) TextureMagFilter.Nearest);

            return tex;
        }

        protected override void Render(double deltaTime)
        {
            base.Render(deltaTime);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref this.camera.View);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            GL.Color3(Color.White);

            GL.Translate(0, 0, 0);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, this.texture);
            GL.Begin(PrimitiveType.Quads);
            foreach (var norm in this.m.Normals)
            {
                GL.Normal3(norm);
            }


            foreach (var tri in this.m.Triangles)
            {
                //GL.Lis
            }

            var i = 0;
            foreach (var vert in this.m.Vertices)
            {
                GL.TexCoord2(this.m.UV[i]);
                GL.Vertex3(vert);
                i++;
            }

            GL.End();
            GL.Disable(EnableCap.Texture2D);

            this.SwapBuffers();
        }*/
    }
}