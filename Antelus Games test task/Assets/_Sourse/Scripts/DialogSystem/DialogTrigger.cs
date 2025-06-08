using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogSystem dialogSystem;
    public float interactionDistance = 3f;
    
    private Transform playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerTransform = player.transform;
    }

    private void Update()
    {
        if (dialogSystem == null || playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        bool isClose = distance <= interactionDistance;

        if (dialogSystem.GetComponent<DialogManager>().pressEPrompt != null)
            dialogSystem.GetComponent<DialogManager>().pressEPrompt.SetActive(isClose && !dialogSystem.IsDialogActive());

        if (isClose && Input.GetKeyDown(KeyCode.E) && !dialogSystem.IsDialogActive())
            dialogSystem.StartDialog();
    }
}