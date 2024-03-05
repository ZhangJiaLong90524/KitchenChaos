using TMPro;
using UnityEngine;

namespace UI
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        private static readonly int NumberPopup = Animator.StringToHash("NumberPopup");
        [SerializeField] private TextMeshProUGUI countdownText;
        private Animator _animator;
        private int _previousCountdownTime;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            GameManager.Instance.OnStateChanged += (_, _)=>
            {
                if (GameManager.Instance.IsCountdownToStart)
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

        private void Update()
        {
            var countdownTimer = Mathf.CeilToInt(GameManager.Instance.CountdownTimer);

            countdownText.text = countdownTimer.ToString();


            if (_previousCountdownTime != countdownTimer)
            {
                _previousCountdownTime = countdownTimer;


                _animator.SetTrigger(NumberPopup);


                SoundManager.Instance.PlayCountdownSound();
            }
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