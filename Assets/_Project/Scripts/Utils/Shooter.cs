using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] protected float _fireInterval = 0.5f;
    [SerializeField] private float _offset = 1.5f;
    [SerializeField] private AudioClip FireSound;

    private AudioSource _AudioSource;

    protected float _lastShootTime;

    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
        if (_AudioSource == null)
        {
            _AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public virtual void Shoot(Vector2 direction)
    {
        _lastShootTime = Time.time;

        if (FireSound != null && _AudioSource != null)
        {
            _AudioSource.PlayOneShot(FireSound);
        }

        Bullet clonedBullet = Instantiate(_bulletPrefab);
        clonedBullet.transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * _offset;
        clonedBullet.Shoot(direction);
    }

    public virtual bool CanShootNow()
    {
        return Time.time - _lastShootTime > _fireInterval;
    }

    public virtual void TryToShoot(Vector2 direction)
    {
        if (CanShootNow())
        {
            Shoot(direction);
        }    
    }

}
