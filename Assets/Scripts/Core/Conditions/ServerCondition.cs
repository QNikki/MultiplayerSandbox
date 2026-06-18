using Unity.Netcode;

namespace DZM.Core.Conditions
{
    public class ServerCondition: NetworkCondition
    {
        public override bool IsMet(NetworkBehaviour behaviour) => behaviour.IsServer;
    }
}