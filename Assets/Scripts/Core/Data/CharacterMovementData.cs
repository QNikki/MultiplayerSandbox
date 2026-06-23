using UnityEngine;

namespace DZM.Core.Data
{
    public class CharacterMovementData : MonoBehaviour
    {
        [field: SerializeField] public Transform Character { get; set; }

        [field: SerializeField] public CharacterInputData Input { get; set; }

        [field: SerializeField] public float Speed { get; set; } = 5f;

        public Vector3 Position => Character.position;

        public Quaternion Rotation => Character.rotation;
    }
}
