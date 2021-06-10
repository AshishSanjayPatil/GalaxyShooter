using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 8f;

    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    GameObject enemyExplosion;

    [SerializeField]
    AudioClip explosionSFX;

    SpawnManager spawnManager;

    UIManager uiManager;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
        StartCoroutine(ShootLaser());
    }

    void Update()
    {
        if (gameManager.GameStatus())
        {
            GameObject newEnemyExplosion = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            newEnemyExplosion.transform.parent = spawnManager.CleanUpContainer();
            Destroy(newEnemyExplosion, 3f);
            Destroy(this.gameObject);
        }

        transform.Translate(speed * Time.deltaTime * Vector3.down);

        if (transform.position.y <= -6)
            transform.position = new Vector3(Random.Range(-9f, 9f), 8.5f);
    }

    IEnumerator ShootLaser()
    {
        while(this.gameObject)
        {
            GameObject newEnemyLaser = Instantiate(laserPrefab, transform.position + new Vector3(0.1f, -1.35f, 0), Quaternion.identity);
            newEnemyLaser.transform.parent = spawnManager.CleanUpContainer();
            yield return new WaitForSeconds(3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().ReduceLives();
            uiManager.AddScore();
            AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, 0.5f);
            GameObject newEnemyExplosion = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            newEnemyExplosion.transform.parent = spawnManager.CleanUpContainer();
            Destroy(newEnemyExplosion, 3f);
            Destroy(this.gameObject);
        }

        if (collision.GetComponent<PlayerLaser>())
        {
            uiManager.AddScore();
            AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, 0.5f);
            GameObject newEnemyExplosion = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            newEnemyExplosion.transform.parent = spawnManager.CleanUpContainer();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
