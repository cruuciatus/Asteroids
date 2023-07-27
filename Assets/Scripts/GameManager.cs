
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;

    public static GameManager Instance { get; private set; }
    [SerializeField] private int score;
    [SerializeField] private int lives;
    [SerializeField] private float respawnTime;
    [SerializeField] private float timeForBackCollision;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private AudioSource audio;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            NewGame();
        }
    }
    public void AsteroidDestroyed(Asteroid asteroid)
    {
        audio.Play();
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();

        if (asteroid._size < 0.7f)
        {
            SetScore(score + 100);
        }
        else if (asteroid._size < 1.4f)
        {
            SetScore(score + 50);
        }
        else
        {
            SetScore(score + 25);
        }
    }

    public void NewGame()
    {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        for (int i = 0; i < asteroids.Length; i++)
        {
            Destroy(asteroids[i].gameObject);
        }

        gameOverUI.SetActive(false);

        SetScore(0);
        SetLives(3);

        Respawn();
    }
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }


    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCOllisions), timeForBackCollision);
    }

    private void TurnOnCOllisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void PlayerDied()
    {


        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
        this.gameObject.SetActive(false);
        SetLives(lives - 1);
        if (lives <= 0)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void MenuInGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            gameOverUI.SetActive(true);
        }
    }
}
