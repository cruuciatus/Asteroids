
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _maxLifetimeAsteroid = 30f;
    

    public float _size = 1f;
    public float _minSize = 0.5f;
    public float _maxSize = 1.5f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private GameManager gameManager;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

        this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
        this.transform.localScale = Vector3.one * this._size;

        _rigidbody.mass = this._size * 2f;

        Destroy(gameObject, _maxLifetimeAsteroid);
    }
    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * _speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Bullet")
        {
            if ((this._size * 0.5f) >= this._minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            GameManager.Instance.AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private Asteroid CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half._size = this._size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * _speed);

        return half;
    }

}
