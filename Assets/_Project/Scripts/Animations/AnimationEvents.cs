using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerController _pcontroller;

    private void Awake()
    {
       //_pcontroller = GetComponent<PlayerController>();
    }

    public void DestroygameObject(string _state)
    {
        if (_state == "destroy") _pcontroller.DestroyGOPlayer();
    }

}
