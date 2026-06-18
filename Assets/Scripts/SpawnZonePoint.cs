using UnityEngine;

namespace DZM
{
    public class SpawnZonePoint: SpawnZone
    {
        [field: SerializeField] private Vector3 Offset { get; set; }

        public override Vector3 SpawnPoint => transform.position + Offset;

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(SpawnPoint, 0.1f);
        }
    }
}