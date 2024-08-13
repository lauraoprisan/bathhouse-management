using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CustomerState { 
    Waiting,
    GoingToBath,
    InsideBath,
    DoneBath
}
public class Customer : MonoBehaviour
{
    [SerializeField] public CustomerState currentState;
    public float moveSpeed = 7f;
    private CustomerManager customerManager;
    private Logic logic;
    public float WaitingInQueueTimeLimit = 5f;
    public float InsideBathTime = 7f;
    public float timer = 0;
    private Vector2 leavingPosition = new Vector2(8, -4);
    private Bath bathUsedByCustomer;
    private bool isInTransit = false;

    private SpriteRenderer spriteRenderer;

    private void Start() {
        currentState = CustomerState.Waiting;
        customerManager = FindObjectOfType<CustomerManager>();
        logic = FindObjectOfType<Logic>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }


    private void Update() {
        if (!isInTransit) { 
            timer += Time.deltaTime;
        }

        if (logic.isGameOver) { 
            Destroy(gameObject);
        }


        //destroy customer if he waited too much in line, remove from queue
        if (currentState == CustomerState.Waiting && timer > WaitingInQueueTimeLimit) {
            customerManager.RemoveCustomerFromQueue(this); //makes sens
            logic.countAngryCustomers++;
            Destroy(gameObject);
        }

        if ((currentState == CustomerState.InsideBath || currentState == CustomerState.DoneBath) && timer > InsideBathTime) {

            if (spriteRenderer != null) {
                spriteRenderer.flipX = false;  
            }

            currentState = CustomerState.DoneBath;

            Vector2 targetPosition = leavingPosition;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            bathUsedByCustomer.SetBathToDirty();

            if ((Vector2)transform.position == targetPosition) {
                logic.countHappyCustomers++;
                Destroy(gameObject);
            }
        }


        if (currentState == CustomerState.GoingToBath){
            Bath readyBath = Bath.GetFirstReadyBath();

            if (readyBath != null) {
                isInTransit = true;
                bathUsedByCustomer = readyBath;
                Vector2 targetPosition = readyBath.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                //customerManager.RemoveCustomerFromQueue(this);  //do this only once

                if ((Vector2)transform.position == targetPosition) {
                    Debug.Log("reached bath. inside now");
                    currentState = CustomerState.InsideBath;
                    isInTransit = false;
                    timer = 0;
                    readyBath.SetBathToOccupied();
                }
            } else {
                Debug.Log("No ready baths available.");
            }
        }
    }

    public void changeCustomerState(CustomerState state) {
        currentState = state;
    }
}
