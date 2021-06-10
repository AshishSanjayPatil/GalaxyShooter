using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    float speed = 8f;

    void Update()
    {
        if (transform.position.y >= 8f)
        {
            if (transform.parent.CompareTag("TripleShot"))
                Destroy(transform.parent.gameObject);

            Destroy(this.gameObject);
        }

        transform.Translate(speed * Time.deltaTime * Vector3.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyLaser>())
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
