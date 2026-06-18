using UnityEngine;

namespace DZM.Core
{
    public abstract class DataGroupAuthoring<T> : GroupAuthoring<T> where T : MonoBehaviour
    {
        protected override void Add()
        {
            if (ToAdd.Count <= 0)
            {
                return;
            }

            foreach (var member in ToAdd)
            {
                Members.Add(member);
            }

            ToAdd.Clear();
        }

        protected override void Remove()
        {
            if (ToRemove.Count <= 0)
            {
                return;
            }

            foreach (var member in ToRemove)
            {
                Members.Remove(member);
            }

            ToRemove.Clear();
        }
    }
}