using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DZM.Core.Conditions;
using Unity.Netcode;
using UnityEngine;

namespace DZM.Core
{
    public class NetworkSystemGroupActivator : NetworkBehaviour
    {
        [Serializable]
        private class SystemGroupInfo
        {
            [field: SerializeField] private SystemGroupAuthoring _systemGroup;

            [field: SerializeField] private NetworkCondition _condition;

            [field: SerializeField] private PlayerLoopTiming _updateTiming;
            
            public void Setup(WorldSystem context, NetworkBehaviour networkBehaviour)
            {
                if (_condition.IsMet(networkBehaviour))
                {
                    context.AddGroup(_systemGroup, _updateTiming);
                }
            }

            public void Cleanup(WorldSystem context)
            {
                context.RemoveGroup(_systemGroup);
            }
        }
        
        [field: SerializeField] private WorldSystem _worldSystem;

        [field: SerializeField] private GroupRegistry _groupRegistry;

        [field: SerializeField] private List<SystemGroupInfo> _groupInfos = new();

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (_groupRegistry != null)
            {
                _groupRegistry.Activate(this);
            }

            foreach (var info in _groupInfos)
            {
                info.Setup(_worldSystem, this);
            }
        }

        public override void OnNetworkDespawn()
        {
            foreach (var info in _groupInfos)
            {
                info.Cleanup(_worldSystem);
            }

            if (_groupRegistry != null)
            {
                _groupRegistry.Deactivate();
            }

            base.OnNetworkDespawn();
        }
    }
}