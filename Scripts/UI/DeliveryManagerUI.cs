using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private Transform orderedRecipesParent;
        [SerializeField] private GameObject orderedRecipeUIPrefab;

        private readonly List<OrderedRecipeUI> _orderedRecipeUis = new();

        private void Start()
        {
            while (orderedRecipesParent.childCount > 0)
            {
                DestroyImmediate(orderedRecipesParent.GetChild(0).gameObject);
            }


            DeliveryManager.Instance.OnRecipeListChanged += (_, args)=>
            {
                if (args.IsNewOrder)
                {
                    var orderedRecipeUI = Instantiate(orderedRecipeUIPrefab, orderedRecipesParent)
                        .GetComponent<OrderedRecipeUI>();

                    orderedRecipeUI.SetRecipeProperties(args.OrderRecipe);


                    _orderedRecipeUis.Add(orderedRecipeUI);
                }
                else
                {
                    foreach (var orderedRecipeUI in _orderedRecipeUis.Where(orderedRecipeUI=>
                                 args.OrderRecipe == orderedRecipeUI.OrderRecipe))
                    {
                        Destroy(orderedRecipeUI.gameObject);


                        _orderedRecipeUis.Remove(orderedRecipeUI);
                        break;
                    }
                }
            };
        }
    }
}