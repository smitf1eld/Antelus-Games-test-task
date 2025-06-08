using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayReachAnimation()
    {
        animator.Play("Reach");
    }
}