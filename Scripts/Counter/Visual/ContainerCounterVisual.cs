using UnityEngine;

namespace Counter.Visual
{
    public class ContainerCounterVisual : MonoBehaviour
    {
        private static readonly int OpenClose = Animator.StringToHash("OpenClose");

        [SerializeField] private ContainerCounter containerCounter;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            containerCounter.OnPlayerGrabbedKitchenObject += (_, _)=>{ _animator.SetTrigger(OpenClose); };
        }
    }
}