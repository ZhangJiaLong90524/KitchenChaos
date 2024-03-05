using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu]
    public class OrderRecipe : UnityEngine.ScriptableObject
    {
        public List<KitchenObjectProperties> kitchenObjectPropertiesList;

        public string recipeName;
    }
}