using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour {
    private PlayerMovement playerMovement;
    private float maxInteractionDistance = 2f;
    public LayerMask layersToRaycast;

    private Vector2 lastValidMovementDir;

    private void Start() {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() {
        handleRaycast();
    }

    void handleRaycast() {
        Vector2 currentMovementDir = playerMovement.movementDir;

        //if the current movement direction is zero, use the last valid movement direction
        if (currentMovementDir != Vector2.zero) {
            lastValidMovementDir = currentMovementDir;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastValidMovementDir, maxInteractionDistance, layersToRaycast);

        Debug.DrawRay(transform.position, lastValidMovementDir * maxInteractionDistance, Color.black);

        if (hit.collider != null) {

            Debug.DrawRay(transform.position, lastValidMovementDir * maxInteractionDistance, Color.red);

            handleRaycastOnBathLayer(hit);
            handleRaycastOnReceptionDeskLayer(hit);


        }
    }

    void handleRaycastOnBathLayer(RaycastHit2D hit) {
        string bathLayerStr = "Bath";
        int bathLayer = LayerMask.NameToLayer(bathLayerStr);


        if (hit.collider.gameObject.layer == bathLayer) {

            Bath bath = hit.collider.gameObject.GetComponent<Bath>();

            if (bath != null && Input.GetKeyDown(KeyCode.Space)) {
                switch (bath.bathState) {
                    case BathState.TurnedOff:
                        bath.FillBathWithWater();
                        break;
                    case BathState.DirtyWater:
                        bath.CleanBath();
                        break;
                    default:
                        Debug.Log("Bath has no valid state");
                        break;
                }
            }
        }
    }

    void handleRaycastOnReceptionDeskLayer(RaycastHit2D hit) {

        string receptionDeskLayerStr = "ReceptionDesk";
        int receptionDeskLayer = LayerMask.NameToLayer(receptionDeskLayerStr);


        if (hit.collider.gameObject.layer == receptionDeskLayer) {

            CustomerManager customerManager = FindObjectOfType<CustomerManager>();
            Bath readyBath = Bath.GetFirstReadyBath();


            if (customerManager != null && customerManager.GetSizeOfCustomersQueue() != 0 && readyBath!=null && Input.GetKeyDown(KeyCode.Space)) {
                
                //take the first customer and change his state to InvitedIn => that is GoingToBath state
                customerManager.GetFirstCustomer().changeCustomerState(CustomerState.GoingToBath);
                customerManager.RemoveCustomerFromQueue(customerManager.GetFirstCustomer());  //do this only once

            }

        }
    }
}
