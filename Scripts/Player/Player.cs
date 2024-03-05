using System;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IKitchenObjectHolder
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private GameInput gameInput;
        [SerializeField] private Transform kitchenObjectPositionTransform;
        private KitchenObject.KitchenObject _kitchenObjectInHand;

        private Transform _playerTransform;

        private Counter.Counter _selectedCounter;

        public bool IsWalking { get; private set; }
        public static Player Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one Player instance in the scene!");
            }

            Instance = this;

            _playerTransform = transform;
        }

        private void Start()
        {
            gameInput.OnInteractAction += (_, _)=>
            {
                if (_selectedCounter != null && GameManager.Instance.IsGamePlaying)
                {
                    _selectedCounter.Interact(this);
                }
            };


            gameInput.OnInteractAlternateAction += (_, _)=>
            {
                if (_selectedCounter != null && GameManager.Instance.IsGamePlaying)
                {
                    _selectedCounter.InteractAlternate(this);
                }
            };
        }

        private void Update()
        {
            HandleMovement();
            HandleInteractions();
        }

        public Transform GetHolderTransform()=>kitchenObjectPositionTransform;

        public void SetKitchenObject(KitchenObject.KitchenObject kitchenObject)
        {
            _kitchenObjectInHand = kitchenObject;

            if (kitchenObject != null)
            {
                OnPickedSomething?.Invoke(this, EventArgs.Empty);
            }
        }

        public KitchenObject.KitchenObject GetKitchenObject()=>_kitchenObjectInHand;

        public void ClearKitchenObject()
        {
            _kitchenObjectInHand = null;
        }

        public bool HasKitchenObject()=>_kitchenObjectInHand != null;

        public static event EventHandler OnPickedSomething;

        public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChanged;

        private void HandleInteractions()
        {
            const float interactionDistance = 2f;
            var notCastAnything = !Physics.Raycast(_playerTransform.position, _playerTransform.forward,
                out var rayCastHit,
                interactionDistance);
            if (notCastAnything)
            {
                SetSelectedCounter(null);
                return;
            }


            var isNotCastedCounter = !rayCastHit.transform.TryGetComponent(out Counter.Counter counter);
            if (isNotCastedCounter)
            {
                SetSelectedCounter(null);
            }
            else if (counter != _selectedCounter)
            {
                SetSelectedCounter(counter);
            }
        }

        private void SetSelectedCounter(Counter.Counter counter)
        {
            _selectedCounter = counter;
            OnSelectedCounterChanged?.Invoke(this, new SelectedCounterChangedEventArgs
            {
                SelectedCounter = counter
            });
        }

        private void HandleMovement()
        {
            var inputVector = gameInput.GetMovementVector();
            var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

            if (moveDirection != Vector3.zero)
            {
                IsWalking = true;
            }
            else
            {
                IsWalking = false;
                return;
            }


            _playerTransform.forward =
                Vector3.Slerp(_playerTransform.forward, moveDirection, Time.deltaTime * rotateSpeed);


            var playerPosition = _playerTransform.position;
            var moveDistance = moveSpeed * Time.deltaTime;

            if (CanNotMoveTo(moveDirection))
            {
                // If we can't move diagonally, try moving horizontally or vertically.
                if (moveDirection.x != 0 && CanNotMoveTo(new Vector3(inputVector.x, 0, 0)))
                {
                    moveDirection.x = 0;
                }

                if (moveDirection.z != 0 && CanNotMoveTo(new Vector3(0, 0, inputVector.y)))
                {
                    moveDirection.z = 0;
                }
            }


            _playerTransform.Translate(moveDirection.normalized * moveDistance, Space.World);


            return;


            bool CanNotMoveTo(Vector3 direction)
            {
                const float playerRadius = 0.7f;
                const float playerHeight = 2f;
                return Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight,
                    playerRadius, direction, moveDistance);
            }
        }

        public class SelectedCounterChangedEventArgs : EventArgs
        {
            public Counter.Counter SelectedCounter;
        }
    }
}