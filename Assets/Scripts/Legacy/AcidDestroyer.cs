using UnityEngine;
using System.Collections;

public class AcidDestroyer : MonoBehaviour
{

    public PlayerMove player;

    Rigidbody2D acid;

    public float acidSpeed = 0.005f;

    // Use this for initialization
    void Start()
    {
        
        acid = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.winning == false)
        {
            acid.transform.position = new Vector3(acid.transform.position.x, (acid.transform.position.y - acidSpeed), acid.transform.position.z);
        }
    }
}

