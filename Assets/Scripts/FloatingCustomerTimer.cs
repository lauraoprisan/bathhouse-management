using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;


public class FloatingCustomerTimer : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    private Customer customer;

    private void Start() {
        customer = GetComponentInParent<Customer>();
        if (customer == null) {
            Debug.LogError("Customer component not found in parent.");
        }
    }
    public void UpdateTimerSlider(float currentValue, float maxTime) {
        if (slider != null) {
            if (customer.currentState == CustomerState.Waiting) {
                slider.value = (maxTime-currentValue) / maxTime;

                //change color based on the percentage left
                if (slider.value < 0.2f) {
                    SetSliderColor(Color.red);
                } else if (slider.value < 0.5f) {
                    SetSliderColor(Color.yellow);
                } else {
                    SetSliderColor(Color.green); //default color for waiting
                }

            } else if (customer.currentState == CustomerState.InsideBath) {
                slider.value = currentValue / maxTime;
                SetSliderColor(Color.cyan);
            }
        } else {
            Debug.LogError("Slider component is missing.");
        }
    }



    private void Update() {
        if (customer == null) {
            Debug.LogWarning("Customer reference is null.");
            return;
        }


        if (customer.currentState == CustomerState.Waiting) {
            UpdateTimerSlider(customer.timer, customer.WaitingInQueueTimeLimit);
        } else if (customer.currentState == CustomerState.InsideBath) {
            slider.gameObject.SetActive(true);
            UpdateTimerSlider(customer.timer, customer.InsideBathTime);
        } else {
            slider.gameObject.SetActive(false);
        }


    }

    private void SetSliderColor(Color color) {
     
        if (fill != null) {
            fill.color = color;
        } else {
            Debug.LogError("Fill object is missing in the slider.");
        }
    }
}
