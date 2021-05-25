using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject laserPrefab;

    float speed = 6.5f;

    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.y <= -3.5f)
            transform.Translate(Vector3.up * 1.5f * Time.deltaTime);
        else
        {
            Move();
            if(Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }
    }

    private void Move()
    {
        transform.Translate(new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0) * speed * Time.deltaTime);
    }

    private void Shoot()
    {
        Instantiate(laserPrefab, transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity);
    }
}
