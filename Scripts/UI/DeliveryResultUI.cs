using UnityEngine;

namespace UI
{
    public class DeliveryResultUI : MonoBehaviour
    {
        private static readonly int BannerPopup = Animator.StringToHash("BannerPopup");
        [SerializeField] private GameObject deliverySuccessBanner;
        [SerializeField] private GameObject deliveryFailedBanner;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            DeliveryManager.Instance.OnRecipeSuccess += (_, _)=>
            {
                deliveryFailedBanner.SetActive(false);
                deliverySuccessBanner.SetActive(true);


                _animator.SetTrigger(BannerPopup);
            };
            DeliveryManager.Instance.OnRecipeFailed += (_, _)=>
            {
                deliverySuccessBanner.SetActive(false);
                deliveryFailedBanner.SetActive(true);


                _animator.SetTrigger(BannerPopup);
            };


            deliverySuccessBanner.SetActive(false);
            deliveryFailedBanner.SetActive(false);
        }
    }
}