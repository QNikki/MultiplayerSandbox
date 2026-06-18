using DZM.Core;
using Unity.Netcode;
using UnityEngine;

namespace DZM
{
    public class MovementSystem : NetworkBehaviour, ISystem
    {
        [SerializeField]
        private CharacterMovementData inputBuffer;

        [SerializeField]
        private float speed = 5f;
        
        private void Update()
        {
            if (!IsServer)
                return;

            var input = inputBuffer.CurrentInput;

            var direction =
                new Vector3(input.MoveX, 0f, input.MoveY);

            transform.position += direction * speed * Time.deltaTime;
        }

        public void Setup()
        {
            throw new System.NotImplementedException();
        }

        public void Execute()
        {
            if (!IsServer)
                return;

            var input = inputBuffer.CurrentInput;

            var direction =
                new Vector3(input.MoveX, 0f, input.MoveY);

            transform.position += direction * speed * Time.deltaTime;
        }

        public void Cleanup()
        {
            throw new System.NotImplementedException();
        }
    }
}
