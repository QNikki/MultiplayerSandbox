using DZM.Client;
using UnityEngine;

namespace DZM.Core.Data
{
    public class CharacterInputData : MonoBehaviour
    {
        [field: SerializeField] public CharacterNetworkRpc Network { get; set; }

        public CharacterMoveInput CurrentInput { get; set; }

        public float SendTime { get; set; }
    }
}
