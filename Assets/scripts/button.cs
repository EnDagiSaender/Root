using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public delegate void NewRoll();
    public static NewRoll newRoll;
    public void ButtonClicked()
    {
        newRoll();
    }



}
