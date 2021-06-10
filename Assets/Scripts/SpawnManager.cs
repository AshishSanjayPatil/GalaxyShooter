using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    float spawnRate = 5;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    GameObject[] powerUpsPrefabs;

    [SerializeField]
    GameObject asteroidPrefab;

    [SerializeField]
    GameObject cleanUp;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUps());
        StartCoroutine(SpawnAsteroid());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(4);

        while (!gameManager.GameStatus())
        {
            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-9f, 9f), 8.5f, 0), Quaternion.identity);
            newEnemy.transform.parent = cleanUp.transform;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator SpawnPowerUps()
    {
        yield return new WaitForSeconds(4);

        while (!gameManager.GameStatus())
        {
            yield return new WaitForSeconds(Random.Range(7, 11));
            GameObject newPowerUp = Instantiate(powerUpsPrefabs[Random.Range(0, powerUpsPrefabs.Length)], new Vector3(Random.Range(-9f, 9f), 8.5f, 0), Quaternion.identity);
            newPowerUp.transform.parent = cleanUp.transform;
        }
    }

    IEnumerator SpawnAsteroid()
    {
        while(!gameManager.GameStatus())
        {
            yield return new WaitForSeconds(30);
            GameObject newAsteroid = Instantiate(asteroidPrefab, new Vector3(Random.Range(-10f, 10f), 12f, 0), Quaternion.identity);
            newAsteroid.transform.parent = cleanUp.transform;
        }
    }

    public Transform CleanUpContainer()
    {
        return cleanUp.transform;
    }
}
