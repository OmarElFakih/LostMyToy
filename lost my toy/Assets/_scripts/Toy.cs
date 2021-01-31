using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    private bool _moving = false;

    private float _startPosX;
    private float _startPosY;

    public SpriteRenderer variableColorRenderer;
    public ToyType type = ToyType.Fodder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_moving && type == ToyType.Fodder) {
            Movement();
        }
    }

    private void OnEnable()
    {
        ToyGenerator.OnWin += Terminate;
        ToyGenerator.OnLoose += Reveal;
        GamePauser.OnPause += PauseRoutine;
        GamePauser.OnUnpause += UnpauseRoutine;
    }

    private void OnDisable()
    {
        ToyGenerator.OnWin -= Terminate;
        ToyGenerator.OnLoose -= Reveal;
        GamePauser.OnPause -= PauseRoutine;
        GamePauser.OnUnpause -= UnpauseRoutine;
    }

    private void OnMouseDown()
    {
        if (type == ToyType.Fodder)
        {
            MovementSetUp();
        }
        else if(type == ToyType.Main)
        {
            Debug.Log("Main Toy Found");
            ToyGenerator.Instance.WinRoutine(transform.position);
        }
    }

    private void OnMouseUp()
    {
        _moving = false;
    }

    private void MovementSetUp()
    {
        Vector3 _mousePosition;
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

        _startPosX = _mousePosition.x - this.transform.position.x;
        _startPosY = _mousePosition.y - this.transform.position.y;

        _moving = true;
    }

    private void Movement()
    {
        Vector3 _mousePosition;
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

        bool _inRange = _mousePosition.x > ToyGenerator.Instance.xRange.x
                     && _mousePosition.x < ToyGenerator.Instance.xRange.y
                     && _mousePosition.y > ToyGenerator.Instance.yRange.x
                     && _mousePosition.y < ToyGenerator.Instance.yRange.y;
        
        if(_inRange)
            transform.position = new Vector3(_mousePosition.x - _startPosX, _mousePosition.y - _startPosY, transform.position.z);
    }


    public void AssignColor(Color color)
    {
        variableColorRenderer.color = color;
    }

    public void Terminate()
    {
        Destroy(this.gameObject);
    }

    public void Reveal()
    {
        if (type == ToyType.Main)
        {
            ToyGenerator.Instance.SpawnSpotlight(transform.position);
        }
    }

    public void PauseRoutine()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void UnpauseRoutine()
    {
        GetComponent<Collider2D>().enabled = true;
    }

}


public enum ToyType
{
    Fodder,
    Main,
    Sample
}
