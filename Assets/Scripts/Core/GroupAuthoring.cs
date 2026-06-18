using System;
using System.Collections.Generic;

namespace DZM.Core
{
    public abstract class GroupAuthoring<TMember>: Group
    {
        protected readonly List<TMember> Members = new();

        protected readonly HashSet<TMember> ToAdd = new HashSet<TMember>();

        protected readonly HashSet<TMember> ToRemove = new HashSet<TMember>();

        public IReadOnlyList<TMember> ROMembers => Members;
        
        public  void MarkForAddition(TMember member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (!Members.Contains(member))
            {
                ToAdd.Add(member);
            }

            ToRemove.Remove(member);
        }
        
        public  void MarkForRemoval(TMember member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (Members.Contains(member))
            {
                ToRemove.Add(member);
            }
            
            ToAdd.Remove(member);
        }

        public override  void UpdateGroup()
        {
            Add();
            Remove();  
        }

        public override void Dispose()
        {
            Members.Clear();
            ToAdd.Clear();
            ToRemove.Clear();
        }

        protected abstract void Add();

        protected abstract void Remove();
    }
}