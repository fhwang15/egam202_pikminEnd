using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

//State of enum, making it global.
public enum PikminStates
{
    Idle,
    Active,
    Carrying,
    TryingToCarry
}


public class PlayerCharacter : MonoBehaviour
{
    // What happens here?
    // ==> See if the pikmin has been selected or not.
    // It will allow pikmins to move around.


    //State of the Pikmin

    public PikminStates currentState;
    private PikminStates changedState;

    //

    public Camera myCamera;

    private pikmin PlayerChar; //Individual Pikmins

    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseposition = Input.mousePosition;
            Ray worldRay = Camera.main.ScreenPointToRay(mouseposition);
            //Determine where the mouse is clicked


            if (Physics.Raycast(worldRay, out RaycastHit hitinfo)) 
            {
                pikmin selectedPikmin = hitinfo.transform.gameObject.GetComponent<pikmin>();
                //If pikmin is hitted, then it will call that pikmin and put it in here.

                if (PlayerChar == null) //if the Player character is not selected,
                {
                    PlayerChar = selectedPikmin; //it will select the pikmin
                    PlayerChar.activatedPikmin(true); //indication


                } 
                
                else if(PlayerChar != null) //if the Player character is already selected, but has been clicked
                {
                    PlayerChar.activatedMovement(hitinfo.point);
                }
            }

        }

        if (Input.GetMouseButtonDown(1)) //When right-clicked, it will deactivate the Pikmin.
        {
            PlayerChar.activatedPikmin(false); //deactivate pikmin
            PlayerChar = null; //selection is emptied

        }
    }

    


}
