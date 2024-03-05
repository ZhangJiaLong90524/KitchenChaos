using System;
using UnityEngine;

namespace Counter
{
    public class TrashCounter : Counter
    {
        [SerializeField] private float destructionDelay = 0.2f;
        public static event EventHandler OnAnyObjectTrashed;

        public new static void InitializeStaticData()
        {
            OnAnyObjectTrashed = null;
        }

        public override void Interact(Player.Player player)
        {
            if (!player.HasKitchenObject())
            {
                return;
            }


            var kitchenObject = player.GetKitchenObject();

            kitchenObject.ChangeHolder(this, player);


            kitchenObject.GetComponent<Rigidbody>().useGravity = true;


            kitchenObject.DestroySelf(destructionDelay);


            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}