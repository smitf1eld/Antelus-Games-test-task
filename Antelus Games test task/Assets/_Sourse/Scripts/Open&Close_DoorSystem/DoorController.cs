using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private GameObject pressEText;
    private bool lastOpenedFront; // Запоминает, в какую сторону последний раз открывалась дверь
    [Header("Settings")]
    [SerializeField] private float interactionDistance = 2f;

    private bool isAnimating = false;
    private bool isOpen = false;
    private bool isPlayerInFrontZone = false;

    public void SetPlayerInFrontZone(bool value) => isPlayerInFrontZone = value;

    private void Update()
    {
        if (isAnimating) return;

        float distance = Vector3.Distance(transform.position, PlayerInstance.Instance.transform.position);
        bool canInteract = distance <= interactionDistance;

        pressEText.SetActive(canInteract && !isAnimating);

        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(InteractWithDoor());
            PlayerInstance.Instance.PlayerAnimator.PlayReachAnimation();
        }
    }

    private IEnumerator InteractWithDoor()
    {
        isAnimating = true;
        pressEText.SetActive(false);
        

        string animationName;
        if (isOpen)
        {
            // Закрываем дверь с учетом текущего направления
            animationName = lastOpenedFront ? "DoorCloseFromFront" : "DoorCloseFromBack";
        }
        else
        {
            // Открываем в нужную сторону
            animationName = isPlayerInFrontZone ? "DoorOpenFront" : "DoorOpenBack";
            lastOpenedFront = isPlayerInFrontZone; // Запоминаем сторону открытия
        }

        doorAnimator.Play(animationName);
        yield return new WaitForSeconds(doorAnimator.GetCurrentAnimatorStateInfo(0).length);
    
        isOpen = !isOpen;
        isAnimating = false;
    }

    public void HidePrompt() => pressEText.SetActive(false);
}
