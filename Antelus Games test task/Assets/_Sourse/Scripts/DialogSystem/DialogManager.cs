using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogLine
    {
        public string speakerName;
        [TextArea(3, 10)] public string text;
        public Camera dialogCamera;
    }

    [Header("UI Elements")]
    public GameObject dialogPanel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogText;
    public GameObject pressEPrompt;
    public GameObject spaceContinuePrompt;

    [Header("Dialog Settings")]
    public DialogLine[] dialogLines;
    public float textSpeed = 0.05f;
}