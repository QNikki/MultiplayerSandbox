using Cysharp.Threading.Tasks;
using DZM.Core.Conditions;
using Unity.Netcode;
using UnityEngine;

namespace DZM.Core
{
    public class NetworkSystemGroup: NetworkBehaviour
    {
        [field: SerializeField] private WorldSystem _worldSystem;
        
        [field: SerializeField] private SystemGroupAuthoring _systemGroup;

        [field: SerializeField] private PlayerLoopTiming _updateTiming;

        [field: SerializeField] private NetworkCondition _condition;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (_condition.IsMet(this))
            {
                _worldSystem.AddGroup(_systemGroup, _updateTiming);
            }
        }

        public override void OnNetworkDespawn()
        {
            _worldSystem.RemoveGroup(_systemGroup);
            base.OnNetworkDespawn();
        }
    }
}