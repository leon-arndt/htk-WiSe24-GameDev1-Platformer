using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10f;
    private Vector2 _direction;

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
}