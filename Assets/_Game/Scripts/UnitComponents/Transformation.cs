using _Game.Scripts.Utils;
using UnityEngine;

namespace Game.PlayerSystem
{
    public class Transformation
    {
        public Observable<Vector2> Direction { get; }
        public Observable<Vector2> Position { get; }
        public Observable<Vector3> Scale { get; }

        public Transformation(Vector2 position, Vector3 scale)
        {
            Position = new Observable<Vector2>(position);
            Scale = new Observable<Vector3>(scale);
            Direction = new Observable<Vector2>(Vector2.zero);
        }
    }
}