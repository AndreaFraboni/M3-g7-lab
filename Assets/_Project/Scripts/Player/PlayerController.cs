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

    private AudioSource _AudioSource;

    private void Awake()
    {
        _mover = GetComponent<Mover2D>();
        _shooter = GetComponent<Shooter>();
        _cam = Camera.main;

        _AudioSource = GetComponent<AudioSource>();
        if (_AudioSource == null)
        {
            _AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(h, v);

        _mover.SetSpeed(_speed);
        _mover.SetAndNormalizeInput(input);

        if (_shooter != null)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                mouseScreenPosition.z = -_cam.transform.position.z; // distanza tra camera e piano XY
                Vector3 mouseWorldPosition = _cam.ScreenToWorldPoint(mouseScreenPosition);
                Vector3 shootDirection = mouseWorldPosition - transform.position;
                
                if (shootDirection != Vector3.zero) shootDirection.Normalize();

                _shooter.TryToShoot(shootDirection);
            }
        }

    }

    public void DestroyPlayer()
    {
        if (DeathSound != null)
        {
            AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        }
        Destroy(gameObject);
    }

}
