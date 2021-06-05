using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    float spawnRate = 5;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    GameObject[] powerUpsPrefabs;

    [SerializeField]
    GameObject cleanUp;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUps());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(4);

        while (!gameManager.GameStatus())
        {
            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-9, 9), 13, 0), Quaternion.identity);
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
            GameObject newPowerUp = Instantiate(powerUpsPrefabs[Random.Range(0, powerUpsPrefabs.Length)], new Vector3(Random.Range(-9, 9), 13, 0), Quaternion.identity);
            newPowerUp.transform.parent = cleanUp.transform;
        }
    }

    public Transform CleanUpContainer()
    {
        return cleanUp.transform;
    }
}
