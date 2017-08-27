using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsRotate : MonoBehaviour {
    
    private Transform PlayerPos; 
    private Transform IntendedRotation;
    private int direction; 
    // Use this for initialization
    void Start() {

        direction = Random.Range(0, 9);
        transform.Rotate(0,0,0);
        IntendedRotation = this.transform;
        //transform.position=new Vector3 (Random.Range(0, 9), Random.Range(0, 9), transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //  Debug.Log(PlayerPos.position.z + "++++++++++++++++++" + this.transform.position.z);
        

        if (this.transform.position.z - PlayerPos.position.z > 30)
        {

            if (direction < 5)
            {
                transform.Rotate(0, 0, Time.deltaTime * 60);
               // if (transform.rotation.z == 360) transform.Rotate(0, 0, 0);
            }else
                
            if(direction >= 5)
            {
                transform.Rotate(0, 0, Time.deltaTime * -60);
               // if (transform.rotation.z == -360) transform.Rotate(0, 0, 0);
            }

            // transform.Rotate(Vector3.back, Time.deltaTime * 10, Space.World);
        }

       transform.Rotate(0,0,IntendedRotation.rotation.z);
                
     //   if (transform.position.x > 0)
       // {
      //    transform.position = new Vector3(transform.position.x-0.01f, transform.position.y, transform.position.z);
      //  }
      //  if (transform.position.y > 0)
      //  {
       //     transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
       // }
    }

}
