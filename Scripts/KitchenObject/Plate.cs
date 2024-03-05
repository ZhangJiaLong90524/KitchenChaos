using System;
using System.Collections.Generic;
using ScriptableObject;
using UnityEngine;

namespace KitchenObject
{
    public class Plate : KitchenObject
    {
        [SerializeField] private List<KitchenObjectProperties> availableKitchenObjectList;
        public readonly List<KitchenObjectProperties> KitchenObjectsInPlate = new();
        private bool _isMeatAdded;

        public event EventHandler<IngredientAddedEventArgs> OnIngredientAdded;

        public bool TryAddIngredient(KitchenObjectProperties kitchenObjectProperties)
        {
            if (KitchenObjectsInPlate.Contains(kitchenObjectProperties) ||
                !availableKitchenObjectList.Contains(kitchenObjectProperties))
            {
                return false;
            }


            if (kitchenObjectProperties.objectName.Contains("Meat Patty"))
            {
                if (_isMeatAdded)
                {
                    return false;
                }

                _isMeatAdded = true;
            }


            KitchenObjectsInPlate.Add(kitchenObjectProperties);


            OnIngredientAdded?.Invoke(this, new IngredientAddedEventArgs
            {
                AddedKitchenObjectProperties = kitchenObjectProperties
            });


            return true;
        }

        public class IngredientAddedEventArgs : EventArgs
        {
            public KitchenObjectProperties AddedKitchenObjectProperties;
        }
    }
}