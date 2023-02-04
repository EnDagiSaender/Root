using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;
    public Vector3 LastPosition;
    private Collider2D _collider;
    private DragController _dragController;
    private float _movementTime = 15f;
    private System.Nullable<Vector3> _movementDestonation;
    private Color _fade = new Color(0.8f, 0.8f, 0.8f);
    private Color _full = new Color(1f, 1f, 1f);
    public SpriteRenderer Sr;
    public bool ActiveInCalculation = false;
    public delegate void UpdateCalculation(int position, int value);
    public static UpdateCalculation updateCalculation;
    void Start(){
        _collider = GetComponent<Collider2D>();
        _dragController = FindObjectOfType<DragController>();
    }
    void FixedUpdate() {
        if(_movementDestonation.HasValue){
            if (IsDragging){               
                _movementDestonation = null;
                return;
            }        
            if(transform.position == _movementDestonation){
                gameObject.layer = Layer.Default;
                _movementDestonation = null;
            }else{
                transform.position = Vector3.Lerp(transform.position, _movementDestonation.Value, _movementTime * Time.fixedDeltaTime);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other){

        Draggable colliderDraggable = other.GetComponent<Draggable>();
        if(colliderDraggable != null && _dragController.LastDragged.gameObject == gameObject){
            ColliderDistance2D colliderDistance2D = other.Distance(_collider);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;
            transform.position -= diff;
        }
        if (other.CompareTag("DropOperator")){
            Sr = other.GetComponent<SpriteRenderer>();
            if (gameObject.tag == "Operator" && Sr.color != _fade)
            {
                _movementDestonation = other.transform.position;
                updateCalculation(Sr.sortingOrder, gameObject.GetComponent<SpriteRenderer>().sortingOrder);
                Sr.color = _fade;
                ActiveInCalculation = true;
                //print(gameObject.tag);
            }
            else{
                 _movementDestonation = LastPosition;
            }
            //Debug.Log("Test");
        }else if(other.CompareTag("DropDice")){
            Sr = other.GetComponent<SpriteRenderer>();
            if (gameObject.tag == "Dice" && Sr.color != _fade)
            {
                _movementDestonation = other.transform.position;
                //print(gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
                updateCalculation(Sr.sortingOrder, int.Parse(gameObject.GetComponentInChildren<TextMeshProUGUI>().text));
                Sr.color = _fade;
                ActiveInCalculation = true;
            }
            else{
                 _movementDestonation = LastPosition;
            }
        }else if(other.CompareTag("DropInvalid")){
            _movementDestonation = LastPosition;
        }
    }
}
