using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    private void Update()
    {
        if (this.gameObject != null)
        {
            Destroy(gameObject, 1f);
           
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Debug.Log(collision.gameObject.tag);
      
        }
        Debug.Log("Collided");
    }

}
