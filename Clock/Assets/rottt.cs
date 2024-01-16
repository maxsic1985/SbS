using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class rottt : MonoBehaviour,IDragHandler
{
    public float turnSpeed = 45; // degrees per second
  

    private Vector2 mousePosition = new Vector2();
    private Vector2 startPosition = new Vector2();
    private Vector2 differencePoint = new Vector2();
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            UpdateMousePosition();
            UpdateDifferencePoint();
        }
        if(Input.GetMouseButtonDown(0))
        {
            UpdateStartPosition();
          
        }
    }
    
    
    private void UpdateMousePosition()
    {
        mousePosition.x = Input.mousePosition.x;
        mousePosition.y = Input.mousePosition.y;
    }
    
    private void UpdateStartPosition()
    {
        startPosition.x = transform.position.x;
        startPosition.y = transform.position.y;
    }
    
    private void UpdateDifferencePoint()
    {
        differencePoint = mousePosition - startPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.rotation =Quaternion.Euler(transform.rotation.x,transform.rotation.y, -differencePoint.x) ;
    }
}
