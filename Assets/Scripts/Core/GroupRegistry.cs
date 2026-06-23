using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace DZM.Core
{
    [CreateAssetMenu(fileName = "GroupRegistry", menuName = "Authoring/GroupRegistry")]
    public class GroupRegistry : ScriptableObject
    {
        [Serializable]
        private class Binding
        {
            [field: SerializeField] public Group Group { get; private set; }

            [field: SerializeField] public RoleScope Scope { get; private set; }
        }

        [SerializeField] private List<Binding> _bindings = new();

        private readonly Dictionary<Type, IDataGroup> _processRoutes = new();

        private readonly Dictionary<Type, IDataGroup> _ownerRoutes = new();

        private bool _active;

        public void Activate(NetworkBehaviour process)
        {
            if (process == null)
            {
                throw new ArgumentNullException(nameof(process));
            }

            _processRoutes.Clear();
            _ownerRoutes.Clear();

            foreach (var binding in _bindings)
            {
                if (binding.Group is not IDataGroup dataGroup)
                {
                    Debug.LogError($"[GroupRegistry] {binding.Group?.name} does not implement IDataGroup");
                    continue;
                }

                binding.Group.Dispose();

                switch (binding.Scope)
                {
                    case RoleScope.Server:
                        if (process.IsServer)
                        {
                            _processRoutes[dataGroup.MemberType] = dataGroup;
                        }
                        break;
                    case RoleScope.Client:
                        if (process.IsClient)
                        {
                            _processRoutes[dataGroup.MemberType] = dataGroup;
                        }
                        break;
                    case RoleScope.Owner:
                        _ownerRoutes[dataGroup.MemberType] = dataGroup;
                        break;
                }
            }

            _active = true;
        }

        public void Deactivate()
        {
            if (!_active)
            {
                return;
            }

            foreach (var binding in _bindings)
            {
                if (binding.Group != null)
                {
                    binding.Group.Dispose();
                }
            }

            _processRoutes.Clear();
            _ownerRoutes.Clear();
            _active = false;
        }

        public void Register(NetworkBehaviour entity, IReadOnlyList<Component> data)
        {
            if (!_active)
            {
                Debug.LogWarning("[GroupRegistry] Register called before Activate");
                return;
            }

            foreach (var component in data)
            {
                if (component == null)
                {
                    continue;
                }

                var type = component.GetType();

                if (_processRoutes.TryGetValue(type, out var processGroup))
                {
                    processGroup.MarkForAddition(component);
                }

                if (entity.IsOwner && _ownerRoutes.TryGetValue(type, out var ownerGroup))
                {
                    ownerGroup.MarkForAddition(component);
                }
            }
        }

        public void Unregister(NetworkBehaviour entity, IReadOnlyList<Component> data)
        {
            if (!_active)
            {
                return;
            }

            foreach (var component in data)
            {
                if (component == null)
                {
                    continue;
                }

                var type = component.GetType();

                if (_processRoutes.TryGetValue(type, out var processGroup))
                {
                    processGroup.MarkForRemoval(component);
                }

                if (entity.IsOwner && _ownerRoutes.TryGetValue(type, out var ownerGroup))
                {
                    ownerGroup.MarkForRemoval(component);
                }
            }
        }
    }
}
