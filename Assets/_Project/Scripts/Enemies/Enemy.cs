using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 2.0f;

    private Mover2D _mover;

    private AnimationParamHandler _animParam;

    private EnemiesManager _enemiesManager;

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

        _enemiesManager = FindObjectOfType<EnemiesManager>();
    }

    private void OnEnable()
    {
        if (_enemiesManager != null)
        {
            Debug.Log("HO TROVARTO EnemiesManager E MI REGISTRO !");
            _enemiesManager.RegistEnemy(this);
        }
        else
        {
            Debug.LogError("EnemiesManager non trovato in scena!");
        }
    }

    private void OnDisable()
    {
        if (_enemiesManager != null)
        {
            _enemiesManager.RemoveEnemy(this);
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
