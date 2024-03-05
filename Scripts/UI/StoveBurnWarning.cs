using Counter;
using UnityEngine;

namespace UI
{
    public class StoveBurnWarning : MonoBehaviour
    {
        [SerializeField] private StoveCounter stoveCounter;
        [SerializeField] private float burnWarningThreshold = 0.5f;

        public float BurnWarningThreshold=>burnWarningThreshold;


        private void Start()
        {
            stoveCounter.OnProgressChanged += (_, args)=>
            {
                if (stoveCounter.IsBurning && args.ProgressNormalized <= burnWarningThreshold)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            };

            Hide();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}