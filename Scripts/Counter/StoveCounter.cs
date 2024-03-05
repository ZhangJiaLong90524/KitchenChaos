using System;
using ScriptableObject;
using UnityEngine;

namespace Counter
{
    public class StoveCounter : Counter, IHasProgress
    {
        public enum FryingState
        {
            Idle,
            Frying,
            Burning
        }

        [SerializeField] private FryingRecipe[] fryingRecipes;
        private FryingRecipe _currentFryingRecipe;

        private float _fryingTimer;
        private FryingState _state = FryingState.Idle;

        public bool IsBurning=>_state == FryingState.Burning;

        private void Update()
        {
            switch (_state)
            {
                case FryingState.Idle:
                    OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs
                    {
                        ProgressStop = true
                    });
                    break;
                case FryingState.Frying:
                    Fry(_currentFryingRecipe.fried);
                    OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs
                    {
                        ProgressNormalized = _fryingTimer / _currentFryingRecipe.fryingTimeNeeded
                    });
                    break;
                case FryingState.Burning:
                    Fry(_currentFryingRecipe.burned);
                    OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs
                    {
                        ProgressNormalized = _fryingTimer / _currentFryingRecipe.burningTimeNeeded
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public event EventHandler<IHasProgress.ProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<StateChangedEventArgs> OnStateChanged;

        public override void Interact(Player.Player player)
        {
            base.Interact(player);

            UpdateFryingState();
        }

        private void Fry(KitchenObjectProperties possibleOutput)
        {
            _fryingTimer -= Time.deltaTime;

            if (_fryingTimer <= 0)
            {
                KitchenObjectOnTop.DestroySelf();
                KitchenObject.KitchenObject.Spawn(possibleOutput, this);
                UpdateFryingState();
            }
        }

        private void UpdateFryingState()
        {
            if (!HasKitchenObject())
            {
                SetState(FryingState.Idle);
                return;
            }


            var kitchenObjectProperties = KitchenObjectOnTop.GetProperties();
            foreach (var fryingRecipe in fryingRecipes)
            {
                if (fryingRecipe.input == kitchenObjectProperties)
                {
                    SetState(FryingState.Frying);

                    _fryingTimer = fryingRecipe.fryingTimeNeeded;
                    _currentFryingRecipe = fryingRecipe;

                    OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs
                    {
                        ProgressStart = true
                    });
                    return;
                }


                if (fryingRecipe.fried == kitchenObjectProperties)
                {
                    SetState(FryingState.Burning);

                    _fryingTimer = fryingRecipe.burningTimeNeeded;
                    _currentFryingRecipe = fryingRecipe;

                    OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedEventArgs
                    {
                        ProgressStart = true
                    });
                    return;
                }
            }


            SetState(FryingState.Idle);
        }

        private void SetState(FryingState state)
        {
            _state = state;

            OnStateChanged?.Invoke(this, new StateChangedEventArgs
            {
                State = state
            });
        }

        public class StateChangedEventArgs : EventArgs
        {
            public FryingState State;
        }
    }
}