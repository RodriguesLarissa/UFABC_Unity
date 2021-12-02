using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
/// Classe responsavel pelo controle dos dialogos entre o NPC e o Player
///</summary>
public class DialogueManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile; //
    public Text speechText; //
    public Text actorNameText; // Texto para mostrar o nome de quem está falando

    [Header("Settings")]
    public float typingSpeed; // Velocidade em que o texto é mostrado
    private string[] sentences;
    private int index = 0;

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            // ainda há textos
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            } else // acabou os textos
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
            }
        }
    }
}
