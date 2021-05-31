using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    int lives = 3;

    [SerializeField]
    int shieldLives = 3;

    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    GameObject tripleShotPrefab;

    float speed = 6.5f;

    float fireRate = 0.25f;

    float nextFire = 0;

    [SerializeField]
    bool tripleShotActive = false;

    [SerializeField]
    bool speedBoostActive = false;

    [SerializeField]
    bool shieldActive = false;

    Coroutine TripleShotRoutine = null;

    Coroutine SpeedBoostRoutine = null;

    Coroutine ShieldOnRoutine = null;

    SpawnManager spawnManager;

    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        if (transform.position.y <= -3.5f)
            transform.Translate(1.5f * Time.deltaTime * Vector3.up);
        else
        {
            Move();
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time > nextFire)
                Shoot();
        }
    }

    private void Move()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

        if (!speedBoostActive)
            transform.Translate(speed * Time.deltaTime * new Vector3(horizontalInput, verticalInput, 0));
        else
            transform.Translate(speed * 1.5f * Time.deltaTime * new Vector3(horizontalInput, verticalInput, 0));

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

        if(!tripleShotActive)
        {
            GameObject newPlayerLaser = Instantiate(laserPrefab, transform.position + new Vector3(0, 1.08f, 0), Quaternion.identity);
            newPlayerLaser.transform.parent = spawnManager.CleanUpContainer();
        }
        else
        {
            GameObject newTripleShot = Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            newTripleShot.transform.parent = spawnManager.CleanUpContainer();
        }
    }

    public void ReduceLives()
    {
        if (!shieldActive)
        {
            lives--;

            if (lives <= 0)
            {
                spawnManager.StopSpawning();
                Destroy(this.gameObject);
            }
        }
        else
        {
            shieldLives--;
            
            if (shieldLives <= 0)
            {
                shieldActive = false;

                if (ShieldOnRoutine != null)
                {
                    StopCoroutine(ShieldOnRoutine);
                    ShieldOnRoutine = null;
                }
            }
        }
    }

    public void TripleShotOn()
    {
        tripleShotActive = true;

        if (TripleShotRoutine != null)
        {
            StopCoroutine(TripleShotRoutine);
            TripleShotRoutine = null;
        }

        TripleShotRoutine = StartCoroutine(TripleShotPowerUpCD());
    }

    public void SpeedBoostOn()
    {
        speedBoostActive = true;

        if (SpeedBoostRoutine != null)
        {
            StopCoroutine(SpeedBoostRoutine);
            SpeedBoostRoutine = null;
        }

        SpeedBoostRoutine = StartCoroutine(SpeedBoostPowerUpCD());
    }

    public void ShieldOn()
    {
        shieldActive = true;

        if (ShieldOnRoutine != null)
        {
            StopCoroutine(ShieldOnRoutine);
            ShieldOnRoutine = null;
        }

        ShieldOnRoutine = StartCoroutine(ShieldPowerUpCD());
    }

    IEnumerator TripleShotPowerUpCD()
    {
        yield return new WaitForSeconds(5);
        tripleShotActive = false;
    }

    IEnumerator SpeedBoostPowerUpCD()
    {
        yield return new WaitForSeconds(5);
        speedBoostActive = false;
    }

    IEnumerator ShieldPowerUpCD()
    {
        yield return new WaitForSeconds(10);
        shieldActive = false;
    }
}
