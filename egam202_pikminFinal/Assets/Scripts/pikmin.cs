using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

//Testing global enum ==> Works for now
public enum PikminColors
{
    Red,
    Yellow,
    Blue,
}

public class pikmin : MonoBehaviour
{
 

    public PikminColors currentColors;
    private PikminColors lastColor;

    //the movement of Character
    public NavMeshAgent playerCharacter;
    GameObject marker;




    //Distance Marker
    public GameObject distanceMarker;
    GameObject madeDMarker;
    bool isCreated;

    private bool On;

    //Color Manager
    private Renderer myRend;

    // Start is called before the first frame update
    
    void Start()
    {
        myRend = this.GetComponent<Renderer>(); //Determines the activation color

        marker = this.transform.GetChild(0).gameObject; //getting the Marker
        marker.SetActive(false); //Not seen when the pikmin is not selected.
        
        ApplyColors();
    }

    // Update is called once per frame
    void Update()
    {  
        if (currentColors != lastColor)
        {
            ApplyColors();
        }
    }

    void ApplyColors()
    {
        lastColor = currentColors;

        switch (currentColors) 
        {
            case PikminColors.Red:
                myRend.material.color = Color.red;
                break;
            case PikminColors.Yellow:
                myRend.material.color = Color.yellow;
                break;
            case PikminColors.Blue:
                myRend.material.color = Color.blue;
                break;
            case PikminColors.Black:
                myRend.material.color = Color.black;
                break;
        }
    }



    //Once the Pikmin activation is true,
    public void activatedPikmin(bool activated)
    {
        if (activated)
        {
            marker.SetActive(true);
        }
        else if(!activated) 
        {
            marker.SetActive(false);
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

            madeDMarker.transform.position = destination; //Setting the distance marker
            playerCharacter.SetDestination(destination); //Also move the selected pikmin to the selected place
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Will be gone once it hits the player.
        if(collision.gameObject.name == "Sphere(Clone)")
        {
            Destroy(madeDMarker);
            isCreated = false;
        }

        //will change later

    }



}
