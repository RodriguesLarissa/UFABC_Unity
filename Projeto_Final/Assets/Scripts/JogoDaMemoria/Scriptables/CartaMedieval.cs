using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que armazena informações de uma carta do Jogo da Memoria Medieval
/// </summary>
[CreateAssetMenu(menuName = "Carta Medieval")]
public class CartaMedieval : ScriptableObject
{
    public Sprite frente;   //sprite para a frente da carta
    public Sprite verso;    //sprite para o verso da carta
    public string tag;      //identificador da carta

}
