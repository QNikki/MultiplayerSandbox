using UnityEngine;

namespace DZM.Core
{
    public class CharacterMovementData : MonoBehaviour
    {
        public CharacterMoveInput CurrentInput { get; set; }

        public float SendTime { get; set; }
    }
}