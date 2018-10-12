using UnityEngine;
using System.Collections;

public class CameraFollowMaybeBad : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public Vector3 offsetPosition;
    public bool CameraLock;
    Vector3 cameraVelocity = Vector3.zero;
    Vector3 targetPosition;
    float AdjustSpeed;

    private static CameraFollowMaybeBad instance;
    public static CameraFollowMaybeBad Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CameraFollowMaybeBad>();
            }
            return instance;
        }
    }

    private void Start()
    {
        CameraLock = false;
    }

    private void Update()
    {
        AdjustSpeed = 8f * Time.deltaTime;
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }
        // compute position
        if (CameraLock == false)
        {
            targetPosition = new Vector3(target.position.x + offsetPosition.x, offsetPosition.y, offsetPosition.z);
            if (transform.position.x < targetPosition.x)
            {
                transform.position = new Vector3(transform.position.x + AdjustSpeed, transform.position.y, transform.position.z);
            }
            if (transform.position.x > targetPosition.x)
            {
                transform.position = new Vector3(transform.position.x - AdjustSpeed, transform.position.y, transform.position.z);
            }
            if (transform.position.y < targetPosition.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + AdjustSpeed, transform.position.z);
            }
            if (transform.position.y > targetPosition.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - AdjustSpeed, transform.position.z);
            }
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref cameraVelocity, 0.15f);
            if (transform.position.x - targetPosition.x > -0.2f && transform.position.x - targetPosition.x < 0.2f)
            {
                transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
            }
            if (transform.position.y - targetPosition.y > -0.2f && transform.position.y - targetPosition.y < 0.2f)
            {
                transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
            }
            if (transform.position.z - targetPosition.z > -0.2f && transform.position.z - targetPosition.z < 0.2f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, targetPosition.z);
            }
            //Debug.Log(transform.position.x - targetPosition.x);
            if (transform.position == targetPosition)
            {
                CameraLock = true;
            }
        }
        if (CameraLock)
        {
            transform.position = new Vector3(target.position.x + offsetPosition.x, offsetPosition.y, offsetPosition.z);
            //transform.position = target.position;
        }
    }
}