using System;
using UnityEngine;

namespace DZM.Core
{
    public abstract class DataGroupAuthoring<T> : GroupAuthoring<T>, IDataGroup where T : MonoBehaviour
    {
        public Type MemberType => typeof(T);

        void IDataGroup.MarkForAddition(Component member)
        {
            if (member is T typed)
            {
                MarkForAddition(typed);
            }
        }

        void IDataGroup.MarkForRemoval(Component member)
        {
            if (member is T typed)
            {
                MarkForRemoval(typed);
            }
        }

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
