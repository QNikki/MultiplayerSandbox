using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace DZM.Core
{
    [CreateAssetMenu(fileName = "Random_", menuName = "Authoring/Utils/Random")]
    public class RandomAuthoring : ScriptableObject
    {
        [field: SerializeField] public uint Seed { get; private set; }
        
        [field: SerializeField] private bool GenerateSeed { get; set; }

        public Random Random { get; private set; }

        internal void CreateRandom()
        {
            if (GenerateSeed)
            {
                var uniqueString = DateTime.Now + SystemInfo.deviceUniqueIdentifier;
                Seed = (uint)GenerateHash(uniqueString);
            }

            Random = new Random(Seed);
        }

        internal void Dispose()
        {
            Random = default;
        }
        
        private int GenerateHash(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToInt32(bytes, 0) & int.MaxValue;
        }
    }
}