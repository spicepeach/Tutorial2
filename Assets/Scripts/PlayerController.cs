using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;

    private int count;
    private int lives;

    public TextMeshProUGUI countText;
    public TextMeshProUGUI livesText;

    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject playAgainButton;

    public float speed;

    public GameObject backgroundMusic;
    public GameObject winMusic;

    Animator anim;
    SpriteRenderer sprite;
    bool grounded;


    //private int scoreValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        //score.text = scoreValue.ToString();

        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        count = 0;
        lives = 3;

        SetCountText();
        SetLivesText();

        winMusic.SetActive(false);
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        playAgainButton.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            sprite.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            sprite.flipX = true;
        }

        if (grounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("State", 2);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                anim.SetInteger("State", 1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                anim.SetInteger("State", 1);
            }
            else
            {
                anim.SetInteger("State", 0);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        rd2d.velocity = new Vector2(hozMovement * speed, rd2d.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = true;
        }

        if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
            collision.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            count = count + 1;
            SetCountText();
            collision.gameObject.SetActive(false);
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1") && count >= 4)
        {
            SceneManager.LoadScene("Level2");
            lives = 3;
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2") && count >= 4)
        {
            backgroundMusic.SetActive(false);
            winMusic.SetActive(true);

            winTextObject.SetActive(true);
            playAgainButton.SetActive(true);
            //this.gameObject.SetActive(false);
        }
    }



    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives <= 0)
        {
            loseTextObject.SetActive(true);
            playAgainButton.SetActive(true);
            this.gameObject.SetActive(false);
        } 
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }
        }
    }
}
