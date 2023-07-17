using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
    
        private Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(IsWalking, player.IsWalking);
        }

        private void Update()
        {
            _animator.SetBool(IsWalking, player.IsWalking);
        }
    }
}
