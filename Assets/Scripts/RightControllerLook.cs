using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RightControllerLook : MonoBehaviour
{
    public float joyStickSensitivity = 100f;
    private float _xRotation = 0f;

    public Transform player;

    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        
        foreach (var device in inputDevices)
        {
            if (device.name.Equals("Oculus Touch Controller - Right"))
            {
                device.TryGetFeatureValue(CommonUsages.primary2DAxis, out var twoDimensionalAxisValue);

                float joyStickX = twoDimensionalAxisValue.x * joyStickSensitivity * Time.deltaTime;
                
                // float joyStickY = twoDimensionalAxisValue.y * joyStickSensitivity * Time.deltaTime;
                // Debug.Log("X " + twoDimensionalAxisValue.x + " - Y " + twoDimensionalAxisValue.y);
                
                player.Rotate(Vector3.up * joyStickX);
            }
        }
    }
}
