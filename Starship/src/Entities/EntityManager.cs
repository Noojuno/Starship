using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Starship.Entities
{
    public static class EntityManager
    {
        private static List<Entity> _entities = new List<Entity>();

        public static void Update(GameTime gameTime)
        {
            foreach (var entity in _entities)
            {
                entity.Update(gameTime);
            }
        }

        public static void Render(GameTime gameTime)
        {
            foreach (var entity in _entities)
            {
                entity.Render(gameTime);
            }
        }
    }
}
