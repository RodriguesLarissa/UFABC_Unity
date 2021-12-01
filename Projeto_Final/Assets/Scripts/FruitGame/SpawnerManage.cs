using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManage : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fruits;
    private BoxCollider2D collider;

    float x1, x2;

    
    void Awake(){
        collider = GetComponent<BoxCollider2D>();
        
        x1 = transform.position.x - collider.bounds.size.x / 2f;
        x2 = transform.position.x + collider.bounds.size.x / 2f;

    }
    
    void Start()
    {
        StartCoroutine(SpwanFruit(1f));
    }

    IEnumerator SpwanFruit(float time){
        yield return new WaitForSecondsRealtime(time);

        Vector3 temp = transform.position;
        temp.x = Random.Range(x1, x2);

        Instantiate(fruits[Random.Range(0, fruits.Length)], temp, Quaternion.identity);

        StartCoroutine(SpwanFruit(Random.Range(0.5f,1f)));
    }
}
