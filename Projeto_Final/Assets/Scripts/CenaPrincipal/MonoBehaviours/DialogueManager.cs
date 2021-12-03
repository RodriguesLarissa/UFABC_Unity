using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
/// Classe responsavel pelo controle o aparecimento da caixa de dialogo e como ele será mostrado na cena
///</summary>
public class DialogueManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj; // GameObject da caixa de dialogo
    public Image profile; // Local onde ficara a imagem do NPC
    public Text speechText; // Texto a ser mostrado na cena
    public Text actorNameText; // Texto para mostrar o nome de quem está falando

    [Header("Settings")]
    public float typingSpeed; // Velocidade em que o texto é mostrado
    private string[] sentences; // Frases que serão ditas pelo NPC
    private int index = 0; // Posição atual dentro do vetor sentences

    /* 
     * Função que mostra define as propriedades da caixa de dialogo e inicia o escrita na cena
     */
    public void Speech(Sprite p, string[] txt, string actorName)
    {
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
    }

    /* 
     * Função responsável por mostrar o Texto na tela. Ela mostra o texto letra por letra 
     * de acordo com uma velocidade
     */
    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    /* 
     * Função que muda para a frase seguinte ou fecha o dialogo se for a ultima frase
     */
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
