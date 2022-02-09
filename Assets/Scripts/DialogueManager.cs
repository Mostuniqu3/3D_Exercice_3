using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueTrigger trigger;
    [SerializeField] private DialogueSkinChanger skinChanger;

    [SerializeField] private Image CharacterImage;
    [SerializeField] private Image BackgroundImage;

    [Header("TextMeshPro")]
    [SerializeField] private TextMeshProUGUI NameText;
    [SerializeField] private TextMeshProUGUI DialogueText;

    [Header("Délais d'affichage du text")]
    [SerializeField] private float LetterPerSecond = 2f;
    [SerializeField] private float DelayAfterTextEnds = 2f;

    private bool showLetter = true;
    private List<Dialogue> dialogueList = new List<Dialogue>();
    private List<List<char>> textList = new List<List<char>>();

    private Dialogue currentDialogue;
    private List<char> currentText;

    private char toShow;
    private bool wait = false;
    private float timer;
    private Color defaultColor = new Color(0,0,0,0);
    IEnumerator TextUpdateTimer()
    {
        while (true){
            yield return new WaitForSeconds(1 / LetterPerSecond);
            showLetter = true;
        }
    }

    // Remplissage des Lists nécessaires pour afficher les dialogues
    void populateLists()
    {
        foreach (Dialogue dialogue in trigger.GetDialogues())
        {
            foreach (string text in dialogue.text)
            {
                dialogueList.Add(dialogue);
                textList.Add(new List<char>(text));
            }
        }
        currentDialogue = dialogueList[0];
        currentText = textList[0];
        dialogueList.RemoveAt(0);
        textList.RemoveAt(0);
    }

    void Start()
    {
        StartCoroutine(TextUpdateTimer());
        populateLists();
        DialogueText.text = "";
        updateDialogue();
    }

    void updateDialogue()
    {
        currentText = textList[0];
        currentDialogue = dialogueList[0];
        textList.RemoveAt(0);
        dialogueList.RemoveAt(0);

        NameText.text = currentDialogue.characterName;
        CharacterImage.sprite = currentDialogue.characterSprite;
        if (currentDialogue.hasToChangeBackground) BackgroundImage.sprite = currentDialogue.background;
        if (currentDialogue.dialogueSkin.BackgroundColor != defaultColor) skinChanger.changeDialogueColor(currentDialogue.dialogueSkin.BackgroundColor);
        if (currentDialogue.dialogueSkin.BorderColor != defaultColor)     skinChanger.changeDialogueBorderColor(currentDialogue.dialogueSkin.BorderColor);
        if (currentDialogue.nameSkin.BackgroundColor != defaultColor)     skinChanger.changeNameColor(currentDialogue.nameSkin.BackgroundColor);
        if (currentDialogue.nameSkin.BorderColor != defaultColor)         skinChanger.changeNameBorderColor(currentDialogue.nameSkin.BorderColor);

    }

    void updateText()
    {
        toShow = currentText[0];
        // Eviter d'attendre pour les espaces
        if(toShow == ' ' && currentText.Count > 1)
        {
            DialogueText.text += toShow;
            currentText.RemoveAt(0);
            toShow = currentText[0];
        }
        DialogueText.text += toShow;
        currentText.RemoveAt(0);
        showLetter = false;
    }

    // Appeler à la fin du dialogue entier
    void EndDialogue()
    {
        NameText.text = "Fin";
        DialogueText.text = "Dialogue Terminé ! ";
    }

    // Attente après la fin d'une suite de phrases
    void WaitAfterTextEnds()
    {
        wait = true;
        timer = DelayAfterTextEnds;
    }

    bool HasToWait()
    {
        if (!wait) return false;
        timer -= Time.deltaTime;
        if (timer > 0) return true;
        else
        {
            wait = false;
            return false;
        }
    }

    private void Update()
    {
        if (HasToWait()) return;

        if (!showLetter) return;
        if(currentText.Count == 0)
        {
            if (textList.Count == 0) { 
                EndDialogue();
                return;
            }
            updateDialogue();
            DialogueText.text = "";
        }
        updateText();
        if(currentText.Count == 0) WaitAfterTextEnds();
    }
}
