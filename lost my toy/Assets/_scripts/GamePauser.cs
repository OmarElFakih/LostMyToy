using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauser : MonoBehaviour
{
    public static GamePauser Instance = null;


    public delegate void Pause();
    public static Pause OnPause;

    public delegate void UnPause();
    public static UnPause OnUnpause;

    private void Awake()
    {
        if (GamePauser.Instance == null)
        {
            GamePauser.Instance = this;
        }
        else if (GamePauser.Instance != null && GamePauser.Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            OnPause();
        }
    }

    public void UnPauseGame()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            OnUnpause();
        }
    }
}
