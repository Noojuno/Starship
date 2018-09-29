using Microsoft.Xna.Framework;
using Starship.Interfaces;

namespace Starship.Entities
{
    public abstract class Entity : IUpdate, IRender
    {
        public Vector3 Position = Vector3.Zero;
        public Vector3 EulerRotation = Vector3.Zero;
        public Quaternion Rotation = Quaternion.Identity;

        public virtual void Render(GameTime gameTime)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
    }
}
