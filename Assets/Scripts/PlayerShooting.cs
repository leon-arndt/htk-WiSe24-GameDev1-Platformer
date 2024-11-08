using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Projectile _bulletPrefab;
    private float _timeBetweenShots = 0.15f;
    private float _timeSinceLastShot = 0f;

    private void Update()
    {
        _timeSinceLastShot += Time.deltaTime;
        if (Input.GetButton("Fire1") && _timeSinceLastShot >= _timeBetweenShots)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _timeSinceLastShot = 0f;
        var bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
        bullet.SetDirection(Vector2.right);
    }
}