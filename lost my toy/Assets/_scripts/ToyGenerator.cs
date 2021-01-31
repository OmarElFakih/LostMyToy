using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToyGenerator : MonoBehaviour
{
    public static ToyGenerator Instance = null;

    public delegate void Win();
    public static Win OnWin;

    public delegate void Loose();
    public static Loose OnLoose;

    private void Awake()
    {
        if (ToyGenerator.Instance == null)
        {
            ToyGenerator.Instance = this;
        
        }
        else if (ToyGenerator.Instance != null && ToyGenerator.Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private Vector2 _mainToyIndex = new Vector2(0, 0);

    

    public GameObject spotLight;
    public GameObject[] toyList;
    public Color[] colorList;


    public float fodderToys;
    public float fodderIncrease;

    public Transform sampleToyLocation;

    public Vector2 xRange;
    public Vector2 yRange;
    public Vector2 zRange;

    public float delay;

    public AudioClip winTune;
    public AudioClip looseTune;
    
    




    // Start is called before the first frame update
    void Start()
    {
        SpawnRoutine();
        StartCoroutine(DeleyedStart());
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelfDestruct()
    {
        Destroy(this.gameObject);
    }

    private Vector2 RandomVector2()
    {
        int _x = Random.Range(0, toyList.Length);
        int _y = Random.Range(0, colorList.Length);

        return new Vector2(_x, _y);
    }

    private Vector3 RandomLocation()
    {
        float _x = Random.Range(xRange.x, xRange.y);
        float _y = Random.Range(yRange.x, yRange.y);
        float _z = Random.Range(zRange.x, zRange.y);

        return new Vector3(_x, _y, _z);

    }

    public void SpawnRoutine() {
        _mainToyIndex = RandomVector2();

        GameObject main = Instantiate(toyList[(int)_mainToyIndex.x], RandomLocation(), Quaternion.Euler(0, 0, Random.Range(0, 360)));
        //assing color
        main.GetComponent<Toy>().AssignColor(colorList[(int)_mainToyIndex.y]);
        main.GetComponent<Toy>().type = ToyType.Main;

        GameObject sample = Instantiate(toyList[(int)_mainToyIndex.x], sampleToyLocation.position, Quaternion.identity);
        //assign color
        sample.GetComponent<Toy>().AssignColor(colorList[(int)_mainToyIndex.y]);
        sample.GetComponent<Toy>().type = ToyType.Sample;

        Vector2 _nextToy = new Vector2(0, 0);

        for (int i = 0; i < fodderToys; i++) {
            do
            {

                _nextToy = RandomVector2();

            } while (_nextToy == _mainToyIndex);

            GameObject fodderToy = Instantiate(toyList[(int)_nextToy.x], RandomLocation(), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            //assign color
            fodderToy.GetComponent<Toy>().AssignColor(colorList[(int)_nextToy.y]);

        } 


    }

    public void EnableTimer()
    {
        GetComponent<Timer>().enabled = true;
    }


    public void WinRoutine(Vector3 position)
    {
        GetComponent<AudioSource>().PlayOneShot(winTune);
        fodderToys += fodderIncrease;
        GetComponent<Timer>().enabled = false;
        ScoreManager.instance.AddScore();
        Instantiate(spotLight, position, Quaternion.identity);
        StartCoroutine(DeleyedRestart());
    }

    public void LooseRoutine()
    {
        GetComponent<AudioSource>().PlayOneShot(looseTune);
        GetComponent<Timer>().enabled = false;
        OnLoose();
    }

    public void SpawnSpotlight(Vector3 position)
    {
        Instantiate(spotLight, position, Quaternion.identity);
    }

    public IEnumerator DeleyedStart()
    {
        
        yield return new WaitForSeconds(delay);
        
        EnableTimer();
    }

    public IEnumerator DeleyedRestart()
    {
        GetComponent<Timer>().LerpInterval();
        yield return new WaitForSeconds(delay);
        OnWin();
        SpawnRoutine();
        StartCoroutine(DeleyedStart());

    }

    


}
