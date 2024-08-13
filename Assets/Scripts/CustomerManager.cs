using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class CustomerManager : MonoBehaviour
{
    private List<Customer> customers = new List<Customer>();
    private float spaceBetweenCustomers = 2f;
    private CustomerSpawner customerSpawner;

 

    private void Start() {
         customerSpawner = FindObjectOfType<CustomerSpawner>();
    }


    public void AddCustomerToQueue(Customer customer) { 
        customers.Add(customer);
    }

    public void RemoveCustomerFromQueue(Customer customer) {
        if (customers.Contains(customer)) {
            customers.Remove(customer);
            MoveCustomersByOneSpace();
            customerSpawner.prevYSpawnLocation -= 2;


        } else {
            Debug.Log("customers queue does not contain this customer: " + customer);
        }
    }

    public int GetSizeOfCustomersQueue() { 
        return customers.Count;
    }

    public Customer GetFirstCustomer() {
        return customers[0];
    }

    public void MoveCustomersByOneSpace() {
        if (customers.Count != 0) {
            for (int i = 0; i < customers.Count; i++) {
                Customer customer = customers[i];

                Vector2 targetPosition = new Vector2(customer.transform.position.x, customer.transform.position.y - spaceBetweenCustomers);

                customer.transform.position = targetPosition;
            }
        }
    }
}
