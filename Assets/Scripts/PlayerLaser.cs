using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 14f)
            Destroy(this.gameObject);

        transform.Translate(speed * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() || collision.GetComponent<EnemyLaser>())
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
