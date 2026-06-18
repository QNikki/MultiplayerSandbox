using Unity.Netcode;

namespace DZM.Core.Conditions
{
    public class ClientCondition: NetworkCondition
    {
        public override bool IsMet(NetworkBehaviour behaviour) =>  behaviour.IsClient;
    }
}