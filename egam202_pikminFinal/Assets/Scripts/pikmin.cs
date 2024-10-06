using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class pikmin : MonoBehaviour
{
    public NavMeshAgent playerCharacter;
    GameObject marker;

    public GameObject distanceMarker;
    GameObject madeDMarker;
    bool isCreated;

    private Renderer myRend;

    private bool On;


    Color active;
    Color notActive;

    // Start is called before the first frame update
    
    void Start()
    {
        myRend = this.GetComponent<Renderer>(); //Determines the activation color

        marker = this.transform.GetChild(0).gameObject; //getting the Marker
        marker.SetActive(false); //Not seen when the pikmin is not selected.

        active = Color.black; //Color will change into black once selected
        notActive = myRend.material.color;

        
    }

    // Update is called once per frame
    void Update()
    {  
        
    }


    //Once the Pikmin activation is true,
    public void activatedPikmin(bool activated)
    {
        if (activated)
        {
            marker.SetActive(true);
            myRend.material.color = active;
        }
        else if(!activated) 
        {
            marker.SetActive(false);
            myRend.material.color = notActive;
            Destroy(madeDMarker);
        }

        On = activated;
    }

    public void activatedMovement(Vector3 destination)
    {
        if (On)
        {
            if (!isCreated)
            {
                madeDMarker = Instantiate(distanceMarker, destination, Quaternion.identity);
                isCreated = true;
            }

            madeDMarker.transform.position = destination;

            playerCharacter.SetDestination(destination);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Sphere(Clone)")
        {
            Destroy(madeDMarker);
            isCreated = false;
        }
    }



}
