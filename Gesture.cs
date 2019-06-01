using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gesture : MonoBehaviour
{
    // Vectors for first touch
    public Vector2 startPosition;
    public Vector2 endPosition;

    // Vectors for second touch
    public Vector2 startPosition2;
    public Vector2 endPosition2;

    // Touch structures for the class
    private Touch firstTouch; // For tapping and swiping
    private Touch secondTouch; // For zoom, used in conjunction with first touch

    private bool screenContact; // Screen being touched
    private bool swiping; // Movement 
    private bool zooming; // Two touch movement

    // Scaling zoom pinches on scale of zero to one
    private const float ZSCALEMIN = 0;
    private const float ZSCALEMAX = 1;

    // Times for shooting rays
    System.DateTime currentTimeShot;
 
   

    
    void Start()
    {
        // Instantiate for first touch
        startPosition = new Vector2();
        endPosition = new Vector2();

        // Instantiate for second touch
        startPosition2 = new Vector2();
        endPosition2 = new Vector2();

        // Initialize contact states
        screenContact = false;
        swiping = false;
        zooming = false;

      

    }
    
    // Update is called once per frame
    void Update()
    {
        // There is at least one finger on the screen
        if (Input.touchCount == 1)
        {
            firstTouch = Input.GetTouch(0);
            screenContact = true;
            zooming = false;
            // Getting starting touch position
            if (firstTouch.phase == TouchPhase.Began)
            {
                startPosition = firstTouch.position;
            }
            else if (firstTouch.phase == TouchPhase.Ended)
            {
                endPosition = firstTouch.position;
                zooming = false;
                screenContact = false;
                swiping = false;
            }
            else if (firstTouch.phase == TouchPhase.Moved)
            {
                swiping = true;
            }
        } else if (Input.touchCount == 2)
        {
            firstTouch = Input.GetTouch(0);
            secondTouch = Input.GetTouch(1);
            zooming = true;
            swiping = false;

            // On zoom pinch
            if (firstTouch.phase == TouchPhase.Moved || secondTouch.phase == TouchPhase.Moved)
            {
                // Code goes in here based off of points
                // Optional code to control two finger inputs

            }

        }
        
    }

    public bool touchingScreen()
    {
        return screenContact;
    }

    public bool zoom()
    {
        return zooming;
    }

    public bool swipe()
    {
        return swiping;
    }

    public Vector2Int getScreenSize()
    {
        return new Vector2Int(Screen.width, Screen.height);
    }

    private float normalize(float value, float rangeA, float rangeB, float normRangeA, float normRangeB)
    {
        return normRangeA + ((value - rangeA) * (normRangeB - normRangeA)) / (rangeB - rangeA);
    }
    
    public void shootRay(Vector3 origin, Vector3 direction)
    {
        Ray ray = new Ray(origin, direction);
        Physics.Raycast(ray);


        currentTimeShot = new System.DateTime();
    }

    public long getTimeSinceLastRay()
    {
        System.DateTime nowTimeShot = new System.DateTime();
        return nowTimeShot.Ticks - currentTimeShot.Ticks;
    }

}
