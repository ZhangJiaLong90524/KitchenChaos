using Counter;
using UnityEngine;

namespace UI
{
    public class StoveBurnProgressBarFlash : MonoBehaviour
    {
        private static readonly int IsFlashing = Animator.StringToHash("IsFlashing");


        [SerializeField] private StoveCounter stoveCounter;
        [SerializeField] private StoveBurnWarning stoveBurnWarning;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        private void Start()
        {
            stoveCounter.OnProgressChanged += (_, args)=>
            {
                if (stoveCounter.IsBurning && args.ProgressNormalized <= stoveBurnWarning.BurnWarningThreshold)
                {
                    _animator.SetBool(IsFlashing, true);
                }
                else
                {
                    _animator.SetBool(IsFlashing, false);
                }
            };
        }
    }
}