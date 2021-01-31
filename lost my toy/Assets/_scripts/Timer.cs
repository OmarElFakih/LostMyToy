using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public UnityEvent OnTimer;

    [SerializeField]
    private bool _loop = false;

    [SerializeField]
    private bool _executeOnStart = false;

    [SerializeField]
    private float _intervalLength = 0f;

    public bool lerpIntervals;

    [SerializeField]
    private float _targetInterval = 0f;

    [SerializeField]
    private float _lerpT = 0;

    private float _nextTimer = 0f;

    public bool canExecute = true;

    public Image clockImage;

    private void OnEnable()
    {
        _nextTimer = Time.time + _intervalLength;
    }

    private void Awake()
    {
        
         //   _nextTimer = Time.time + _intervalLength;
        
    }


    private void Start()
    {
        if (_executeOnStart)
        {
            OnTimer.Invoke();
        }

        //_nextTimer = Time.time + _intervalLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextTimer && canExecute)
        {
            OnTimer.Invoke();

            if (lerpIntervals)
            {
                LerpInterval();
            }

            if (_loop)
            {
                _nextTimer = Time.time + _intervalLength;
            }
        }

        if (clockImage != null)
        {
            clockImage.fillAmount = PercentageLeft();
        }

        //Debug.Log(PercentageLeft());
    }


    public float PercentageLeft()
    {
        return ((_nextTimer - Time.time) / _targetInterval) / 2;
    }

    public void LerpInterval()
    {
        _intervalLength = Mathf.Lerp(_intervalLength, _targetInterval, _lerpT * Time.deltaTime);
    }

    public void test()
    {
        Debug.Log("executed");
    }
}
