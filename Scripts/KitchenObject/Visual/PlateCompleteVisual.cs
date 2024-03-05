using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObject;
using UnityEngine;

namespace KitchenObject.Visual
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [SerializeField] private Plate plate;

        [SerializeField]
        private List<KitchenObjectPropertiesToVisualGameObject> kitchenObjectPropertiesToVisualMap = new();

        private void Start()
        {
            plate.OnIngredientAdded += (_, args)=>
            {
                foreach (var kitchenObjectPair in kitchenObjectPropertiesToVisualMap.Where(kitchenObjectPair=>
                             kitchenObjectPair.kitchenObjectProperties == args.AddedKitchenObjectProperties))
                {
                    kitchenObjectPair.kitchenObjectVisualGameObject.SetActive(true);
                }
            };


            foreach (var kitchenObjectPropertiesToVisualGameObject in kitchenObjectPropertiesToVisualMap)
            {
                kitchenObjectPropertiesToVisualGameObject.kitchenObjectVisualGameObject.SetActive(false);
            }
        }

        [Serializable]
        private struct KitchenObjectPropertiesToVisualGameObject
        {
            public KitchenObjectProperties kitchenObjectProperties;
            public GameObject kitchenObjectVisualGameObject;
        }
    }
}