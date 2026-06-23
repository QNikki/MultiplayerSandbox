using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace DZM.Core.Entities
{
    public class EntityRegistrar : NetworkBehaviour
    {
        [SerializeField] private GroupRegistry _registry;

        [SerializeField] private List<Component> _data = new();

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (_registry != null)
            {
                _registry.Register(this, _data);
            }
        }

        public override void OnNetworkDespawn()
        {
            if (_registry != null)
            {
                _registry.Unregister(this, _data);
            }

            base.OnNetworkDespawn();
        }
    }
}
