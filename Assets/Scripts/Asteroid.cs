using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float speed = 5f;

    float rotateSpeed = 50f;

    float xDirection = 1;

    float yDirection = 1;

    [SerializeField]
    GameObject explosionVFX;

    [SerializeField]
    GameObject repairKitPrefab;

    [SerializeField]
    AudioClip explosionSFX;

    SpawnManager spawnManager;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.GameStatus())
        {
            AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, 0.5f);
            GameObject newAsteroidExplosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            newAsteroidExplosion.transform.parent = spawnManager.CleanUpContainer();
            Destroy(this.gameObject);
        }

        transform.position += speed * Time.deltaTime * new Vector3(xDirection, yDirection, 0);

        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);

        if (transform.position.y >= 8f || transform.position.y <= -8f)
        {
            transform.position = new Vector3(transform.position.x, 8f, 0);
            yDirection = -1f;
        }

        if (transform.position.x >= 12f)
        {
            transform.position = new Vector3(12f, transform.position.y, 0);
            xDirection = -1f;
        }

        if (transform.position.x <= -12f)
        {
            transform.position = new Vector3(-12f, transform.position.y, 0);
            xDirection = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().ReduceLives();
            AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, 0.5f);
            GameObject newAsteroidExplosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            newAsteroidExplosion.transform.parent = spawnManager.CleanUpContainer();
            Destroy(this.gameObject);
        }

        if (collision.GetComponent<PlayerLaser>())
        {
            AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, 0.5f);
            GameObject newAsteroidExplosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            newAsteroidExplosion.transform.parent = spawnManager.CleanUpContainer();
            GameObject newRepairKit = Instantiate(repairKitPrefab, transform.position, Quaternion.identity);
            newRepairKit.transform.parent = spawnManager.CleanUpContainer();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
