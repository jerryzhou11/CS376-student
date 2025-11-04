using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverImage;
    public Text finalScoreText;
    
    [Header("Game Components")]
    public GameTimer gameTimer;
    public GameObject player;
    
    private bool gameEnded = false;
    
    void Start()
    {
        // make sure game over image is hidden at start
        if (gameOverImage != null)
        {
            gameOverImage.SetActive(false);
        }
    }
    
    void Update()
    {
        // check if game should end
        if (!gameEnded && gameTimer != null && !gameTimer.IsGameRunning())
        {
            EndGame();
        }
        
        // check for restart input
        if (gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    
    public void EndGame()
    {
        gameEnded = true;
        
        // stop player movement
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = false;
            }
            
            // stop player physics
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
        
        // show game over image
        if (gameOverImage != null)
        {
            gameOverImage.SetActive(true);
        }
        
        // display final score
        if (finalScoreText != null)
        {
            float finalScore = ScoreKeeper.GetScore();
            finalScoreText.text = string.Format("Final Score: {0}", finalScore);
        }
        
        //disable spawner
        ObjectSpawner spawner = FindObjectOfType<ObjectSpawner>();
        if (spawner != null)
        {
            spawner.enabled = false;
        }
        // destroy all remaining collectibles
        DestroyAllCollectibles();
    }
    
    void DestroyAllCollectibles()
    {
        // destroy all money
        GameObject[] moneys = GameObject.FindGameObjectsWithTag("Money");
        foreach (GameObject money in moneys)
        {
            Destroy(money);
        }
        
        // destroy all falling ice
        GameObject[] fallingIces = GameObject.FindGameObjectsWithTag("FallingIce");
        foreach (GameObject ice in fallingIces)
        {
            Destroy(ice);
        }
        
        // destroy all moving ice
        GameObject[] movingIces = GameObject.FindGameObjectsWithTag("MovingIce");
        foreach (GameObject ice in movingIces)
        {
            Destroy(ice);
        }
    }
    
    void RestartGame()
    {
        // reset score
        ScoreKeeper.ResetScore();
        
        // reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}