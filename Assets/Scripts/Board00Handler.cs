using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board00Handler : MonoBehaviour
{
    static Board00Handler instance;

    GameObject testPlayer;
    List<GameObject> markers = new List<GameObject>();
    GameObject markerParent;


    // Start is called before the first frame update
    void Start()
    {
        //Ensuring Class remains singleton
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);


        //Add Marker Array Data
        markerParent = GameObject.Find("Board00 Markers");
        int childCountMarkers = markerParent.transform.childCount;

        for (int i = 0; i < childCountMarkers; i++)
        {
            markers.Add(markerParent.transform.GetChild(i).gameObject);
        }

        Debug.Log(markers.Count);

        for (int i = 0; i < markers.Count; i++)
        {
            Debug.Log(markers[i].name);
        }


        //Instaniate/Get Test Player 
        testPlayer = GameObject.Find("PlayerLocation");

        //center
        testPlayer.transform.position = new Vector3(markers[0].transform.position.x, markers[0].transform.position.y + 1.75f, markers[0].transform.position.z); //@ y add player height so flush on board
        //1st quadrant
        testPlayer.transform.position = new Vector3(markers[0].transform.position.x-(markers[0].GetComponent<Collider>().bounds.size.x/4), markers[0].transform.position.y + 1.75f, markers[0].transform.position.z+ (markers[0].GetComponent<Collider>().bounds.size.z / 4));
    }

    // Update is called once per frame
    void Update()
    {
        //Check Marker Tags
    }
}
