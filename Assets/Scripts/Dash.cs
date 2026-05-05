using UnityEngine;

namespace MaiNull
{
    public class Dash : MonoBehaviour
    {
        [SerializeField] private int dashSpeedMultiplier;
        [SerializeField] private float dashDuration;
        [SerializeField] private float cooldown;
        private bool _canDash = true;
        private PlayerController2D _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController2D>();
        }

        public void StartDash()
        {
            if (!_playerController || !_canDash) return;
            
            _playerController.MovementSpeedMultiplier += (uint)dashSpeedMultiplier;
            Invoke(nameof(ResetSpeedMultiplier), dashDuration);

            if (cooldown <= 0) return;
            
            _canDash = false;
            Invoke(nameof(ActivateDash), cooldown);
        }

        private void ActivateDash()
        {
            _canDash = true;
        }
        
        private void ResetSpeedMultiplier()
        {
            if (_playerController)
            {
                _playerController.MovementSpeedMultiplier -= (uint)dashSpeedMultiplier;
            }
        }
    }
}