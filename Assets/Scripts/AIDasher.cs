using System;
using UnityEngine;

namespace MaiNull
{
    public class AIDasher : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speedMultiplier;
        [SerializeField] private float detectDistanceRadius;
        [SerializeField] private LayerMask layerMask;
        private Dash _dash;

        private void Awake()
        {
            _dash = new Dash(speedMultiplier);
        }

        private void Update()
        {
            if (!target || !_dash.CanDash()) return;

            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, target.position - transform.position, detectDistanceRadius, layerMask);
            if (hit2D)
            {
                
            }
        }
    }
}
