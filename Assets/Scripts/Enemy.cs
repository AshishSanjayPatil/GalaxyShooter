using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject laserPrefab;

    float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootLaser());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= -6)
            transform.position = new Vector3(Random.Range(-9, 9), 13);
    }

    IEnumerator ShootLaser()
    {
        while(this.gameObject)
        {
            GameObject newEnemyLaser = Instantiate(laserPrefab, transform.position + new Vector3(0, -0.85f, 0), Quaternion.identity);
            //newEnemyLaser.transform.parent = 

            yield return new WaitForSeconds(3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            other.GetComponent<Player>().ReduceLives();
            Destroy(this.gameObject);
        }
    }
}
