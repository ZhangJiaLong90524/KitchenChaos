using System.Collections.Generic;
using ScriptableObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class OrderedRecipeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipeNameLabel;
        [SerializeField] private GameObject ingredientIconPrefab;
        [SerializeField] private Transform ingredientIconsParent;

        public OrderRecipe OrderRecipe { get; private set; }


        public void SetRecipeProperties(OrderRecipe orderRecipe)
        {
            OrderRecipe = orderRecipe;


            recipeNameLabel.text = orderRecipe.recipeName;


            var kitchenObjectPropertiesBuffer =
                new List<KitchenObjectProperties>(orderRecipe.kitchenObjectPropertiesList);
            do
            {
                var randomIndex = Random.Range(0, kitchenObjectPropertiesBuffer.Count);
                var randomKitchenObjectProperties =
                    kitchenObjectPropertiesBuffer[randomIndex];
                Instantiate(ingredientIconPrefab, ingredientIconsParent).GetComponent<Image>().sprite =
                    randomKitchenObjectProperties.sprite;


                kitchenObjectPropertiesBuffer.RemoveAt(randomIndex);
            } while (kitchenObjectPropertiesBuffer.Count > 0);
        }
    }
}