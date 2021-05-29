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

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(4);

        while (player.IsAlive())
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
}
