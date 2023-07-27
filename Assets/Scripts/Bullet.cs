
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 500f;
    [SerializeField] private float maxLifeTime = 10f;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * _speed);

        Destroy(this.gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
