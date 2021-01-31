using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnWin : MonoBehaviour
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
        ToyGenerator.OnWin += Terminate;
    }

    private void OnDisable()
    {
        ToyGenerator.OnWin -= Terminate;
    }

    public void Terminate()
    {
        Destroy(this.gameObject);
    }
}
