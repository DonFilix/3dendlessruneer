using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject Pleyr;
    private CharacterMovement CameraTilt;
    private Transform PlayerPos;
    private Vector3 CameraPos;
    private Vector3 StedyCamera;
    private float zzz;
    private Quaternion target;
    // Use this for initialization
    void Start () {

        PlayerPos = Pleyr.transform;
        CameraPos = this.transform.position - PlayerPos.position;
        

    }
	
	// Update is called once per frame
	void Update () {

        if (Pleyr.GetComponent<CharacterMovement>().laneNumber == 0) zzz = -5;
        if (Pleyr.GetComponent<CharacterMovement>().laneNumber == 1) zzz = 0;
        if (Pleyr.GetComponent<CharacterMovement>().laneNumber == 2) zzz = 5;
        target = Quaternion.Euler(20, 10, zzz);
        //Player.GetComponent<CharacterMovement>.
        StedyCamera = PlayerPos.position + CameraPos;
        //StedyCamera.x = 0;
        StedyCamera.y = Mathf.Clamp(StedyCamera.y, 2, 2.2f);//2;
        this.transform.position = StedyCamera;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 50);
    }

}
