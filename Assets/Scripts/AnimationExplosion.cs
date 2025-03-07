using UnityEngine;

public class AnimationExplosion : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ExplosionAnimation(bool isPlaying)
    {
        _animator.SetBool(AnimatorData.Params.IsExplosion, isPlaying);
    }
}