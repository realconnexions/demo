using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

	SteamVR_Controller.Device device;
	SteamVR_TrackedObject trackedObj;

	// Use this for initialization
	void Start ()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		device = SteamVR_Controller.Input ((int)trackedObj.index);
	}

	void OnTriggerStay (Collider other)
	{
		Debug.Log ("Entered a collider");
		Debug.Log (other.gameObject.tag + "    " + other.gameObject.name);
		if (other.gameObject.CompareTag ("Pickable"))
		{
			Debug.Log ("Picking Obj");
			if (device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger))
			{
				DropIt (other);
			}
			else if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger))
			{
				GrabIt (other);
			}

		}
	}

	void GrabIt (Collider grab)
	{
		grab.transform.SetParent (gameObject.transform);
		grab.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		device.TriggerHapticPulse (2000);
	}

	void DropIt (Collider grab)
	{
		grab.transform.SetParent (null);
		Rigidbody rb = grab.GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		rb.velocity = device.velocity;
		rb.angularVelocity = device.angularVelocity;
	}
}
