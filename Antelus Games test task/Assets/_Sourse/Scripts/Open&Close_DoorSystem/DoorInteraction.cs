using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] private DoorController doorController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorController.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorController.enabled = false;
            doorController.HidePrompt();
        }
    }
}