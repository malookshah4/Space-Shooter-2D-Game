using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float _rotateSpeeed = 20.0f;

    [SerializeField]
    private GameObject _explosionPrefab;

     private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeeed * Time.deltaTime);
        
    }


    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Laser")
        {
           Instantiate(_explosionPrefab,transform.position,Quaternion.identity);
           Destroy(other.gameObject);
           _spawnManager.StratSpawning();
           Destroy(this.gameObject,0.5f);
        }
    }

}
