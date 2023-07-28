using Player;
using UnityEngine;

namespace Audio
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private float maxFootStepsTime = .1f; 
        
        private PlayerController _playerController;
        private float _footStepsTimer;
        

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            _footStepsTimer -= Time.deltaTime;

            if (_footStepsTimer <= 0f)
            {
                _footStepsTimer = maxFootStepsTime;
                
                if (_playerController.IsWalking)
                {
                    SoundManager.Instance.PlayFootstepsSound(transform.position);
                }
            }
        }
    }
}
