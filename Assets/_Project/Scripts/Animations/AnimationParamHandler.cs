using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationParamHandler : MonoBehaviour
{
    [SerializeField] private string _verticalSpeedParamName = "vSpeed";
    [SerializeField] private string _horizontalSpeedParamName = "hSpeed";

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    public void SetDirectionalSpeedParams(Vector2 speed)
    {
        SetHorizontalSpeedParam(speed.x);
        SetVerticalSpeedParam(speed.y);
    }

    public void SetVerticalSpeedParam(float speed)
    {
        _anim.SetFloat(_verticalSpeedParamName, speed);
    }

    public void SetHorizontalSpeedParam(float speed)
    {
        _anim.SetFloat(_horizontalSpeedParamName, speed);
    }

    public void SetBoolParam(string stateParam, bool value)
    {
        _anim.SetBool(stateParam, value);
    }

}
