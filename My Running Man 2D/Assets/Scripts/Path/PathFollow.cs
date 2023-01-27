using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float minDistanceToPoint = 0.01f;


    public List<Vector3> points = new List<Vector3>();

    private bool _playing; //kiem tra truoc khi vao che do Play
    private bool _moved;
    private int _currentPoint = 0;
    private Vector3 _currentPosition;

    private void Start()
    {
        _playing = true;

        _currentPosition = transform.position;
        transform.position = _currentPosition + points[0];
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        //Set first position
        if (_moved)
        {
            transform.position = _currentPosition + points[0]; //di chuyen  obejct den point dau tien
            _currentPoint++;
            _moved = true;
        }

        //Move to next point
        transform.position = Vector3.MoveTowards(transform.position, _currentPosition + points[_currentPoint], Time.deltaTime * moveSpeed);

        //Evaluate move to next point 
        float distanceToNextPoint = Vector3.Distance(_currentPosition + points[_currentPoint], transform.position);
        if (distanceToNextPoint < minDistanceToPoint)
        {
            _currentPoint++;
        }

        if (_currentPoint == points.Count)//If we are on the last point, reset our position to the first one
        {
            _currentPoint = 0;
        }
    }

    private void OnDrawGizmos()
    {
        if (transform.hasChanged && !_playing)
        {
            _currentPosition = transform.position;
        }
        if (points != null)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (i < points.Count)
                {
                    //Draw point
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(_currentPosition + points[i], 0.4f);

                    //Draw line giua cac point da setup
                    Gizmos.color = Color.black;

                    if (i < points.Count - 1) //first point to last-second point
                    {
                        Gizmos.DrawLine(_currentPosition + points[i], _currentPosition + points[i + 1]);
                    }
                    if (i == points.Count - 1) //last point to first point
                    {
                        Gizmos.DrawLine(_currentPosition + points[i], _currentPosition + points[0]);
                    }
                }
            }
        }
    }
}
