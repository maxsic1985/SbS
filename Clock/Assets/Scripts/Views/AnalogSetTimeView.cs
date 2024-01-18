using System;
using System.Collections;
using System.Collections.Generic;
using MSuhinin.Clock;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public sealed class AnalogSetTimeView : BaseView, IDragHandler,IBeginDragHandler
{
   
    [FormerlySerializedAs("_sharedData")] [SerializeField]  private GameInputSharedData inputSharedData;
    private bool _direction;
    private Vector2 mousePosition = new Vector2();
    private Vector2 startPosition = new Vector2();
    private Vector2 differencePoint = new Vector2();
    private Vector2 _lastPosition = new Vector2();

 
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdateMousePosition();
            UpdateDifferencePoint();
        }
        if (Input.GetMouseButtonDown(0))
        {
            UpdateStartPosition();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.rotation = Quaternion.Euler(
            transform.rotation.x,
            transform.rotation.y, -differencePoint.x);
        
     inputSharedData.GetMouseDirection=(_lastPosition - eventData.position).normalized.x < 0 ? true : false;
 
    }
    

    public void OnBeginDrag(PointerEventData eventData)
    {  
        _lastPosition = eventData.position;
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
}