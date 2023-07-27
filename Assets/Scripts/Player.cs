
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] private float _thrustSpeed = 1.0f;
    [SerializeField] private float _turnSpeed = 1.0f;
    [SerializeField] private AudioSource audioShoot, audioDead;
    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private float _turnDirection;
    private GameManager gameManager;
   
        


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W);

        if (Input.GetKey(KeyCode.A))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0f;

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this._thrustSpeed);
        }
        if (_turnDirection != 0f)
        {
            _rigidbody.AddTorque(_turnDirection * this._turnSpeed);
        }
    }

    private void Shoot()
    {
        audioShoot.Play();
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            audioDead.Play();
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0f;

            this.gameObject.SetActive(false);

            gameManager.PlayerDied();
        }
    }
}
