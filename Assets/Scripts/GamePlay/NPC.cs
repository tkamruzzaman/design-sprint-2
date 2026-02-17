using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
   public NPCDialogue dialogueData;
   public GameObject dialoguePanel;
   public TMP_Text dialogueText, nameText;
   public Image portraitImage;

   private int dialogueIndex;
   private bool isTyping, isDialogueActive;

   public bool CanInteract()
   {
      return !isDialogueActive;
   }

   public void Interact()
   {

      if (dialogueData == null /*|| !isDialogueActive*/)
         return;
      print("Interacted >>>>>>>>>> ))");
      if (isDialogueActive)
      {
         NextLine();
      }
      else
      {
         StartDialogue();
      }
   }

   void StartDialogue()
   {
      isDialogueActive = true;
      dialogueIndex = 0;

      nameText.SetText(dialogueData.npcName);
      portraitImage.sprite = dialogueData.npcSprite;

      dialoguePanel.SetActive(true);

      StartCoroutine(TypeLine());
   }

   void NextLine()
   {
      if (isTyping)
      {
         //Skip typing animation and show the full line
         StopAllCoroutines();
         dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
         isTyping = false;
      }
      else if (++dialogueIndex < dialogueData.dialogueLines.Length)
      {
         //If another line, type next line 
         StartCoroutine(TypeLine());
      }
      else
      {
         EndDialogue();
      }
   }

   IEnumerator TypeLine()
   {
      isTyping = true;
      dialogueText.SetText("");

      foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
      {
         dialogueText.text += letter;
         //playSoundEffect
         yield return new WaitForSeconds(dialogueData.typingSpeed);
      }

      isTyping = false;
      if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
      {
         yield return new WaitForSeconds(dialogueData.autoProgressDelay);
         NextLine();

      }
   }

   public void EndDialogue()
   {
      StopAllCoroutines();
      isDialogueActive = false;
      dialogueText.SetText("");
      dialoguePanel.SetActive(false);
   }
}
