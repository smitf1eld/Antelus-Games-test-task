using UnityEngine;

public class DoorZoneTrigger : MonoBehaviour
{
    [SerializeField] private bool isFrontZone;
    [SerializeField] private DoorController doorController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorController.SetPlayerInFrontZone(isFrontZone);
        }
    }
}