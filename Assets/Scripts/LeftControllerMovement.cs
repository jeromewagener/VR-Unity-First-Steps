using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LeftControllerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 3f;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private Vector3 _velocity;
    private bool _isGrounded;

    void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        
        UnityEngine.Vector2 leftAxisValue = new Vector2(0,0);
        bool xPressed = false;
        
        foreach (var device in inputDevices)
        {
            if (device.name.Equals("Oculus Touch Controller - Left"))
            {
                device.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftAxisValue);
                // Debug.Log("X: " + leftAxisValue.x);
                // Debug.Log("Y: " + leftAxisValue.y);
            }

            if (device.name.Equals("Oculus Touch Controller - Right"))
            {
                device.TryGetFeatureValue(CommonUsages.primaryButton, out xPressed);
            }
        }
        
        Vector3 move = transform.right * leftAxisValue.x + transform.forward * leftAxisValue.y;
        controller.Move(move * speed * Time.deltaTime);

        // Debug.Log("IsXPressed: " + xPressed);
        // Debug.Log("IsGrounded: " + isGrounded);
        
        if (xPressed && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime * Time.deltaTime);
    }
}
