using System;
using System.Linq;
using ScriptableObject;
using UnityEngine;

namespace Counter
{
    public class CuttingCounter : Counter, IHasProgress
    {
        [SerializeField] private CuttingRecipe[] cuttingRecipes;
        private CuttingRecipe _currentCuttingRecipe;

        private int _cuttingProgress;

        public event EventHandler<IHasProgress.ProgressChangedEventArgs> OnProgressChanged;
        public static event EventHandler OnCut;

        public new static void InitializeStaticData()
        {
            OnCut = null;
        }

        protected override void OnMovedKitchenObjectFromPlayerToCounter(Player.Player player)
        {
            UpdateCurrentCuttingRecipe();


            if (_currentCuttingRecipe == null)
            {
                return;
            }


            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs
            {
                ProgressStart = true
            });
        }

        protected override void OnMovedKitchenObjectFromCounterToPlayer(Player.Player player)
        {
            if (_currentCuttingRecipe == null)
            {
                return;
            }

            StopCutting();
        }

        public override void InteractAlternate(Player.Player player)
        {
            Cut(player);
        }

        private void Cut(Player.Player player)
        {
            if (!HasKitchenObject() || player.HasKitchenObject() || _currentCuttingRecipe == null)
            {
                return;
            }


            UpdateCuttingProgress(1);


            if (_cuttingProgress < _currentCuttingRecipe.cuttingProgressNeeded)
            {
                return;
            }


            KitchenObjectOnTop.DestroySelf();


            KitchenObject.KitchenObject.Spawn(_currentCuttingRecipe.output, this);


            StopCutting();
        }

        private void UpdateCurrentCuttingRecipe()
        {
            _currentCuttingRecipe = cuttingRecipes.FirstOrDefault(cuttingRecipe=>
                cuttingRecipe.input == KitchenObjectOnTop.GetProperties());
        }


        private void StopCutting()
        {
            _cuttingProgress = 0;

            _currentCuttingRecipe = null;

            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs
            {
                ProgressStop = true
            });
        }

        private void UpdateCuttingProgress(int increment)
        {
            _cuttingProgress += increment;


            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs
            {
                ProgressNormalized = (float)_cuttingProgress / _currentCuttingRecipe.cuttingProgressNeeded
            });


            OnCut?.Invoke(this, EventArgs.Empty);
        }
    }
}