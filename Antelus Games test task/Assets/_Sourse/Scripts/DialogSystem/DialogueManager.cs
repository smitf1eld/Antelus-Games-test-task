using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public event Action OnDialogueStart;
    public event Action OnDialogueEnd;

    [Header("UI Elements")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject interactPrompt;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private Camera dialogueCamera;

    private Dialogue currentDialogue;
    private int currentLineIndex;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private Camera mainCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);

        dialoguePanel.SetActive(false);
        if (interactPrompt) interactPrompt.SetActive(false);
    }

    private void Start()
    {
        mainCamera = Camera.main;
        if (dialogueCamera) dialogueCamera.gameObject.SetActive(false);
    }

    public void ShowInteractPrompt(bool show)
    {
        if (interactPrompt) interactPrompt.SetActive(show);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null || dialogue.lines.Length == 0) return;

        currentDialogue = dialogue;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        
        OnDialogueStart?.Invoke();
        
        if (dialogueCamera != null)
        {
            mainCamera.enabled = false;
            dialogueCamera.gameObject.SetActive(true);
        }
        
        ShowCurrentLine();
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        OnDialogueEnd?.Invoke();
        
        if (dialogueCamera != null)
        {
            dialogueCamera.gameObject.SetActive(false);
            mainCamera.enabled = true;
        }
        
        currentDialogue = null;
    }

    public void ShowNextLine()
    {
        if (isTyping)
        {
            CompleteLine();
            return;
        }

        currentLineIndex++;
        
        if (currentLineIndex < currentDialogue.lines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowCurrentLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        
        typingCoroutine = StartCoroutine(TypeText(currentDialogue.lines[currentLineIndex].text));
        
        if (currentDialogue.lines[currentLineIndex].camera != null)
        {
            dialogueCamera.transform.position = currentDialogue.lines[currentLineIndex].camera.transform.position;
            dialogueCamera.transform.rotation = currentDialogue.lines[currentLineIndex].camera.transform.rotation;
        }
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        isTyping = false;
    }

    private void CompleteLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        
        dialogueText.text = currentDialogue.lines[currentLineIndex].text;
        isTyping = false;
    }

    public bool IsDialogueActive()
    {
        return currentDialogue != null;
    }
}