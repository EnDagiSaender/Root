using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] int dice1 = 0;
    [SerializeField] int dice2 = 0;
    [SerializeField] int dice3 = 0;
    [SerializeField] int operand1 = 0; // 1=+, 2=-, 3=*, 4=/
    [SerializeField] int operand2 = 0;
    // Start is called before the first frame update

    private void OnEnable()
    {
        DragController.updateCalculation += UpdateCalculation;
        Draggable.updateCalculation += UpdateCalculation;
    }
    private void OnDisable()
    {
        DragController.updateCalculation -= UpdateCalculation;
        Draggable.updateCalculation -= UpdateCalculation;
    }
    private void UpdateCalculation(int position, int value)
    {
        switch (position)
        {
            case 1:
                dice1 = value;
                break;
            case 2:
                operand1 = value;
                break;
            case 3:
                dice2 = value;
                break;
            case 4:
                operand2 = value;
                break;
            case 5:
                dice3 = value;
                break;
            default:
                break;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
