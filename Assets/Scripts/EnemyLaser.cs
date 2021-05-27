using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    float speed = 10f;

    Player player;

    Rigidbody rigidBody;

    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>();
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        rigidBody.velocity = new Vector3(moveDirection.x + 0.5f, moveDirection.y, 0);
        Destroy(this.gameObject, 5f);
    }
}
