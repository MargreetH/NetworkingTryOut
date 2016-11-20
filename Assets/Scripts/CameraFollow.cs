using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraFollow : NetworkBehaviour
{
    Vector3 initialCameraPosition;
    Quaternion intialCameraRotation;
    private bool reconnect = false;

    private Transform target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.
    private GameObject currentPlayer;


    Vector3 offset;                     // The initial offset from the target.

    void Start()
    {
        initialCameraPosition = new Vector3 (0f, 15f, -22f);
        Vector3 eulerRotation = new Vector3(30f, 0f, 0f);
        intialCameraRotation = Quaternion.Euler(eulerRotation);

        bool succes = setCurrentPlayerSuccess();
        if (!succes)
        {
            return;
        }

        Debug.Log("Kom ik hier ooit");

        target = currentPlayer.transform;

        // Calculate the initial offset.
        //offset = transform.position - target.position;
    }

    void FixedUpdate()
    {

        if (currentPlayer == null)
        {
            if (!setCurrentPlayerSuccess())
            {
                return;
            } else
            {
                resetCameraPosition();
            }

            target = currentPlayer.transform;

            // Calculate the initial offset.
            offset = transform.position;
        } else
        {
            target = currentPlayer.transform;
        }

        if (target == null)
        {
            Debug.Log("Target null");
            return;
        }

        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + offset;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    private bool setCurrentPlayerSuccess()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");

        if (currentPlayer == null)
        {
            return false;
        }
        return true;
    }

    public void resetCameraPosition()
    {
        transform.position = initialCameraPosition;
        transform.rotation = intialCameraRotation;
        offset = transform.position;
    }

    public Transform getTarget()
    {
        return target;
    }
}