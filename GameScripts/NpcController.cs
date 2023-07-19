using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{

    public GameObject[] fakeFood;
    public Transform follower;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position= follower.position;
        transform.rotation= follower.rotation;
    }
}
