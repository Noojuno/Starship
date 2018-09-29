using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Starship.Entities;

namespace Starship.Graphics
{
    public class Camera : Entity
    {
        protected const float m_pitchLimit = 1.4f;
        protected const float m_speed = 0.25f;
        protected const float m_mouseSpeedX = 0.0045f;
        protected const float m_mouseSpeedY = 0.0025f;
        public Vector3 Direction;

        protected MouseState m_prevMouse;
        public Matrix Projection;

        public Matrix View;


        /// <summary>
        ///     Creates the instance of the camera.
        /// </summary>
        public Camera()
        {
            var game = StarshipEngine.INSTANCE;
            this.Position = new Vector3(1, 1, 1);

            // Create the direction vector and normalize it since it will be used for movement
            this.Direction = Vector3.Zero - this.Position;
            this.Direction.Normalize();

            // Create default camera matrices
            this.Projection =
                Matrix.CreatePerspectiveFieldOfView(90 / 100f, game.Size.Width / (float) game.Size.Height, 0.01f, 1000);
            this.View = this.CreateLookAt();
        }

        /// <summary>
        ///     Yaw of the camera in radians.
        /// </summary>
        public double Yaw => Math.PI - Math.Atan2(this.Direction.X, this.Direction.Z);

        /// <summary>
        ///     Pitch of the camera in radians.
        /// </summary>
        public double Pitch => Math.Asin(this.Direction.Y);

        /// <summary>
        ///     Create a view matrix using camera vectors.
        /// </summary>
        protected Matrix CreateLookAt()
        {
            return Matrix.CreateLookAt(this.Position, this.Position + this.Direction, Vector3.UnitY);
        }

        protected virtual void ProcessInput()
        {
            var mouse = Mouse.GetState();
            var keyboard = Keyboard.GetState();
            var speed = m_speed;

            if (keyboard.IsKeyDown(Keys.LeftControl))
            {
                speed = m_speed * 2;
            }

            // Move camera with WASD keys
            if (keyboard.IsKeyDown(Keys.W))
                // Move Forward and backwards by adding Position and Direction vectors
                this.Position += this.Direction * speed;

            if (keyboard.IsKeyDown(Keys.S))
                this.Position -= this.Direction * speed;

            if (keyboard.IsKeyDown(Keys.A))
                // Strafe by adding a cross product of Vector3.UnitY and Direction vectors
                this.Position += Vector3.Cross(Vector3.UnitY, this.Direction) * speed;

            if (keyboard.IsKeyDown(Keys.D))
                this.Position -= Vector3.Cross(Vector3.UnitY, this.Direction) * speed;

            if (keyboard.IsKeyDown(Keys.Space))
                this.Position += Vector3.UnitY * speed;

            if (keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.X))
                this.Position -= Vector3.UnitY * speed;


            // Calculate yaw to look around with a mouse
            this.Direction = Vector3.Transform(this.Direction,
                (Matrix.CreateFromAxisAngle(Vector3.UnitY, -m_mouseSpeedX * (mouse.X - this.m_prevMouse.X))));

            // Pitch is limited to m_pitchLimit
            var angle = m_mouseSpeedY * (mouse.Y - this.m_prevMouse.Y);
            if ((this.Pitch < m_pitchLimit || angle > 0) && (this.Pitch > -m_pitchLimit || angle < 0))
                this.Direction = Vector3.Transform(this.Direction,
                    Matrix.CreateFromAxisAngle(Vector3.Cross(Vector3.UnitY, this.Direction), angle));

            this.m_prevMouse = mouse;
        }

        /// <summary>
        ///     Allows the game component to update itself.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            this.View = this.CreateLookAt();
            this.ProcessInput();
        }
    }
}