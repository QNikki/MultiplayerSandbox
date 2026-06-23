using UnityEngine;

namespace DZM.Core
{
    public abstract class SystemBase : ScriptableObject, ISystem
    {
        public abstract void Setup();

        public abstract void Execute();

        public abstract void Cleanup();
    }

    public abstract class DataSystem<T> : SystemBase where T : MonoBehaviour
    {
        [SerializeField] protected DataGroupAuthoring<T> DataGroup;
    }
}
