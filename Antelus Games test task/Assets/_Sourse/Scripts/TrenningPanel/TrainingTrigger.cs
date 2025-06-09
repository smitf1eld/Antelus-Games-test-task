using UnityEngine;

public class TrainingTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            TrainingManager.Instance.StartTraining();
        }
    }
}