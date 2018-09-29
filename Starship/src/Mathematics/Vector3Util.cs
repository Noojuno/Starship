using Microsoft.Xna.Framework;

namespace Starship.Mathematics
{
    public static class Vector3Util
    {
        public static Vector3 Forward = new Vector3(0, 0, 1);
        public static Vector3 Back = new Vector3(0, 0, -1);

        public static Vector3 AdjustByDirection(this Vector3 position, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return position + Vector3.Up;
                case Direction.Down:
                    return position + Vector3.Down;
                case Direction.Left:
                    return position + Vector3.Left;
                case Direction.Right:
                    return position + Vector3.Right;
                case Direction.Forward:
                    return position + Vector3Util.Forward;
                case Direction.Back:
                    return position + Vector3Util.Back;
            }

            return position;
        }
    }
}
