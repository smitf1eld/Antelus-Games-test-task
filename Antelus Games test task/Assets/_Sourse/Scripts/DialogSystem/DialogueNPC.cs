using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private float interactionDistance = 3f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        interactPrompt.SetActive(false);
    }

    private void Update()
    {
        if (DialogueManager.Instance.IsDialogueActive()) return;

        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance <= interactionDistance)
        {
            interactPrompt.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.Instance.StartDialogue(dialogue);
            }
        }
        else
        {
            interactPrompt.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}