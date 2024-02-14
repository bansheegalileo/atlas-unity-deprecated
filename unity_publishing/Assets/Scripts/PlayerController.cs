using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text winLoseText;
    public Image winLoseBG;
    void Start()
    {
        SetScoreText();
        SetHealthText();
        winLoseBG.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        
        transform.Translate(movement * speed * Time.fixedDeltaTime, Space.World);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
        }
        else if (other.CompareTag("Goal"))
        {
            WinGame();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health;
    }

    void Update()
    {
        if (health == 0)
        {
            GameOver();
        }
    }
    
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        health = 5;
        score = 0;
    }
    void GameOver()
    {
        winLoseText.text = "Game Over!";
        winLoseText.color = Color.white;
        winLoseBG.color = Color.red;
        winLoseBG.gameObject.SetActive(true);
        StartCoroutine(LoadScene(3f));
    }
    void WinGame()
    {
        winLoseText.text = "You Win!";
        winLoseText.color = Color.black;
        winLoseBG.color = Color.green;
        winLoseBG.gameObject.SetActive(true);
        StartCoroutine(LoadScene(3f));
    }
}
