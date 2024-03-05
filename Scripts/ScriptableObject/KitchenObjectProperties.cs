using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu]
    public class KitchenObjectProperties : UnityEngine.ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public string objectName;
    }
}