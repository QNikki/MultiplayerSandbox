using System;
using UnityEngine;

namespace DZM.Core
{
    public interface IDataGroup
    {
        Type MemberType { get; }

        void MarkForAddition(Component member);

        void MarkForRemoval(Component member);
    }
}
