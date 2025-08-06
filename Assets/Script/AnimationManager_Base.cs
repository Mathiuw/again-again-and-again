using UnityEngine;

public class AnimationManager_Base : MonoBehaviour
{
    protected Animator _animator;
    protected string _currentAnimation;

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimation(string animation)
    {
        if (_currentAnimation != animation)
        {
            _currentAnimation = animation;
            _animator.Play(animation);
        }
    }
}
