using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private List<Customer> customers = new List<Customer>();
    private float gameTimer = 0f;
    private float spawnInterval = 3f; //starting point for spawnInterval
    private float minSpawnTime = 3f;
    private float maxSpawnTime = 7f;
    private float currentYSpawnLocation;
    [SerializeField] private float fixedXPosition;
    [SerializeField] public float prevYSpawnLocation;
    private int currentCustomerIndex;
    private Logic logic;

    private void Start() {
        logic = FindObjectOfType<Logic>();
    }

    private void Update() {
        gameTimer += Time.deltaTime;

        if (gameTimer > spawnInterval && !logic.isGameOver) {
            SpawnCustomer();
            spawnInterval = Random.Range(minSpawnTime, maxSpawnTime);
            gameTimer = 0f;
        }
    }

    void SpawnCustomer() {

        if(customers.Count > 0) {
            currentCustomerIndex = Random.Range(0, customers.Count);
        }

        currentYSpawnLocation = prevYSpawnLocation + 2;

        Customer customer = Instantiate(customers[currentCustomerIndex], new Vector3(fixedXPosition, currentYSpawnLocation, 0), transform.rotation);
        CustomerManager customerManager = FindObjectOfType<CustomerManager>();
        customerManager.AddCustomerToQueue(customer);

        prevYSpawnLocation = currentYSpawnLocation;
    }



}
