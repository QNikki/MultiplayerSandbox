using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace DZM
{
    public class CreatePlayerService: MonoBehaviour
    {
        [field: SerializeField] private NetworkManager _networkManager;
        
        [field: SerializeField] private List<GameObject> _variants;

        [field: SerializeField] private List<SpawnZone> _spawnZones;

        private Dictionary<ulong, NetworkObject> _playerToId = new Dictionary<ulong, NetworkObject>();

        private Random _random;
        
        private void Awake()
        {
            _networkManager.OnServerStarted += OnServerStarted;
            _networkManager.OnServerStopped += OnServerStopped;

        }

        private void OnDestroy()
        {
            _networkManager.OnServerStarted -= OnServerStarted;
            _networkManager.OnServerStopped -= OnServerStopped;
        }

        private void OnServerStarted()
        {
            _random = new Random((uint)System.DateTime.Now.Ticks);
            _networkManager.OnClientConnectedCallback += SingletonOnOnClientConnectedCallback;
            _networkManager.OnClientDisconnectCallback += SingletonOnOnClientDisconnectCallback;
        }

        private void OnServerStopped(bool force)
        {
            _playerToId.Clear();
            _networkManager.OnClientConnectedCallback -= SingletonOnOnClientConnectedCallback;
            _networkManager.OnClientDisconnectCallback -= SingletonOnOnClientDisconnectCallback;
        }


        private void SingletonOnOnClientDisconnectCallback(ulong obj)
        {
            if (_playerToId.TryGetValue(obj, out var networkObject))
            {
                networkObject.Despawn();
                _playerToId.Remove(obj);
            }
        }

        private void SingletonOnOnClientConnectedCallback(ulong clientId)
        {
            if (_playerToId.ContainsKey(clientId))
            {
                Debug.Log("obj exist");
                return;
            }

            var player = _variants[_random.NextInt(0, _variants.Count - 1)];
            var spawnZone = _spawnZones[_random.NextInt(0, _spawnZones.Count - 1)];
            var playerGo = Instantiate(player, spawnZone.SpawnPoint, Quaternion.identity);
          
            var networkObject = playerGo.GetComponent<NetworkObject>();
            Debug.Log($"SPAWN: clientId={clientId} ownerBefore={networkObject.OwnerClientId}");
            networkObject.SpawnAsPlayerObject(clientId);
            _playerToId.Add(clientId, networkObject);
            Debug.Log($"AFTER SPAWN owner={networkObject.OwnerClientId}");
        }
    }
}