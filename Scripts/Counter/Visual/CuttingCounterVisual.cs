using UnityEngine;

namespace Counter.Visual
{
    public class CuttingCounterVisual : MonoBehaviour
    {
        private static readonly int Cut = Animator.StringToHash("Cut");

        [SerializeField] private CuttingCounter cuttingCounter;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            cuttingCounter.OnProgressChanged += (_, args)=>
            {
                if (args.ProgressStart || args.ProgressStop)
                {
                    return;
                }

                _animator.SetTrigger(Cut);
            };
        }
    }
}