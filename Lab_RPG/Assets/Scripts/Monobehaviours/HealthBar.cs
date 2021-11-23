using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe que representa a interface em que os pontos de vida do jogador principal são exibidos
/// </summary>

public class HealthBar : MonoBehaviour
{
    public PontosDano pontosDano;   //valor atual de pontos de vida do player
    public Player caractere;        //referência ao player
    public Image medidorImagem;     
    public Text pdTexto;
    float maxPontosDano;            //valor máximo de pontos de vida que o player pode acumular

    // Start is called before the first frame update
    void Start()
    {
        maxPontosDano = caractere.maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if(caractere != null)
        {
            medidorImagem.fillAmount = pontosDano.value / maxPontosDano;
            pdTexto.text = "PD:" + (medidorImagem.fillAmount * 100);
        }
    }
}
