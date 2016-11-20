using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Billboard : MonoBehaviour
{

    public float offsety;
    private bool hadAValueOnce;
    public GameObject attachedEntity;
    private static Transform playerHealthBarTransform;

    void Start()
    {

        if (attachedEntity == null)
        {
            Debug.Log("Attached entity null");
            return;
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

        if (playerHealthBarTransform == null)
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("PlayerSelf");
            if (player.Length == 0)
            {
                return;
            }
            playerHealthBarTransform = player[0].GetComponent<Health>().healthBar.transform.parent.gameObject.transform.parent.gameObject.transform;
        } else
        {

        }

        Vector3 temp = attachedEntity.transform.position;
        //Apply y offset
        temp.y += offsety;
        transform.position = temp;
        transform.rotation = playerHealthBarTransform.rotation;
        //transform.LookAt(Camera.main.transform);

    }

}