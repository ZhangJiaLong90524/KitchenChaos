using System;
using ScriptableObject;
using UnityEngine;

namespace Counter
{
    public class PlatesCounter : Counter
    {
        [SerializeField] private KitchenObjectProperties plateProperties;

        [SerializeField] private int maxPlates = 4;
        [SerializeField] private float spawnPlateDelay = 4f;
        private int _plateAmount;
        private float _spawnPlateTimer;

        private void Update()
        {
            _spawnPlateTimer -= Time.deltaTime;

            if (GameManager.Instance.IsGamePlaying && _spawnPlateTimer < 0 && _plateAmount < maxPlates)
            {
                ChangePlateAmount(1);

                _spawnPlateTimer = spawnPlateDelay;
            }
        }

        public event EventHandler<PlateAmountChangedEventArgs> OnPlateAmountChanged;

        public override void Interact(Player.Player player)
        {
            if (!player.HasKitchenObject() && _plateAmount > 0)
            {
                ChangePlateAmount(-1);


                KitchenObject.KitchenObject.Spawn(plateProperties, player);
            }
        }

        private void ChangePlateAmount(int delta)
        {
            _plateAmount += delta;

            OnPlateAmountChanged?.Invoke(this, new PlateAmountChangedEventArgs
            {
                PlateAmount = _plateAmount
            });
        }

        public class PlateAmountChangedEventArgs : EventArgs
        {
            public int PlateAmount;
        }
    }
}