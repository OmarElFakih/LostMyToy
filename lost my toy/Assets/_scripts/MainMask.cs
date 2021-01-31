using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMask : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        ToyGenerator.OnWin += Animate;
    }

    private void OnDisable()
    {
        ToyGenerator.OnWin -= Animate;
    }

    public void Animate()
    {
        GetComponent<Animator>().SetTrigger("Slide");
    }

}
