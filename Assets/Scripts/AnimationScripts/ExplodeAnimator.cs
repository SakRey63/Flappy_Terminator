using UnityEngine;

public class ExplodeAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ExplosionAnimation(bool isPlaying)
    {
        _animator.SetBool(AnimatorData.Params.Explosion, isPlaying);
    }
}