using UnityEngine;

public class Municao : MonoBehaviour
{
    public int danoCausado; ///poder de dano da municao

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision is BoxCollider2D){
            Inimigo inimigo = collision.gameObject.GetComponent<Inimigo>();
            StartCoroutine(inimigo.DanoCaractere(danoCausado, 0.0f));
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
