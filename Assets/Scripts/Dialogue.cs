using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string characterName;
    public Sprite characterSprite;
    [Tooltip("If empty, doesn't change background")]
    public bool hasToChangeBackground = false;
    public Sprite background;
    [Header("Apparence des boîtes de dialogue")]
    public DialogueSkin nameSkin;
    public DialogueSkin dialogueSkin;
    [TextArea(3, 10)]
    public string[] text;
}
