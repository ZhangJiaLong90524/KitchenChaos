using UI;
using UnityEngine;

namespace Counter
{
    public class StoveCounterSound : MonoBehaviour
    {
        [SerializeField] private StoveBurnWarning stoveBurnWarning;
        [SerializeField] private float stoveBurnWarningSoundPeriod = 0.2f;
        private AudioSource _audioSource;

        private bool _isPlayingWarningSound;
        private StoveCounter _stoveCounter;
        private float _warningSoundTimer;

        private void Awake()
        {
            _stoveCounter = GetComponentInParent<StoveCounter>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _stoveCounter.OnStateChanged += (_, args)=>
            {
                if (args.State is StoveCounter.FryingState.Frying or StoveCounter.FryingState.Burning)
                {
                    _audioSource.Play();
                }
                else
                {
                    _audioSource.Pause();
                }
            };

            _stoveCounter.OnProgressChanged += (_, args)=>
            {
                _isPlayingWarningSound = _stoveCounter.IsBurning &&
                                         args.ProgressNormalized <= stoveBurnWarning.BurnWarningThreshold;
            };
        }

        private void Update()
        {
            if (!_isPlayingWarningSound)
            {
                return;
            }

            _warningSoundTimer -= Time.deltaTime;

            if (_warningSoundTimer <= 0)
            {
                _warningSoundTimer = stoveBurnWarningSoundPeriod;


                SoundManager.Instance.PlayStoveBurnWarningSound();
            }
        }
    }
}