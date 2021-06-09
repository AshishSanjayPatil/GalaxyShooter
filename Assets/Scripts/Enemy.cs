using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    GameObject enemyExplosion;

    [SerializeField]
    AudioClip explosionSFX;

    float speed = 8f;

    SpawnManager spawnManager;

    UIManager uiManager;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
        StartCoroutine(ShootLaser());
    }

    // Update is called once per frame
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
            transform.position = new Vector3(Random.Range(-9, 9), 13);
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
