using Counter;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progressBar;

        [SerializeField] private GameObject hasProgressGameObject;
        private IHasProgress _hasProgress;

        private void Start()
        {
            _hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

            if (_hasProgress == null)
            {
                Debug.LogError("Game Object " + hasProgressGameObject +
                               " does not have a component that implements IHasProgress.");
                return;
            }


            _hasProgress.OnProgressChanged += (_, args)=>
            {
                if (args.ProgressStart)
                {
                    Show();
                }
                else if (args.ProgressStop)
                {
                    progressBar.fillAmount = 0f;

                    Hide();
                }
                else
                {
                    progressBar.fillAmount = args.ProgressNormalized;
                }
            };


            progressBar.fillAmount = 0f;

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