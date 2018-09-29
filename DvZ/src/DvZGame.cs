using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Starship;
using Starship.Event;
using Starship.Event.Input;
using Starship.World.Blocks;
using DvZ.Blocks;
using Lidgren.Network;
using Runed.Voxel;
using Starship.Graphics;
using Starship.Graphics.Geometry;
using Starship.Input;
using Starship.Network;
using Starship.Network.Packets;
using Starship.Registry;
using Starship.Util;
using Starship.World;

namespace DvZ
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class DvZGame : StarshipEngine
    {
        public override string Name => "Dwarves vs Zombies v0.01a";

        private Camera camera;
        private NetClient client;

        private readonly Mesh m = new CubeMesh(1f);
        public Server Server;
        private int texture;

        BasicEffect effect;

        private Chunk c;
        private MeshData md;
        private World w = new World(1000);

        public DvZGame(CommandLineOptions args) : base(args)
        {
            this.Window.Title = this.Name;

            this.c = new Chunk(w, Vector3.Zero);

            EVENT_BUS.Subscribe<PreInitializationEvent>(this.OnPreInit);
            EVENT_BUS.Subscribe<InitializationEvent>(this.OnInit);
            EVENT_BUS.Subscribe<PostInitializationEvent>(this.OnPostInit);
            EVENT_BUS.Subscribe<RegistryRegisterEvent<BlockDefinition>>(this.BlockRegistryEvent);
            EVENT_BUS.Subscribe<KeyReleaseEvent>(this.OnKeyRelease);
        }

        private void BlockRegistryEvent(RegistryRegisterEvent<BlockDefinition> e)
        {
            e.GetRegistry().Register(new BlockDefinitionTest());
            BlockRegistry.Register(new BlockDefinitionTest());
            BlockRegistry.Register(new BlockDefinitonAir());

            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        this.c[x, y, z] = new Block(BlockRegistry.Get("test"));
                    }
                }
            }

            this.md = MeshBuilder.BuildChunk(this.c);
        }

        private void OnPreInit(PreInitializationEvent e)
        {
            Mouse.SetCursor(MouseCursor.Arrow);

            Logger.GetLogger().Info("Beginning Pre-Initialization Phase");

            //GL.ClearColor(Color.Black);
            //GL.Enable(EnableCap.DepthTest);

            ///BlockRegistry.Register();

            //this.texture = this.LoadTexture("assets/textures/blockDefinition/stone.png");

            this.Server = new Server();
            this.Server.Start();
        }

        private void OnInit(InitializationEvent e)
        {
            Logger.GetLogger().Info("Beginning Initialization Phase");

            this.camera = new Camera();
            this.effect = new BasicEffect(this.GraphicsDevice);

            foreach (var block in RegistryManager.BLOCKS.Get())
            {
                Logger.GetLogger().Debug(block.Value.GetUnlocalizedName());
            }
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
                case Keys.D1:
                    var msg = this.client.CreateMessage();
                    var packet = new PacketTest(new Random().Next(100));
                    msg.Write(packet.GetId());
                    packet.ToMessage(msg);
                    this.client.SendMessage(msg, NetDeliveryMethod.ReliableOrdered);
                    break;

                case Keys.D2:
                    Logger.GetLogger().DebugFormat("Client Connections: {0}", this.client.ConnectionsCount);
                    break;

                case Keys.D3:
                    InputManager.ToggleMouseLocked();
                    break;

                case Keys.L:
                    Logger.GetLogger().Debug($"{this.Size.Width} {this.Size.Height} {this.Size.X} {this.Size.Y}");
                    break;

                case Keys.Escape:
                    this.Exit();
                    break;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.camera.Update(gameTime);
        }

        protected override void Render(GameTime gameTime)
        {
            this.effect.View = this.camera.View;
            this.effect.Projection = this.camera.Projection;

            /*GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

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
            */

            // Enable some pretty lights
            this.effect.EnableDefaultLighting();

            using (VertexBuffer buffer = this.md.ToVertexBuffer(this.GraphicsDevice))
            {
                GraphicsDevice.SetVertexBuffer(buffer);

                using (IndexBuffer indexBuffer = this.md.ToIndexBuffer(this.GraphicsDevice))
                {
                    GraphicsDevice.Indices = indexBuffer;
                }
            }

            
            

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in this.effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 12, 0, 20);
            }
        
            base.Render(gameTime);
        }
    }
}