using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    [SerializeField] private EnemyController _econtroller;

    private void Awake()
    {
        _econtroller = GetComponentInParent<EnemyController>();
    }

    public void DestroygameObject(string _state)
    {
        if (_state == "destroy") _econtroller.DestroyGOEnemy();
    }
}
