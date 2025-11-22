using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    public GameObject explosionPrefab;
    private GameManager gameManager;

    private float moveSpeed;
    private float waveSpeed;
    private float waveSize;
    private float timer;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        moveSpeed = 2.5f;
        waveSpeed = 3f;
        waveSize = 2f;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float xMove = Mathf.Sin(timer * waveSpeed) * waveSize * Time.deltaTime;
        float yMove = -moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(xMove, yMove, 0));

        if (transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "Player")
        {
            whatDidIHit.GetComponent<PlayerController>().LoseALife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if(whatDidIHit.tag == "Weapons")
        {
            Destroy(whatDidIHit.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.AddScore(5);
            Destroy(this.gameObject);
        }
    }
}
