using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu]
    public class CuttingRecipe : UnityEngine.ScriptableObject
    {
        public KitchenObjectProperties input;
        public KitchenObjectProperties output;
        public int cuttingProgressNeeded;
    }
}