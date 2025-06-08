using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance Instance;
    public PlayerAnimator PlayerAnimator { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerAnimator = GetComponentInChildren<PlayerAnimator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}