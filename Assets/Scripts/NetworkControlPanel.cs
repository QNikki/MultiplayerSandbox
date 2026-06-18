using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace DZM
{
    public class NetworkControlPanel: MonoBehaviour
    {
        [Serializable]
        private class StatefulButton: IDisposable
        {
            
            [field: SerializeField] private Button _button;

            private bool _state;

            private Action _activeState;
            
            private Action _disableState;

            public void Init(Action activeState, Action disableState)
            {
                _activeState = activeState;
                _disableState = disableState;
                _button.onClick.AddListener(OnClick);
            }

            private void OnClick()
            {
                var targetState = _state ? _disableState : _activeState;
                targetState.Invoke();
                _state = !_state;
            }

            public void Dispose()
            {
                _button.onClick.RemoveListener(OnClick);
                _activeState = null;
                _disableState = null;
            }
        }


        [field: SerializeField] private StatefulButton _host;
        [field: SerializeField] private StatefulButton _client;
        [field: SerializeField] private StatefulButton _server;
        [field: SerializeField] private NetworkManager _networkManager;
        
        private void OnEnable()
        {
            _host.Init(() => _networkManager.StartHost(), () => Debug.Log("Stop Host"));
            _client.Init(() =>  _networkManager.StartClient(), () => Debug.Log("Stop Client"));
            _server.Init(() => _networkManager.StartServer(), () => Debug.Log("Stop Server"));
            
            _networkManager.OnClientConnectedCallback += SingletonOnOnClientStarted;
            _networkManager.OnClientDisconnectCallback += SingletonOnClientDisconnectCallback;
        }



        private void OnDisable()
        {
            
            _networkManager.OnClientConnectedCallback -= SingletonOnOnClientStarted;
            _networkManager.OnClientDisconnectCallback -= SingletonOnClientDisconnectCallback;
            
            _host.Dispose();
            _client.Dispose();
            _server.Dispose();
        }
        
        private void SingletonOnOnClientStarted(ulong id)
        {
            Debug.Log($"DZM Client connect id {id}");
        }
        
        private void SingletonOnClientDisconnectCallback(ulong id)
        {
            Debug.Log($"DZM Client Disconnect id {id}");
        }
    }
}