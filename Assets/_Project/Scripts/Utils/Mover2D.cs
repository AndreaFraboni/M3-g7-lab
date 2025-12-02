using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover2D : MonoBehaviour
{
    private float _speed = 5.0f;
    private Rigidbody2D _rb;
    private Vector2 _input;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_input != Vector2.zero)
        {
            _rb.MovePosition(_rb.position + _input * (_speed * Time.deltaTime));
        }
    }

    public void SetInput(Vector2 input)
    {
        _input = input;
    }

    public void SetAndNormalizeInput(Vector2 input)
    {
        float sqrMagnitude = input.sqrMagnitude;
        if (sqrMagnitude > 1)
        {
            input /= Mathf.Sqrt(sqrMagnitude);
        }
        SetInput(input);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
