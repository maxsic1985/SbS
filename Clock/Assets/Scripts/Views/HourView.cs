using System;
using System.Collections;
using System.Collections.Generic;
using MSuhinin.Clock;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class HourView : BaseView, IDragHandler,IBeginDragHandler
{
    private Vector2 _drugEvent = new Vector2();
    public Vector2 DrugEvent=>sasa(ref _lastPosition);
    public Vector2 beginEvent;
    [SerializeField]  private GameSharedData _sharedData;
    public Vector2 BeginEvent => _lastPosition;
    public bool Direction=>  (_lastPosition - _drugEvent).normalized.x > 0 ? true : false;
    private bool _direction;

    private Vector2 sasa(ref Vector2 s)
    {
        return s;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.rotation = Quaternion.Euler(
            transform.rotation.x,
            transform.rotation.y, -differencePoint.x);
       
      //  _direction =true;
      //  BeginEvent = eventData.position;
   
     _sharedData.GetMouseDirection=(_lastPosition - eventData.position).normalized.x < 0 ? true : false;
        Vector2 direction = eventData.position - _lastPosition;
     //  Debug.Log($"directionmagnitude={direction.magnitude}");
        Debug.Log($"directionnormalized={direction.normalized}");
      //  Debug.Log($"directionsqrMagnitude={direction.sqrMagnitude}");
     //   Debug.Log($"directionsqrCOS={Math.Cos(direction.magnitude)}");
    }

    private Vector2 mousePosition = new Vector2();
    private Vector2 startPosition = new Vector2();
    private Vector2 differencePoint = new Vector2();

    private Vector2 _lastPosition= new Vector2();

    // Update is called once per frame
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

      
  
        //  Debug.Log("euler"+Mathf.RoundToInt(transform.rotation.eulerAngles.z));
        // Debug.Log(Vector2.Dot(transform.TransformDirection(Vector2.left),differencePoint));
        //    Debug.Log(differencePoint.x); 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {  
        _lastPosition = eventData.position;
        beginEvent = eventData.position;
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