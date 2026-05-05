using System.Collections;
using UnityEngine;

namespace MaiNull
{
    public class Dash
    {
        private readonly float _speedMultiplier;
        private bool _canDash = true;
        
        public bool IsDashing { get; private set; }

        public Dash(float speedMultiplier)
        {
            _speedMultiplier = speedMultiplier;
        }
        
        public float CurrentSpeedMultiplier => IsDashing ? _speedMultiplier : 1f;
        
        public IEnumerator DashCoroutine(float duration, float cooldown)
        {
            if (!CanDash()) yield break;
            
            IsDashing = true;
            _canDash = false;
            
            yield return new WaitForSeconds(duration);
            IsDashing = false;

            if (cooldown != 0)
            {
                yield return new WaitForSeconds(cooldown);
            }
            
            _canDash = true;
        }

        public bool CanDash()
        {
            return _canDash && !IsDashing;
        }
    }
}