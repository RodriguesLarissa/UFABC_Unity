using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///<summary>
/// Classe responsavel pelo controle o aparecimento da caixa de dialogo e como ele será mostrado na cena
///</summary>
public class DialogueManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj; // GameObject da caixa de dialogo
    public GameObject decisionButtons; // GameObject da caixa de dialogo
    public Image profile; // Local onde ficara a imagem do NPC
    public Text speechText; // Texto a ser mostrado na cena
    public Text actorNameText; // Texto para mostrar o nome de quem está falando

    [Header("Settings")]
    public float typingSpeed; // Velocidade em que o texto é mostrado
    private string[] sentences; // Frases que serão ditas pelo NPC
    private int index = 0; // Posição atual dentro do vetor sentences
    [HideInInspector]
    public bool inDialogue = false; // // Indica se o dialogo já foi iniciado
    public string sceneName;

    /* 
     * Função que mostra define as propriedades da caixa de dialogo e inicia o escrita na cena
     */
    public void Speech(Sprite p, string[] txt, string actorName, string scene)
    {
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        inDialogue = true;
        sceneName = scene;
        StartCoroutine(TypeSentence(sentences[index]));
    }

    /* 
     * Função responsável por mostrar o Texto na tela. Ela mostra o texto letra por letra 
     * de acordo com uma velocidade
     */
    IEnumerator TypeSentence(string sentence)
    {
        foreach (char letter in sentence.ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    /*
     * Muda a frase precissioando espaço
     */
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextSentence();
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
                StartCoroutine(TypeSentence(sentences[index]));
            } else // acabou os textos
            {
                speechText.text = "";
                Decision();
            }
        }
    }

    public void Decision ()
    {
        StartCoroutine(TypeSentence("Você aceita o desafio?"));
        decisionButtons.SetActive(true);
    }

    public void closeDialogue()
    {
        inDialogue = false;
        speechText.text = "";
        index = 0;
        decisionButtons.SetActive(false);
        dialogueObj.SetActive(false);
    }

    public void goToScene()
    {
        PlayerPrefs.SetString("Scene", sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
