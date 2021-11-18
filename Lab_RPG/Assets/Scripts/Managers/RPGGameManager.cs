using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// Classe usada para gerenciar o jogo RPG
///</summary>
public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager instanciaCompartilhada = null;
    public RPGCameraManager cameraManager;
    public PontoSpawn playerPontoSpawn;

    private void Awake() {
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

    public void SetupScene() 
    {
        SpawnPLayer();
    }

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
        
    }
}
