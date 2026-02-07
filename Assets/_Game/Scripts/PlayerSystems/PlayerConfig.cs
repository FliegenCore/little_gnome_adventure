using UnityEngine;

namespace _Game.Scripts.PlayerSystems
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Hell/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerView PlayerViewPrefab { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public Vector3 StartSpawnPosition { get; private set; }
        [field: SerializeField] public Vector3 StartScale { get; private set; }
    }
}