using UnityEngine;

namespace DZM
{
    public abstract class SpawnZone: MonoBehaviour
    {
        public abstract Vector3 SpawnPoint { get; }
    }
}