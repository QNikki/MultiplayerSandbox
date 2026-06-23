using Unity.Netcode;
using UnityEngine;

namespace DZM.Core.Conditions
{
    [CreateAssetMenu(fileName = "NetworkCondition_Client", menuName = "Authoring/Condition/ClientCondition")]
    public class ClientCondition: NetworkCondition
    {
        public override bool IsMet(NetworkBehaviour behaviour) =>  behaviour.IsClient;
    }
}