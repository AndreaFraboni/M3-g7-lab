using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _currenthp = 100;
    [SerializeField] private int _maxHP = 100;
    [SerializeField] private int _lives = 3;
    [SerializeField] private AudioClip DeathSound;
    [SerializeField] private AudioClip ImpactSound1;
    [SerializeField] private AudioClip ImpactSound2;
    [SerializeField] private AudioClip ImpactSound3;

    private AudioSource _AudioSource;

    private void Awake()
    {
        _AudioSource = GetComponent<AudioSource>();
        if (_AudioSource == null)
        {
            _AudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Getter
    public int GetHp() => _currenthp;
    public int GetMaxHp() => _maxHP;
    public int GetLives() => _lives;

    // Setter
    public int SetLives(int lives) => _lives = lives;
    public void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHP);

        if (hp != _currenthp)
        {
            _currenthp = hp;

            if (_currenthp <= 0)
            {
                _lives--;

                if (_lives == 0)
                {
                    Defeated();
                }
                else
                {
                    _currenthp = 100;
                }
            }
        }
    }

    // Functions
    public void AddHp(int amount) => SetHp(_currenthp + amount);
    public void TakeDamage(int damage)
    {
        if (ImpactSound1 != null && ImpactSound2 != null && ImpactSound3 != null && _AudioSource != null)
        {
            int randomnumber = Random.Range(0, 100 + 1);

            if (randomnumber >= 0 && randomnumber <= 30)
            {
                _AudioSource.clip = ImpactSound1;
            }
            if (randomnumber > 30 && randomnumber <= 50)
            {
                _AudioSource.clip = ImpactSound2;
            }
            if (randomnumber > 50 && randomnumber <= 100)
            {
                _AudioSource.clip = ImpactSound3;
            }
            _AudioSource.Play();
        }
        AddHp(-damage);
    }
    public void TakeHeal(int amount)
    {
        AddHp(amount);
    }

    private void Defeated()
    {
        if (DeathSound != null && _AudioSource != null)
        {
            AudioSource.PlayClipAtPoint(DeathSound, transform.position);
        }
        Destroy(gameObject);
    }

}