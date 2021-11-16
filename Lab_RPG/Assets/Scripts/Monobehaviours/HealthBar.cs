using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PontosDano pontosDano;
    public Player caractere;
    public Image medidorImagem;
    public Text pdTexto;
    float maxPontosDano;
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
