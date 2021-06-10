using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    float speed = 3f;

    [SerializeField]
    int powerUpCode;

    [SerializeField]
    AudioClip powerUpSFX;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.GameStatus())
            Destroy(this.gameObject);

        transform.Translate(speed * Time.deltaTime * Vector3.down);

        if (transform.position.y <= -8f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Player player = collision.GetComponent<Player>();

            switch (powerUpCode)
            {
                case 0:
                    player.TripleShotOn();
                    break;
                case 1:
                    player.SpeedBoostOn();
                    break;
                case 2:
                    player.ShieldOn();
                    break;
                case 3:
                    player.AddLife();
                    break;
                default:
                    Debug.Log("PowerCode Error!!!");
                    break;
            }

            AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position, 0.5f);
            Destroy(this.gameObject);
        }
    }
}
