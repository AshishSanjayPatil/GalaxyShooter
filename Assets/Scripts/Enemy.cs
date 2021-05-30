using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject laserPrefab;

    float speed = 8f;

    SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        StartCoroutine(ShootLaser());
    }

    // Update is called once per frame
    void Update()
    {
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
            Destroy(this.gameObject);
        }
    }
}
