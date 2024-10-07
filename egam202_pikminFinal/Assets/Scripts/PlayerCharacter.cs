using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

using UnityEngine.Timeline;
using static pikmin;


public class PlayerCharacter : MonoBehaviour
{
    // What happens here?
    // ==> See if the pikmin has been selected or not.
    // It will allow pikmins to move around.

  
    public Camera myCamera;
    private pikmin PlayerChar; //Individual Pikmins

    private Treasure CurrentTreasure; //treasure

    bool pselected;



    // Start is called before the first frame update
    void Start()
    {
        PlayerChar = null;
        pselected = false;
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

                Treasure selectedTreasure = hitinfo.transform.GetComponent<Treasure>();
                //once hit the treasure, it will call the position of the treasure that has been selected.

                if (selectedPikmin != null && PlayerChar == null)
                {
                    PlayerChar = selectedPikmin; //it will select the pikmin

                    if (PlayerChar.currentState == PikminStates.Idle)
                    {
                        PlayerChar.currentState = PikminStates.Active;
                    } 
                   

                    pselected = true;
                }

                else if (pselected && selectedTreasure != null)
                {
                    CurrentTreasure = selectedTreasure;
                    CurrentTreasure.activatedTreasure(true);

                    PlayerChar.activatedMovement(CurrentTreasure.transform.position);
                }

                else if(pselected) //if its not a treasure+pikmin yet you have to move around :)
                {
                    if (PlayerChar.currentState == PikminStates.Active)
                    {

                        PlayerChar.activatedMovement(hitinfo.point);
                        
                    }
                }
            }

        }


        if (Input.GetMouseButtonDown(1)) //When right-clicked, it will deactivate the Pikmin.
        {

            //CurrentTreasure.activatedTreasure(false);

            PlayerChar.currentState = PikminStates.Idle;
            PlayerChar = null; //selection is emptied
            pselected = false;

            CurrentTreasure = null;


        }
    }

    // what i need for the future:
    // 1. number of required minions attached to the Treasure
    // 2. number of minions currently attached
    // 3. what else

    void workingPikmins()
    {
        CurrentTreasure.pikminInserted++;

        PlayerChar.currentState = PikminStates.TryingToCarry;
        PlayerChar.transform.SetParent(CurrentTreasure.transform);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (PlayerChar != null)
        {
            if(collision.gameObject.tag == "treasure")
            {
                CurrentTreasure.activatedTreasure(false);

                workingPikmins();

                for (int i = 0; i < CurrentTreasure.pikminRequired; i++)
                {
                    if (CurrentTreasure.requiredPikmin[i] == null)
                    {
                        CurrentTreasure.requiredPikmin[i] = PlayerChar;
                    } 
                    
                    else if (CurrentTreasure.requiredPikmin[i] != null)
                    {
                        return;
                    }

                }

              
            }

            PlayerChar = null;
        }
    }

}


        
  
