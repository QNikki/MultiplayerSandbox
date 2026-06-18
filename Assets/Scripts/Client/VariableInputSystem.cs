using DZM.Core;
using Unity.Netcode;
using UnityEngine;

namespace DZM.Client
{
    public class VariableInputSystem: System<CharacterMovementData>
    {
        private CharacterInputActions _inputActions;

        private float _sendTimer;

        private const float SendRate = 0.05f; // 20 Hz

        public VariableInputSystem(DataGroupAuthoring<CharacterMovementData> dataGroup) : base(dataGroup)
        {
        }
        
        public override void Setup()
        {
            _inputActions = new CharacterInputActions();
            _inputActions.Enable();
        }

        public override void Execute()
        {
            base.Execute();
            foreach (var data in DataGroup.ROMembers)
            {
                data.SendTime += Time.deltaTime;
                if (data.SendTime < SendRate)
                    continue;
                
                data.SendTime = 0f;
                var inputRead = _inputActions.Player.Move.ReadValue<Vector2>();
                var input = new CharacterMoveInput
                {
                    MoveX = inputRead.x,
                    MoveY = inputRead.y,
                };
                
                SubmitInputServerRpc(data, input);
            }
        }
        
        public override void Cleanup()
        {
            _inputActions.Dispose();
            _inputActions = null;
        }
        
        [ServerRpc]
        private void SubmitInputServerRpc(CharacterMovementData data, CharacterMoveInput input)
        {
            data.CurrentInput = input;
        }
    }
}