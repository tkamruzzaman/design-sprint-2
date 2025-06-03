using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] string[] dialogueLines;
    [SerializeField] float typingSpeed = 0.05f;
    
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool dialogueComplete = false;

    public bool IsDialogueComplete() => dialogueComplete;

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    public void StartDialogue()
    {
        if (dialogueLines.Length == 0) 
        {
            dialogueComplete = true;
            return;
        }

        dialogueComplete = false;
        currentLineIndex = 0;
        
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);
            
        StartCoroutine(DisplayLine());
    }

    private IEnumerator DisplayLine()
    {
        isTyping = true;
        dialogueText.text = "";
        
        foreach (char letter in dialogueLines[currentLineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        isTyping = false;
        
        // Wait for player input or auto-advance
        yield return new WaitForSeconds(2f); // Auto-advance after 2 seconds
        
        NextLine();
    }

    public void NextLine()
    {
        if (isTyping) return;
        
        currentLineIndex++;
        
        if (currentLineIndex < dialogueLines.Length)
        {
            StartCoroutine(DisplayLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueComplete = true;
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // Allow clicking to skip typing or advance dialogue
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            NextLine();
        }
        else if (Input.GetMouseButtonDown(0) && isTyping)
        {
            // Skip typing animation
            StopAllCoroutines();
            dialogueText.text = dialogueLines[currentLineIndex];
            isTyping = false;
        }
    }
}