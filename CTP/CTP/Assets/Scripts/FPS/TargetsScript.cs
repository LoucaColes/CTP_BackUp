using UnityEngine;
using System.Collections;

public class TargetsScript : MonoBehaviour {

    private Renderer targetRenderer;
    private Material targetMaterial;
    private bool hit;
    private float stopwatch, timeToWait;

	// Use this for initialization
	void Start () {
        targetRenderer = gameObject.GetComponent<Renderer>();
        targetMaterial = targetRenderer.material;
        targetMaterial.color = Color.red;
        timeToWait = 3.0f;
        hit = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (hit)
        {
            stopwatch += Time.deltaTime;
            if (stopwatch >= timeToWait)
            {
                ResetTarget();
            }
        }
    }

    public void TargetHit()
    {
        targetMaterial.color = Color.white;
        hit = true;
    }

    void ResetTarget()
    {
        targetMaterial.color = Color.red;
        hit = false;
        ResetStopWatch();
    }

    void ResetStopWatch()
    {
        stopwatch = 0.0f;
    }
}
