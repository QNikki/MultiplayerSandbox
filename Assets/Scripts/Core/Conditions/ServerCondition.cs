using Unity.Netcode;
using UnityEngine;

namespace DZM.Core.Conditions
{
    [CreateAssetMenu(fileName = "NetworkCondition_Server", menuName = "Authoring/Condition/ServerCondition")]
    public class ServerCondition: NetworkCondition
    {
        public override bool IsMet(NetworkBehaviour behaviour) => behaviour.IsServer;
    }
}