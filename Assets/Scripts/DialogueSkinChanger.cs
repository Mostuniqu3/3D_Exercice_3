using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSkinChanger : MonoBehaviour
{
    [Header("Name Skin")]
    [SerializeField] private Image nameObject;
    [SerializeField] private Image nameBorderImage;

    [Header("Main Dialogue Skin")]
    [SerializeField] private Image dialogueObject;
    [SerializeField] private Image dialogueBorderImage;

    public void changeNameColor(Color newColor)
    {
        nameObject.color = newColor;
    }

    public void changeNameBorderColor(Color newColor)
    {
        nameBorderImage.color = newColor;
    }

    public void changeDialogueColor(Color newColor)
    {
        dialogueObject.color = newColor;
    }
    public void changeDialogueBorderColor(Color newColor)
    {
        dialogueBorderImage.color = newColor;
    }
}