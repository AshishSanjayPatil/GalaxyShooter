using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    int lives = 3;

    bool tripleShotActive = false;

    bool speedBoostActive = false;

    bool shieldActive = false;

    float speed = 6.5f;

    float fireRate = 0.25f;

    float nextFire = 0;

    string inputAxisHorizontal;

    string inputAxisVertical;

    [SerializeField]
    int shieldLives = 3;

    [SerializeField]
    bool player1 = false;

    [SerializeField]
    GameObject deathVFX;

    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    GameObject tripleShotPrefab;

    [SerializeField]
    GameObject[] engineDamage; 

    [SerializeField]
    GameObject shieldVFX;

    [SerializeField]
    GameObject speedBoostVFX;

    [SerializeField]
    AudioClip explosionSFX;

    Coroutine TripleShotRoutine = null;

    Coroutine SpeedBoostRoutine = null;

    Coroutine ShieldOnRoutine = null;

    SpawnManager spawnManager;

    UIManager uiManager;

    GameManager gameManager;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        uiManager = FindObjectOfType<UIManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
        gameManager = FindObjectOfType<GameManager>();
        uiManager.UpdateLives(lives);

        if (player1)
        {
            inputAxisHorizontal = "Horizontal";
            inputAxisVertical = "Vertical";
        }
        else
        {
            inputAxisHorizontal = "Horizontal2";
            inputAxisVertical = "Vertical2";
        }
    }

    void Update()
    {
        if (gameManager.GameStatus())
        {
            GameObject newDeathVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
            newDeathVFX.transform.parent = spawnManager.CleanUpContainer();
            Destroy(newDeathVFX, 3f);
            Destroy(this.gameObject);
        }

        if (transform.position.y <= -3.5f)
            transform.Translate(1.5f * Time.deltaTime * Vector3.up);
        else
        {
            Move();

#if UNITY_ANDROID
            if (CrossPlatformInputManager.GetButtonDown("Fire") && Time.time > nextFire)
                Shoot();
#else
            if (Input.GetKeyDown(KeyCode.Space) && player1 && Time.time > nextFire)
                Shoot();
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) && !player1 && Time.time > nextFire)
                Shoot();
#endif
        }
    }

    private void Move()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis(inputAxisHorizontal);
        float verticalInput = CrossPlatformInputManager.GetAxis(inputAxisVertical);

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

        audioSource.Play();
    }

    public void ReduceLives()
    {
        if (!shieldActive)
        {
            lives--; 
            
            if (!player1)
                uiManager.UpdateLives(lives, 2);
            else
                uiManager.UpdateLives(lives);

            if (lives == 1)
            {
                engineDamage[0].SetActive(true);
                engineDamage[1].SetActive(true);
            }
            else if (lives == 2)
            {
                engineDamage[Random.Range(0, engineDamage.Length)].SetActive(true);
            }

            if (lives <= 0)
            {
                lives = 0;
                AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position, 0.5f);
                GameObject newDeathVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
                newDeathVFX.transform.parent = spawnManager.CleanUpContainer();
                Destroy(newDeathVFX, 3f);
                Destroy(this.gameObject);
            }
        }
        else
        {
            shieldLives--;
            
            if (shieldLives <= 0)
            {
                shieldActive = false;
                shieldVFX.SetActive(false);

                if (ShieldOnRoutine != null)
                {
                    StopCoroutine(ShieldOnRoutine);
                    ShieldOnRoutine = null;
                }
            }
        }
    }

    public void AddLife()
    {
        if (lives < 3)
        {
            lives++;

            if (!player1)
                uiManager.UpdateLives(lives, 2);
            else
                uiManager.UpdateLives(lives);

            if (engineDamage[0].activeSelf && engineDamage[1].activeSelf)
                engineDamage[Random.Range(0, engineDamage.Length)].SetActive(false);
            else if (engineDamage[0].activeSelf)
                engineDamage[0].SetActive(false);
            else if (engineDamage[1].activeSelf)
                engineDamage[1].SetActive(false);
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
        speedBoostVFX.SetActive(true);

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
        shieldLives = 3;
        shieldVFX.SetActive(true);

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
        speedBoostVFX.SetActive(false);
    }

    IEnumerator ShieldPowerUpCD()
    {
        yield return new WaitForSeconds(10);
        shieldActive = false;
        shieldVFX.SetActive(false);
    }
}
