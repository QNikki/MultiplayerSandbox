using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DZM.Core
{
    public class WorldSystem : MonoBehaviour
    {
        [field: SerializeField] private RandomAuthoring RandomAuthoring { get; set; }
        
        private readonly Dictionary<SystemGroupAuthoring, CancellationTokenSource> _groupUpdate = new();

        private void Awake()
        {
            RandomAuthoring.CreateRandom();
        }
        
        private void OnDestroy()
        {
            foreach (var group in _groupUpdate)
            {
                TryCancelProcess(group.Key);
            }
            
            RandomAuthoring.Dispose();
        }

        public void AddGroup(SystemGroupAuthoring systemGroup, PlayerLoopTiming timing)
        {
            if (_groupUpdate.ContainsKey(systemGroup))
            {
                Debug.LogWarning($"Group Already contains in World {systemGroup.GetType()}");
                return;
            }
            
            systemGroup.Setup();
            StartProcess(systemGroup, timing);
        }

        public void RemoveGroup(SystemGroupAuthoring systemGroup)
        {
            if (_groupUpdate.ContainsKey(systemGroup))
            {
                TryCancelProcess(systemGroup);
                systemGroup.Cleanup();
                _groupUpdate.Remove(systemGroup);
            }
        }


        private void StartProcess(SystemGroupAuthoring group, PlayerLoopTiming timing)
        {
            TryCancelProcess(group);
            var cts = new CancellationTokenSource();
            _groupUpdate.Add(group, cts);
            ProcessUpdateAsync(cts.Token).Forget();

            async UniTaskVoid ProcessUpdateAsync(CancellationToken token)
            {
                while (!token.IsCancellationRequested)
                {
                    group.UpdateGroup();
                    await UniTask.NextFrame(timing, token);
                }
            }
        }
        
        private void TryCancelProcess(SystemGroupAuthoring group)
        {
            if (!_groupUpdate.TryGetValue(group, out var cts))
            {
                return;
            }

            cts.Cancel();
            cts.Dispose();
            _groupUpdate.Remove(group);
        }
    }
}