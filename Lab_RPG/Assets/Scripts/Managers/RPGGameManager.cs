using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Classe usada para gerenciar o jogo RPG
///</summary>
public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager instanciaCompartilhada = null; // refer�ncia a si mesma: singleton
    public RPGCameraManager cameraManager;       // refer�ncia � classe respons�vel pelo controle da c�mera no jogo
    public PontoSpawn playerPontoSpawn;          // refer�ncia ao ponto em que o player deve aparecer em uma cena
    public GameObject[] collectibleItemsOnScene; // array com refer�ncia a todos os GameObjects de itens colet�veis de determinada cena

    private void Awake() {
        // verifica se h� uma �nica inst�nica dessa classe sendo usada
        if (instanciaCompartilhada != null && instanciaCompartilhada != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanciaCompartilhada = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetupScene();
    }

    /*
     * inicia uma cena carregando o Player no seu ponto de spawn e armazenando todos itens cole�veis de uma cena em um array
     */
    public void SetupScene() 
    {
        SpawnPLayer();
        collectibleItemsOnScene = GameObject.FindGameObjectsWithTag("Collectible"); //carrega os GameObjects dos itens colet�veis da cena
    }

    /*
     * m�todo respons�vel por carregar o Player em um ponto espec�fico (PontoSpawnPlayerO) ao iniciar a cena 
    */
    public void SpawnPLayer()
    {
        if (playerPontoSpawn != null)
        {
            GameObject player = playerPontoSpawn.SpawnO();
            cameraManager.virtualCamera.Follow = player.transform;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //TESTE: verifica se o player ainda est� vivo. Caso n�o esteja, spawna ele novamente e recarrega os colet�veis da cena
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            foreach(GameObject colletibleItem in collectibleItemsOnScene){
                colletibleItem.SetActive(true);
            }
            GameObject player = playerPontoSpawn.SpawnO();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
}
