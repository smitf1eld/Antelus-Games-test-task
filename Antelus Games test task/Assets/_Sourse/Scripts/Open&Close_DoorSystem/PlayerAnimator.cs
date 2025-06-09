using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayReachAnimation()
    {
        animator.SetTrigger("Reach");
    }
}