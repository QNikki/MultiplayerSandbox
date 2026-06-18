using UnityEngine;

namespace DZM.Core
{
    [CreateAssetMenu(fileName = "SystemGroup_", menuName = "Authoring/Group/System")]
    public class SystemGroupAuthoring: GroupAuthoring<ISystem>
    {
        private bool _state = false;
        
        public void Setup()
        {
            foreach (var member in Members)
            {
                member.Setup();
            }

            _state = true;
        }

        public override void UpdateGroup()
        {
            base.UpdateGroup();
            foreach (var member in Members)
            {
                member.Execute();
            }
        }
        
        public void Cleanup()
        {
            foreach (var member in Members)
            {
                member.Cleanup();
            }

            _state = false;
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
                if (_state)
                {
                    member.Setup();
                }
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
                if (_state)
                {
                    member.Cleanup();
                }
            }
            
            ToRemove.Clear();
        }
    }
}