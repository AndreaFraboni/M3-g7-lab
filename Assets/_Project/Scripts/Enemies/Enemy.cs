using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 2.0f;

    private Mover2D _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover2D>();

        if (_target == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag(Tags.Player);
            if (go != null)
            {
                _target = go.transform;
            }
        }

    }
    private void FixedUpdate()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        if (_target == null) return;
        Vector2 toTarget = _target.position - transform.position;
        Vector2 input = toTarget.normalized;
        _mover.SetSpeed(_speed);
        _mover.SetAndNormalizeInput(input);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag(Tags.Player))
            {
                _target.gameObject.GetComponent<PlayerController>().DestroyPlayer();
            }
        }
    }

}
