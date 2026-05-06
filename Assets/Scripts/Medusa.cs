using System.Collections;
using UnityEngine;

namespace MaiNull
{
    public class Medusa : Enemy
    {
        protected override void Die()
        {
            Animator animator = GetComponentInChildren<Animator>();
            animator.Play("Medusa_Die");
        }
    }
}