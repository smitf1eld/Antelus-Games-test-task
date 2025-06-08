using UnityEngine;
using System.Collections;

public class DialogSystem : MonoBehaviour
{
    private DialogManager dialogManager;
    private PlayerController playerController;
    private Camera mainCamera;
    private bool isDialogActive = false;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool isTextTyping = false;

    private void Awake()
    {
        dialogManager = GetComponent<DialogManager>();
        if (dialogManager == null)
        {
            Debug.LogError("DialogManager component not found!");
            return;
        }

        InitializeDialogSystem();
    }

    private void InitializeDialogSystem()
    {
        if (dialogManager.dialogPanel != null) 
            dialogManager.dialogPanel.SetActive(false);
        if (dialogManager.pressEPrompt != null) 
            dialogManager.pressEPrompt.SetActive(false);
        if (dialogManager.spaceContinuePrompt != null) 
            dialogManager.spaceContinuePrompt.SetActive(false);
        
        playerController = FindObjectOfType<PlayerController>();
        mainCamera = Camera.main;
    }

    public void StartDialog()
    {
        if (isDialogActive || dialogManager.dialogLines.Length == 0) return;

        isDialogActive = true;
        currentLineIndex = 0;
        dialogManager.dialogPanel.SetActive(true);
        
        if (playerController != null)
            playerController.SetMovementEnabled(false);
        
        Cursor.lockState = CursorLockMode.None;
        DisplayCurrentLine();
    }

    private void DisplayCurrentLine()
    {
        if (currentLineIndex >= dialogManager.dialogLines.Length)
        {
            EndDialog();
            return;
        }

        DialogManager.DialogLine currentLine = dialogManager.dialogLines[currentLineIndex];
        dialogManager.speakerText.text = currentLine.speakerName;
        SwitchCamera(currentLine.dialogCamera);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeDialogText(currentLine.text));
    }

    private IEnumerator TypeDialogText(string text)
    {
        isTextTyping = true;
        dialogManager.spaceContinuePrompt.SetActive(false);
        dialogManager.dialogText.text = "";
        
        foreach (char letter in text)
        {
            dialogManager.dialogText.text += letter;
            yield return new WaitForSeconds(dialogManager.textSpeed);
        }
        
        isTextTyping = false;
        dialogManager.spaceContinuePrompt.SetActive(true);
        typingCoroutine = null;
    }

    private void SwitchCamera(Camera dialogCamera)
    {
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
        }

        if (dialogCamera != null)
        {
            dialogCamera.gameObject.SetActive(true);
        }
    }

    private void EndDialog()
    {
        isDialogActive = false;
        dialogManager.dialogPanel.SetActive(false);
        dialogManager.spaceContinuePrompt.SetActive(false);
        
        if (playerController != null)
            playerController.SetMovementEnabled(true);
        
        Cursor.lockState = CursorLockMode.Locked;

        foreach (var line in dialogManager.dialogLines)
        {
            if (line.dialogCamera != null)
                line.dialogCamera.gameObject.SetActive(false);
        }

        if (mainCamera != null)
            mainCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!isDialogActive) return;

        HandleDialogInput();
    }

    private void HandleDialogInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTextTyping)
        {
            CompleteCurrentLine();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isTextTyping)
        {
            ProceedToNextLine();
        }
    }

    private void CompleteCurrentLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        dialogManager.dialogText.text = dialogManager.dialogLines[currentLineIndex].text;
        isTextTyping = false;
        dialogManager.spaceContinuePrompt.SetActive(true);
    }

    private void ProceedToNextLine()
    {
        dialogManager.spaceContinuePrompt.SetActive(false);
        currentLineIndex++;
        DisplayCurrentLine();
    }

    public bool IsDialogActive()
    {
        return isDialogActive;
    }
}