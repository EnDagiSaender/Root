using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public Draggable LastDragged => _lastDragged;
    private bool _isDragActive = false;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;
    private Color _fade = new Color(0.8f, 0.8f, 0.8f);
    private Color _full = new Color(1f, 1f, 1f);
    public AudioSource source;
    public AudioClip diceClip;

    public delegate void UpdateCalculation(int position, int value);
    public static UpdateCalculation updateCalculation;

    void Awake() {
        DragController[] controllers = FindObjectsOfType<DragController>();
        if (controllers.Length > 1){

            Destroy(gameObject);
            
        }
    
    }
    private void OnEnable()
    {
        button.newRoll += DiceRollSound;
    }
    private void OnDisable()
    {
        button.newRoll -= DiceRollSound;
    }
    public void DiceRollSound()
    {
        source.PlayOneShot(diceClip);
    }
    void Update()
    {
        if(_isDragActive && (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))){
            Drop();
            return;
        }
        if(Input.GetMouseButton(0)){
            Vector3 mousePos = Input.mousePosition;
            _screenPosition= new Vector2(mousePos.x, mousePos.y);
        }else if(Input.touchCount > 0){
            _screenPosition = Input.GetTouch(0).position;
        }else{
            return;
        }
        _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);
        if(_isDragActive){
            Drag();
        }else{
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            if(hit.collider != null){
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if(draggable != null){
                    _lastDragged = draggable;
                    InitDrag();
                    //print(_lastDragged.ActiveInCalculation);
                    if (_lastDragged.ActiveInCalculation)
                    {
                        _lastDragged.ActiveInCalculation = false;
                        _lastDragged.Sr.color = _full;
                        
                        //print(_lastDragged.Sr.sortingOrder);
                        if (_lastDragged.Sr.sortingOrder == 6)
                        {
                            _lastDragged.NoReroll = false;
                            Color tmp = _lastDragged.GetComponent<SpriteRenderer>().color;
                            tmp.a = 1;
                            _lastDragged.GetComponent<SpriteRenderer>().color = tmp;
                        }
                        else
                        {
                            updateCalculation(_lastDragged.Sr.sortingOrder, 0);
                        }
                    }
                }
            }
        }
        
    }
    
    void InitDrag(){
        _lastDragged.LastPosition = _lastDragged.transform.position;
        UpdateDragStatus(true);
    }
    void Drag(){
        _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);       
    }
    void Drop(){
        UpdateDragStatus(false);      
    }
    void UpdateDragStatus(bool isDragging){
        _isDragActive = _lastDragged.IsDragging = isDragging;
        _lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default;
    }

}
