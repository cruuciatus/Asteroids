
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid _asteroidPrefab;
    [SerializeField] private float _spawnRate = 2f;
    [SerializeField] private int _spawnAmount = 1;
    [SerializeField] private float _spawnDistance = 15f;
    [SerializeField] private float _trajectoryVariance = 15f;
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this._spawnRate, this._spawnRate);
    }

    private void Spawn()
    {
        for (int i = 0; i < this._spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this._trajectoryVariance, this._trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Asteroid asteroid = Instantiate(this._asteroidPrefab, spawnPoint, rotation);
            asteroid._size = Random.Range(asteroid._minSize, asteroid._maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection); 
        }
    }
}
 