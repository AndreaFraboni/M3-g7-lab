using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 50;
    [SerializeField] private float _lifeSpan = 5f;

    public float _speed = 10f;

    private Mover2D _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover2D>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifeSpan);
    }

    public void Shoot(Vector2 dir)
    {
        _mover.SetSpeed(_speed);
        _mover.SetAndNormalizeInput(dir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag(Tags.Enemy))
            {
                //collision.gameObject.GetComponent<Enemy>().DestroyMe();
                collision.gameObject.GetComponent<LifeController>().TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }

}
