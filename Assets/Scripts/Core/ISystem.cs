using UnityEngine;

namespace DZM.Core
{
    public interface ISystem
    {
        public void Setup();
        
        public void Execute();

        public void Cleanup();
    }

    public abstract class System<T>: ISystem where T : MonoBehaviour
    {
        protected DataGroupAuthoring<T> DataGroup;

        protected System(DataGroupAuthoring<T> dataGroup)
        {
            DataGroup = dataGroup;
        }

        public abstract void Setup();

        public virtual void Execute()
        {
            DataGroup.UpdateGroup();
        }

        public abstract void Cleanup();
    }
}