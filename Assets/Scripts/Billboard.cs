using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Billboard : MonoBehaviour
{

    public float offsety;
    private bool hadAValueOnce;
    public GameObject attachedEntity;

    void Start()
    {

        if (attachedEntity == null)
        {
            Debug.Log("Attached entity null");
        }
        hadAValueOnce = true;
    }

    void Update()
    {
        if (attachedEntity == null)
        {
            if (hadAValueOnce) //this means the attachedEntity existed once, but is null now. So probably we can also
                //delete the health bar
            {
                Destroy(gameObject);
            }

            Debug.Log("Attached entity was null, healthbar destroyed. exiting script.");
            return;
        } else
        {
            hadAValueOnce = true;
        }

        Vector3 temp = attachedEntity.transform.position;
        //Apply y offset
        temp.y += offsety;
        transform.position = temp;
        transform.LookAt(Camera.main.transform);

        //TODO: straight health bars for enemies as well.
        //Quaternion angle = 
    }

    float tanAlpha()
    {
        Transform target = Camera.main.gameObject.GetComponent<CameraFollow>().getTarget();
        Vector3 a = Camera.main.transform.position - target.position;
        Vector3 b = target.position - attachedEntity.transform.position;
        float aLength = Vector3.Magnitude(a);
        float bLength = Vector3.Magnitude(b);
        return Mathf.Tan(aLength / bLength);
    }
}