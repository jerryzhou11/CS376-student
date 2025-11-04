using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject moneyPrefab;
    public GameObject fallingIcePrefab;
    public GameObject movingIcePrefab;
    
    [Header("Falling Objects Settings")]
    public float fallingSpawnInterval = 1.5f;
    public float spawnHeight = 6f;
    public float spawnRangeX = 8f;
    
    [Header("Moving Ice Settings")]
    public float movingIceSpawnInterval = 3f;
    public float movingIceHeightMin = 1f;
    public float movingIceHeightMax = 4f;
    public float spawnSideOffset = 12f;
    
    [Header("Game Timer")]
    public GameTimer gameTimer;
    
    private float fallingSpawnTimer;
    private float movingIceSpawnTimer;
    
    void Start()
    {
        fallingSpawnTimer = fallingSpawnInterval;
        movingIceSpawnTimer = movingIceSpawnInterval;
    }
    
    void Update()
    {
        // only spawn if game is running
        if (gameTimer != null && !gameTimer.IsGameRunning())
        {
            return;
        }
        
        // spawn falling objects (money or falling ice)
        fallingSpawnTimer -= Time.deltaTime;
        if (fallingSpawnTimer <= 0f)
        {
            SpawnFallingObject();
            fallingSpawnTimer = fallingSpawnInterval;
        }
        
        // spawn moving ice
        movingIceSpawnTimer -= Time.deltaTime;
        if (movingIceSpawnTimer <= 0f)
        {
            SpawnMovingIce();
            movingIceSpawnTimer = movingIceSpawnInterval;
        }
    }
    
    void SpawnFallingObject()
    {
        // random X position
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector2 spawnPosition = new Vector2(randomX, spawnHeight);
        
        // 66% chance money, 33% chance falling ice
        float randomValue = Random.Range(0f, 1f);
        GameObject prefabToSpawn = randomValue < 0.66f ? moneyPrefab : fallingIcePrefab;
        
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
    
    void SpawnMovingIce()
    {
        // random height
        float randomY = Random.Range(movingIceHeightMin, movingIceHeightMax);
        
        // always spawn from left, moving right
        float spawnX = -spawnSideOffset;
        
        Vector2 spawnPosition = new Vector2(spawnX, randomY);
        
        GameObject movingIce = Instantiate(movingIcePrefab, spawnPosition, Quaternion.identity);
        
        // set direction to move right
        MovingIce iceScript = movingIce.GetComponent<MovingIce>();
        if (iceScript != null)
        {
            iceScript.SetDirection(1f);
        }
    }
}