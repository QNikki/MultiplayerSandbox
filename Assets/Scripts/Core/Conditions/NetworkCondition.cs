using Unity.Netcode;
using UnityEngine;

namespace DZM.Core.Conditions
{
    public abstract class NetworkCondition: ScriptableObject
    {
        public abstract bool IsMet(NetworkBehaviour behaviour);
    }
}