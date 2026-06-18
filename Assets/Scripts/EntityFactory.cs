using UnityEngine;

namespace DZM
{
    public class EntityFactory: ScriptableObject
    {
        [field: SerializeField] private Entity Prefab { get; set; }
        
        public Entity CreateEntity(Vector3 position, Quaternion rotation, Transform parent)
        {
            var instance = Instantiate(Prefab, position, rotation, parent);
            return instance.GetComponent<Entity>();
        }
    }
}