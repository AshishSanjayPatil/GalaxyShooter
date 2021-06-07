using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    int powerUpCode;

    float speed = 3f;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
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

            Destroy(this.gameObject);
        }
    }
}
