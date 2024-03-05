using UnityEngine;

namespace Player
{
    public class PlayerFootstepSound : MonoBehaviour
    {
        [SerializeField] private float footstepTimerMax = 0.1f;
        [SerializeField] private float footstepVolume = 1f;
        private float _footstepTimer;
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            _footstepTimer -= Time.deltaTime;
            if (_footstepTimer <= 0 && _player.IsWalking)
            {
                _footstepTimer = footstepTimerMax;


                SoundManager.Instance.PlayFootstepSound(_player.transform.position, footstepVolume);
            }
        }
    }
}