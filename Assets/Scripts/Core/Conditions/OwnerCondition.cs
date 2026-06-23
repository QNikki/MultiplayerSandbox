using Unity.Netcode;
using UnityEngine;

namespace DZM.Core.Conditions
{
    [CreateAssetMenu(fileName = "NetworkCondition_Owner", menuName = "Authoring/Condition/OwnerCondition")]
    public class OwnerCondition: NetworkCondition
    {
        public override bool IsMet(NetworkBehaviour behaviour) =>  behaviour.IsOwner;
    }
}