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
                        pselected = true; //will say that pikmin has been selected.
                    } 
                }


                else if (selectedTreasure != null && pselected)
                {
                    CurrentTreasure = selectedTreasure;
                    CurrentTreasure.activatedTreasure(true);

                    PlayerChar.activatedToTreasure(CurrentTreasure.transform.position);

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

            Vector2 mouseposition = Input.mousePosition;
            Ray worldRay = Camera.main.ScreenPointToRay(mouseposition);

            if (Physics.Raycast(worldRay, out RaycastHit hitinfo))
            {
                //CurrentTreasure.activatedTreasure(false);
                
                //Treasure selectedTreasure = hitinfo.transform.GetComponent<Treasure>();
                //once hit the treasure, it will call the position of the treasure that has been selected.

                //if (selectedTreasure != null)
                //{
                //    PlayerChar.currentState = PikminStates.Idle;
                //    CurrentTreasure = null;
                //} 


                if (PlayerChar != null)
                {
                    PlayerChar.currentState = PikminStates.Idle;
                    PlayerChar = null; //selection is emptied
                    pselected = false;
                }
                else if (PlayerChar == null)
                {
                    return;
                }

                CurrentTreasure = null;

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("treasure"))
         {

            if (PlayerChar == null)
            {
                Debug.Log("You null with PC");
            } else if (CurrentTreasure == null)
            {
                Debug.Log("You null with Treasure");

            }

            //PlayerChar.transform.SetParent(CurrentTreasure.transform);

                CurrentTreasure.activatedTreasure(false);
                //CurrentTreasure.pikminInserted++;

                

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

            pselected = false;
            PlayerChar = null;
        
    }

}


        
  
