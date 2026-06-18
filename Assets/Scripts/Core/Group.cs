
using UnityEngine;

namespace DZM.Core
{
    public abstract class Group: ScriptableObject
    {
        public abstract void UpdateGroup();

        public abstract void Dispose();
    }
}