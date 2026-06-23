using DZM.Core.Data;
using Unity.Netcode;
using UnityEngine;

namespace DZM.Client
{
    public interface ICharacterNetwork
    {
        void SubmitMove(CharacterMoveInput input);
    }

    public class CharacterNetworkRpc : NetworkBehaviour, ICharacterNetwork
    {
        [SerializeField] private CharacterInputData _inputData;

        public void SubmitMove(CharacterMoveInput input)
        {
            SubmitMoveServerRpc(input);
        }

        [ServerRpc]
        private void SubmitMoveServerRpc(CharacterMoveInput input)
        {
            _inputData.CurrentInput = input;
        }
    }
}
