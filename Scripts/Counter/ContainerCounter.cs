using System;
using ScriptableObject;
using UnityEngine;
using UnityEngine.Serialization;

namespace Counter
{
    public class ContainerCounter : Counter
    {
        [FormerlySerializedAs("kitchenObjectSo")]
        [FormerlySerializedAs("kitchenObjectScriptableObject")]
        [SerializeField]
        private KitchenObjectProperties kitchenObjectProperties;

        public event EventHandler OnPlayerGrabbedKitchenObject;

        public override void Interact(Player.Player player)
        {
            if (player.HasKitchenObject())
            {
                return;
            }


            KitchenObject.KitchenObject.Spawn(kitchenObjectProperties, player);


            OnPlayerGrabbedKitchenObject?.Invoke(this, EventArgs.Empty);
        }
    }
}