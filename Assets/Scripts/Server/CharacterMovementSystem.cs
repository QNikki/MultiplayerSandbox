using DZM.Core;
using DZM.Core.Data;
using UnityEngine;

namespace DZM.Server
{
    [CreateAssetMenu(fileName = "System_Movement", menuName = "Authoring/System/Movement")]
    public class MovementSystem : DataSystem<CharacterMovementData>
    {
        public override void Setup()
        {
        }

        public override void Execute()
        {
            var deltaTime = Time.deltaTime;

            foreach (var member in DataGroup.ROMembers)
            {
                var input = member.Input.CurrentInput;
                var direction = new Vector3(input.MoveX, 0f, input.MoveY);
                member.Character.position += direction * (member.Speed * deltaTime);
            }
        }

        public override void Cleanup()
        {
        }
    }
}
