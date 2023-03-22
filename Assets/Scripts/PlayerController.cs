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


    //private int scoreValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        //score.text = scoreValue.ToString();
        count = 0;
        lives = 3;

        SetCountText();
        SetLivesText();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        playAgainButton.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
            collision.gameObject.SetActive(false);
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

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MiniGame") && count >= 12)
        {
            SceneManager.LoadScene("Level2");
            lives = 3;
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2") && count >= 11)
        {
            winTextObject.SetActive(true);
            playAgainButton.SetActive(true);
            this.gameObject.SetActive(false);
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
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
}
