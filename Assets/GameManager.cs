using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int dice1 = 0;
    [SerializeField] int dice2 = 0;
    [SerializeField] int dice3 = 0;
    [SerializeField] int operand1 = 0; // 1=+, 2=-, 3=*, 4=/
    [SerializeField] int operand2 = 0;
    [SerializeField] float totalScore = 0;
    [SerializeField] float totalPoints = 0;
    private Color _green = new Color(0f, 1f, 0f);
    [SerializeField] TextMeshProUGUI totalScoreText;

    [SerializeField] TextMeshProUGUI number_2;
    [SerializeField] TextMeshProUGUI number_3;
    [SerializeField] TextMeshProUGUI number_4;
    [SerializeField] TextMeshProUGUI number_5;
    [SerializeField] TextMeshProUGUI number_6;
    [SerializeField] TextMeshProUGUI number_7;
    [SerializeField] TextMeshProUGUI number_8;
    [SerializeField] TextMeshProUGUI number_9;
    [SerializeField] TextMeshProUGUI number_10;
    [SerializeField] TextMeshProUGUI number_11;
    [SerializeField] TextMeshProUGUI number_12;
    [SerializeField] TextMeshProUGUI number_13;
    // Start is called before the first frame update

    private void OnEnable()
    {
        DragController.updateCalculation += UpdateCalculation;
        Draggable.updateCalculation += UpdateCalculation;
        //Roll.updateCalculation += UpdateCalculation;
    }
    private void OnDisable()
    {
        DragController.updateCalculation -= UpdateCalculation;
        Draggable.updateCalculation -= UpdateCalculation;
        //Roll.updateCalculation -= UpdateCalculation;
    }
    private void FindNewSqare(int score)
    {
        switch (score)
        {
            case 4:
                if (number_2.color != _green)
                {
                    number_2.color = _green;
                    totalPoints += 2;
                }
                break;
            case 9:
                if (number_3.color != _green)
                {
                    number_3.color = _green;
                    totalPoints += 3;
                }
                break;
            case 16:
                if (number_4.color != _green)
                {
                    number_4.color = _green;
                    totalPoints += 4;
                }
                break;
            case 25:
                if (number_5.color != _green)
                {
                    number_5.color = _green;
                    totalPoints += 5;
                }
                break;
            case 36:
                if (number_6.color != _green)
                {
                    number_6.color = _green;
                    totalPoints += 6;
                }
                break;
            case 48:
                if (number_7.color != _green)
                {
                    number_7.color = _green;
                    totalPoints += 7;
                }
                break;
            case 64:
                if (number_8.color != _green)
                {
                    number_8.color = _green;
                    totalPoints += 8;
                }
                break;
            case 81:
                if (number_9.color != _green)
                {
                    number_9.color = _green;
                    totalPoints += 9;
                }
                break;
            case 100:
                if (number_10.color != _green)
                {
                    number_10.color = _green;
                    totalPoints += 10;
                }
                break;
            case 121:
                if (number_11.color != _green)
                {
                    number_11.color = _green;
                    totalPoints += 11;
                }
                break;
            case 144:
                if (number_12.color != _green)
                {
                    number_12.color = _green;
                    totalPoints += 12;
                }
                break;
            case 169:
                if (number_13.color != _green)
                {
                    number_13.color = _green;
                    totalPoints += 13;
                }
                break;
            default:
                break;
        }
    }
    private void UpdateCalculation(int position, int value)
    {
        totalScore = 0;
        switch (position)
        {
            case 1:
                dice1 = value;
                if(operand1 == 0 && operand2 == 0)
                {
                    totalScore = dice1;
                }
                break;
            case 2:
                operand1 = value;
                break;
            case 3:
                dice2 = value;
                if (operand1 == 0 && operand2 == 0)
                {
                    totalScore = dice2;
                }
                break;
            case 4:
                operand2 = value;
                break;
            case 5:
                dice3 = value;
                if (operand1 == 0 && operand2 == 0)
                {
                    totalScore = dice3;
                }
                break;
            default:
                break;
        }
        if (operand1 > operand2)
        {
            CalcFirstOperand(true);
            CalcSecondtOperand(false);
        }
        else
        {
            CalcSecondtOperand(true);
            CalcFirstOperand(false);
        }
        //print(totalScore);
        totalScoreText.text = totalScore.ToString("#.0");
        if(totalScore == Mathf.Floor(totalScore))
        {
            FindNewSqare((int)Mathf.Floor(totalScore));
            //print("true");
        }

    }
    private void CalcFirstOperand(bool firstCalc)
    {
        if (dice1 == 0 || dice2 == 0 || operand1 == 0) {
            return;
        }
        if (firstCalc)
        {
            switch (operand1)
            {
                case 1:
                    totalScore = dice1 + dice2;
                    return;
                case 2:
                    totalScore = dice1 - dice2;
                    return;
                case 3:
                    totalScore = dice1 * dice2;
                    return;
                case 4:
                    totalScore = (float)dice1 / (float)dice2;
                    return;
                default:
                    break;
            }
        }
        else
        {
            switch (operand1)
            {
                case 1:
                    totalScore = dice1 + totalScore;
                    return;
                case 2:
                    totalScore = dice1 - totalScore;
                    return;
                case 3:
                    totalScore = dice1 * totalScore;
                    return;
                case 4:
                    totalScore = (float)dice1 / (float)totalScore;
                    return;
                default:
                    break;
            }
        }
        return;
    }
    private void CalcSecondtOperand(bool firstCalc)
    {
        if (dice3 == 0 || dice2 == 0 || operand2 == 0)
        {
            return;
        }
        if (firstCalc)
        {
            switch (operand2)
            {
                case 1:
                    totalScore = dice2 + dice3;
                    return;
                case 2:
                    totalScore = dice2 - dice3;
                    return;
                case 3:
                    totalScore = dice2 * dice3;
                    return;
                case 4:
                    totalScore = dice2 / dice3;
                    return;
                default:
                    break;
            }
        }
        else
        {
            switch (operand2)
            {
                case 1:
                    totalScore =  totalScore + dice3;
                    return;
                case 2:
                    totalScore = totalScore - dice3;
                    return;
                case 3:
                    totalScore = totalScore * dice3;
                    return;
                case 4:
                    totalScore = totalScore / dice3;
                    return;
                default:
                    break;
            }
        }
        return;
    }

}
