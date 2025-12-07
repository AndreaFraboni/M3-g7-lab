using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 2.0f;

    private Mover2D _mover;
    private AnimationParamHandler _animParam;

    private void Awake()
    {
        _mover = GetComponent<Mover2D>();
        _animParam = GetComponent<AnimationParamHandler>();

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
        if (_target == null)
        {
            _mover.SetSpeed(0);
            return;
        }
        Vector2 toTarget = _target.position - transform.position;
        Vector2 input = toTarget.normalized;

        if (input.x != 0 || input.y != 0)
        {
            _animParam.SetDirectionalSpeedParams(input);
        }

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
