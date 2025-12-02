using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterController : MonoBehaviour
{
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float fireRange = 6.0f;
    [SerializeField] private AudioClip FireSound;

    private AudioSource _AudioSource;

    public GameObject BulletPrefab;

    private float _lastShootTime;

    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
        if (_AudioSource == null)
        {
            _AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Time.time - _lastShootTime > fireRate)
        {
            _lastShootTime = Time.time;
            Shoot();
        }
    }

    private GameObject FindNearestEnemy()
    {
        GameObject NearstEnemyFounded = null;
        GameObject[] enemiesList = GameObject.FindGameObjectsWithTag(Tags.Enemy);

        float nearstDistance = fireRange;

        foreach (GameObject enemy in enemiesList)
        {
            float CurDistance = Vector2.Distance(transform.position, enemy.transform.position);
            if (CurDistance < nearstDistance)
            {
                nearstDistance = CurDistance;
                NearstEnemyFounded = enemy;
            }
        }
        return NearstEnemyFounded;
    }

    public void Shoot()
    {
        GameObject Target = FindNearestEnemy();

        if (Target == null) return;

        Vector2 targetPos = Target.GetComponent<Rigidbody2D>().position;
        Vector2 playerPos = transform.position;
        Vector2 direction = (targetPos - playerPos);

        float spawnOffset = 0.5f;
        Vector2 spawnPosition = playerPos + direction * spawnOffset; // vado a spawnare fuori del rigidbody del player per non causare un problema di spostamento anomalo

        GameObject cloneBullet = Instantiate(BulletPrefab, spawnPosition, Quaternion.identity);

        if (cloneBullet != null)
        {
            if (FireSound != null && _AudioSource != null)
            {
                _AudioSource.PlayOneShot(FireSound);
            }

            cloneBullet.gameObject.GetComponent<Bullet>().Shoot(direction);
        }
        else
        {
            Debug.Log("Non hai assegnato il Prefab del Bullet !!!");
            return;
        }
    }

}
