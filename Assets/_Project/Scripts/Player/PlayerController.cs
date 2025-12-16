using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed = 6.0f;
    [SerializeField] private AudioClip DeathSound;

    private Mover2D _mover;
    private Shooter _shooter;
    private Camera _cam;
    private AnimationParamHandler _animParam;
    private CircleCollider2D _Collider2D;
    private Rigidbody2D _rb;

    private AudioSource _AudioSource;

    private float v, h;

    private bool isWalking = false;
    private bool isAlive = true;

    private void Awake()
    {
        _mover = GetComponent<Mover2D>();
        _shooter = GetComponent<Shooter>();
        _animParam = GetComponent<AnimationParamHandler>();
        _Collider2D = GetComponent<CircleCollider2D>();
        _rb = GetComponent<Rigidbody2D>();

        _cam = Camera.main;

        _AudioSource = GetComponent<AudioSource>();
        if (_AudioSource == null)
        {
            _AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (isAlive)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            Vector2 input = new Vector2(h, v);

            isWalking = input != Vector2.zero;

            _animParam.SetBoolParam("isWalking", isWalking);

            if (isWalking)
            {
                _animParam.SetVerticalSpeedParam(v);
                _animParam.SetHorizontalSpeedParam(h);
            }

            _mover.SetSpeed(_speed);
            _mover.SetAndNormalizeInput(input);

            //if (_shooter != null)
            //{
            //    if (Input.GetMouseButtonDown(0))
            //    {
            //        Vector3 mouseScreenPosition = Input.mousePosition;
            //        mouseScreenPosition.z = -_cam.transform.position.z; // distanza tra camera e piano XY
            //        Vector3 mouseWorldPosition = _cam.ScreenToWorldPoint(mouseScreenPosition);
            //        Vector3 shootDirection = mouseWorldPosition - transform.position;
            //        if (shootDirection != Vector3.zero) shootDirection.Normalize()
            //        _shooter.TryToShoot(shootDirection);
            //    }
            //}
        }
    }

    public void DestroyPlayer()
    {
        isAlive = false;

        if (DeathSound != null)
        {
            AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        }

        if (_Collider2D != null) _Collider2D.enabled = false;
        if (_rb != null) _rb.simulated = false;

        _animParam.SetBoolParam("isDying", true);

        //Destroy(gameObject);
    }

    public void DestroyGOPlayer()
    {
        Destroy(gameObject);
    }

}
