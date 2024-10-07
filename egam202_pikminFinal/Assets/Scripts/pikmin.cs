using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;



public enum PikminStates
{
    Idle,
    Active,
    Carrying,
    TryingToCarry
}

public class pikmin : MonoBehaviour
{

    public PikminStates currentState;
    //private PikminStates changedState;


    public enum PikminColors
    {
        Red,
        Yellow,
        Blue,
    }

    public PikminColors currentColors;
    private PikminColors lastColor;

    //the movement of Character
    public NavMeshAgent playerCharacter;
    GameObject marker;

    //Distance Marker
    public GameObject distanceMarker;
    GameObject madeDMarker;
    bool isCreated;

    //Color Manager
    private Renderer myRend;

    // Start is called before the first frame update
    
    void Start()
    {
        myRend = this.GetComponent<Renderer>(); //Determines the color

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

       
        switch (currentState)
        {
            case PikminStates.Idle:
                IdleState();
                break;
            case PikminStates.Active:
                ActiveState();
                break;
            case PikminStates.Carrying:
                CarryingState();
                break;
            case PikminStates.TryingToCarry:
                TryingToCarryState();
                break;
        
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
        }
    }

    void IdleState()
    {
        playerCharacter.enabled = false; //cannot move
        marker.SetActive(false);

        isCreated = false;
        Destroy(madeDMarker);
    }

    void ActiveState()
    {
        playerCharacter.enabled = true; //Can move around
        marker.SetActive(true); //Marker On

       
    }

    void CarryingState()
    {
        playerCharacter.enabled = false; //cannot move, child of treasure.
    }

    void TryingToCarryState()
    {
        playerCharacter.enabled = false; //cannot move, child of treasure.
    }


    public void activatedMovement(Vector3 destination, bool treasure)
    {
        isCreated = treasure;
        
        if (!isCreated)
        {
            madeDMarker = Instantiate(distanceMarker, destination, Quaternion.identity);
            madeDMarker.transform.position = destination; //Setting the distance marker
            isCreated = true;
        
        }
        playerCharacter.SetDestination(destination); //Also move the selected pikmin to the selected place
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
