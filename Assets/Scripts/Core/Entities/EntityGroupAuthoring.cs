using System;
using Unity.Collections;
using UnityEngine;

namespace DZM.Core.Entities
{
    [CreateAssetMenu(fileName = "EntityGroup", menuName = "Authoring/Group/Entity")]
    public class EntityGroupAuthoring: GroupAuthoring<IEntity>
    {
        public override void MarkForAddition(IEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Id == -1)
            {
                ToAdd.Add(entity);
            }

            ToRemove.Remove(entity);
        }
        
        public override void MarkForRemoval(IEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Id != -1)
            {
                ToRemove.Add(entity);
            }
            
            ToAdd.Remove(entity);
        }
        
        protected override void Add()
        {
            if (ToAdd.Count <= 0)
            {
                return;
            }

            foreach (var entity in ToAdd)
            {
                Members.Add(entity);
                entity.Id = Members.Count - 1;
            }
            
            ToAdd.Clear();
        }

        protected override void Remove()
        {
            if (ToRemove.Count <= 0)
            {
                return;
            }

            foreach (var entity in ToRemove)
            {
                var lastTarget = Members[^1];
                Members.RemoveAtSwapBack(entity.Id);
                lastTarget.Id = entity.Id;
                entity.Id = -1;
            }
            
            ToRemove.Clear();
        }
    }
}