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
    GameObject cleanUp;

    bool stopSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(4);

        while (!stopSpawn)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-9, 9), 13, 0), Quaternion.identity);
            newEnemy.transform.parent = cleanUp.transform;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    public Transform CleanUpContainer()
    {
        return cleanUp.transform;
    }

    public void StopSpawning()
    {
        stopSpawn = true;
    }
}
