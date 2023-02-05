using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Roll : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite rollSprite;
    private int _animationNr = 0;

    // Start is called before the first frame update
    void Start()
    {
        RollDice();
    }

    private void OnEnable()
    {
        button.newRoll += RollDice;
    }
    private void OnDisable()
    {
        button.newRoll -= RollDice;
    }

    void RollDice()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        if(tmp.a == 0.8f)
        {
            return;
        }
        switch (_animationNr)
        {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().sprite = rollSprite;
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Random.Range(1, 20).ToString();
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().flipY = true;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Random.Range(1, 20).ToString();
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Random.Range(1, 20).ToString();
                break;
            case 4:
                gameObject.GetComponent<SpriteRenderer>().flipY = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
                gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Random.Range(1, 20).ToString();
                _animationNr = 0;
                return;

            default:
                break;

        }
        _animationNr++;
        Invoke("RollDice", Random.Range(0.15f, 0.25f));
    }
}
