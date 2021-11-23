using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Classe usada para gerenciar o jogo RPG
///</summary>
public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager instanciaCompartilhada = null; // referência a si mesma: singleton
    public RPGCameraManager cameraManager;       // referência à classe responsável pelo controle da câmera no jogo
    public PontoSpawn playerPontoSpawn;          // referência ao ponto em que o player deve aparecer em uma cena
    public GameObject[] collectibleItemsOnScene; // array com referência a todos os GameObjects de itens coletáveis de determinada cena

    private void Awake() {
        // verifica se há uma única instânica dessa classe sendo usada
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
     * inicia uma cena carregando o Player no seu ponto de spawn e armazenando todos itens coleáveis de uma cena em um array
     */
    public void SetupScene() 
    {
        SpawnPLayer();
        collectibleItemsOnScene = GameObject.FindGameObjectsWithTag("Collectible"); //carrega os GameObjects dos itens coletáveis da cena
    }

    /*
     * método responsável por carregar o Player em um ponto específico (PontoSpawnPlayerO) ao iniciar a cena 
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
        //TESTE: verifica se o player ainda está vivo. Caso não esteja, spawna ele novamente e recarrega os coletáveis da cena
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
