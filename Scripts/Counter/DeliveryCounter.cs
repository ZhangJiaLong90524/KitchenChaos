using KitchenObject;

namespace Counter
{
    public class DeliveryCounter : Counter
    {
        public static DeliveryCounter Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public override void Interact(Player.Player player)
        {
            if (player.HasKitchenObject() && player.GetKitchenObject() is Plate plate)
            {
                DeliveryManager.Instance.ReadyForPickUp(plate);


                plate.DestroySelf();
            }
        }
    }
}