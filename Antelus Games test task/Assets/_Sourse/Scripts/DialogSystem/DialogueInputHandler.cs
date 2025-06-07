using UnityEngine;

public class DialogueInputHandler : MonoBehaviour
{
    private void Update()
    {
        if (DialogueManager.Instance.IsDialogueActive() && Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.Instance.ShowNextLine();
        }
    }
}