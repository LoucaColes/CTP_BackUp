using UnityEngine;
using System.Collections;

public class ArchVizTriggerCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Player")
        {
            // Debug.Log("Object Entered the trigger");
            // Debug.Log("Detected collision between " + gameObject.name + " and " + other.gameObject.name);

            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer myRenderer in renderers)
            {
                myRenderer.enabled = false;
            }

            // Disable the MeshCollider
            MeshCollider[] meshColliders = gameObject.GetComponentsInChildren<MeshCollider>();
            foreach (MeshCollider myCollider in meshColliders)
            {
                myCollider.enabled = false;
            }
        }
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") {

			// Debug.Log ("Object Exited the trigger");
			// Debug.Log("Detected collision between " + gameObject.name + " and " + other.gameObject.name);

            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer myRenderer in renderers)
            {
                myRenderer.enabled = true;
            }

            // Disable the MeshCollider
            MeshCollider[] meshColliders = gameObject.GetComponentsInChildren<MeshCollider>();
            foreach (MeshCollider myCollider in meshColliders)
            {
                myCollider.enabled = true;
            }

			// gameObject.GetComponent<Renderer>().enabled = true;

		}
		// Debug.Log(string.Format("Speed:{0}, Height:{1}, Length:{2}, Number of Bullets: {3}, Name: {4}",fSpeed,fHeight,fLength,iNumberOfBullets,sName));  
	}


}
