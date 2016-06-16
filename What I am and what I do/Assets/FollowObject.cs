using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

    public GameObject ObjectToFollow;
    public float YOffset;
    public float XOffset;
    public float ZOffset;

    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(XOffset, YOffset, ZOffset);
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = (ObjectToFollow.transform.position + offset);
	
	}
}
