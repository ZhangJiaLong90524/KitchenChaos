using System;
using KitchenObject;
using UnityEngine;

namespace Counter
{
    public abstract class Counter : MonoBehaviour, IKitchenObjectHolder
    {
        [SerializeField] protected Transform kitchenObjectPositionTransform;
        protected KitchenObject.KitchenObject KitchenObjectOnTop;

        public Transform GetHolderTransform()=>kitchenObjectPositionTransform;

        public void SetKitchenObject(KitchenObject.KitchenObject kitchenObject)
        {
            KitchenObjectOnTop = kitchenObject;


            if (kitchenObject != null)
            {
                OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
            }
        }

        public KitchenObject.KitchenObject GetKitchenObject()=>KitchenObjectOnTop;

        public void ClearKitchenObject()
        {
            KitchenObjectOnTop = null;
        }

        public bool HasKitchenObject()=>KitchenObjectOnTop != null;

        public static event EventHandler OnAnyObjectPlacedHere;

        public static void InitializeStaticData()
        {
            OnAnyObjectPlacedHere = null;
        }

        public virtual void Interact(Player.Player player)
        {
            switch (player.HasKitchenObject(), HasKitchenObject())
            {
                case (true, false):
                    player.GetKitchenObject().ChangeHolder(this, player);
                    OnMovedKitchenObjectFromPlayerToCounter(player);
                    break;
                case (false, true):
                    KitchenObjectOnTop.ChangeHolder(player, this);
                    OnMovedKitchenObjectFromCounterToPlayer(player);
                    break;
                case (true, true):
                    if (player.GetKitchenObject() is Plate playerPlate &&
                        playerPlate.TryAddIngredient(KitchenObjectOnTop.GetProperties()))
                    {
                        KitchenObjectOnTop.DestroySelf();
                    }
                    else if (KitchenObjectOnTop is Plate counterPlate &&
                             counterPlate.TryAddIngredient(player.GetKitchenObject().GetProperties()))
                    {
                        player.GetKitchenObject().DestroySelf();
                    }

                    break;
            }
        }

        protected virtual void OnMovedKitchenObjectFromPlayerToCounter(Player.Player player)
        {
        }

        protected virtual void OnMovedKitchenObjectFromCounterToPlayer(Player.Player player)
        {
        }

        public virtual void InteractAlternate(Player.Player player)
        {
        }
    }
}