using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {
    public GameObject[] Sections;
    private List<GameObject> PlatformsInGame = new List<GameObject>();
    private Transform PlayerPos;
    private float Z = -4.0f;
    private float Size = 8.0f;
    private int N = 5;
    // Use this for initialization

    private void GeneratePlatform(int index = -1)
    {
        GameObject Platform;
        Platform = Instantiate(Sections[index]) as GameObject;
        Platform.transform.SetParent(this.transform);
        Platform.transform.position = Vector3.forward * Z;
        Z += Size;
        PlatformsInGame.Add(Platform);

    }
    void Start () {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        GeneratePlatform(0);
        GeneratePlatform(0);
        GeneratePlatform(0);


        for (int i = 0; i < N; i++)
        {

            GeneratePlatform(Random.Range(0, Sections.Length)); 
        
        }
    }   

    private void CleanupPlatform()
    {
        Destroy(PlatformsInGame[0]);
        PlatformsInGame.RemoveAt(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerPos.position.z - 8 > (Z - N * Size))
        {
          
            GeneratePlatform(Random.Range(0, Sections.Length));
            
            CleanupPlatform();
        }
	}
}
