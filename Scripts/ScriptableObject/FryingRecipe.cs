using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu]
    public class FryingRecipe : UnityEngine.ScriptableObject
    {
        public KitchenObjectProperties input;
        public float fryingTimeNeeded;
        public KitchenObjectProperties fried;
        public float burningTimeNeeded;
        public KitchenObjectProperties burned;
    }
}