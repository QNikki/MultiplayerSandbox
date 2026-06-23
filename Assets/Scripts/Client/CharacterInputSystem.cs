using DZM.Core;
using DZM.Core.Data;
using UnityEngine;

namespace DZM.Client
{
    [CreateAssetMenu(fileName = "System_CharacterInput", menuName = "Authoring/System/CharacterInput")]
    public class CharacterInputSystem : DataSystem<CharacterInputData>
    {
        private const float SendRate = 0.05f; // 20 Hz

        private CharacterInputActions _inputActions;

        public override void Setup()
        {
            _inputActions = new CharacterInputActions();
            _inputActions.Enable();
        }

        public override void Execute()
        {
            var moveRead = _inputActions.Player.Move.ReadValue<Vector2>();
            var input = new CharacterMoveInput
            {
                MoveX = moveRead.x,
                MoveY = moveRead.y,
            };

            foreach (var data in DataGroup.ROMembers)
            {
                data.SendTime += Time.deltaTime;
                if (data.SendTime < SendRate)
                {
                    continue;
                }

                data.SendTime = 0f;

                if (data.Network != null)
                {
                    data.Network.SubmitMove(input);
                }
            }
        }

        public override void Cleanup()
        {
            _inputActions?.Dispose();
            _inputActions = null;
        }
    }
}
