using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    float speed = 10f;

    Vector3 moveDirection;

    Player[] player;

    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectsOfType<Player>();

        if (player.Length > 0)
        {
            moveDirection = (player[Random.Range(0, player.Length)].transform.position - transform.position).normalized * speed;
            rigidBody.velocity = new Vector3(moveDirection.x + 0.5f, moveDirection.y, 0);
        }
        else
            Destroy(this.gameObject);

        Destroy(this.gameObject, 5f);
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
