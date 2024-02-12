using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private float speed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y >= 8f){

            if (transform.parent != null){
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        } else {

            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        
    }
}
