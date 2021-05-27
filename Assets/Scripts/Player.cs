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

    float fireRate = 0.5f;

    float nextFire = 0;

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
            if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time > nextFire)
                Shoot();
        }
    }

    private void Move()
    {
        transform.Translate(new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0) * speed * Time.deltaTime);
        
        if (transform.position.y >= 0)
            transform.position = new Vector3(transform.position.x, 0, 0);
        else if (transform.position.y <= -3.5f)
            transform.position = new Vector3(transform.position.x, -3.5f, 0);

        if (transform.position.x >= 11.5f)
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        else if (transform.position.x <= -11.5f)
            transform.position = new Vector3(11.5f, transform.position.y, 0);
    }

    private void Shoot()
    {
        nextFire = Time.time + fireRate;
        GameObject newPlayerLaser = Instantiate(laserPrefab, transform.position + new Vector3(0, 0.85f, 0), Quaternion.identity);
        //newPlayerLaser.transform.parent =
    }
}
