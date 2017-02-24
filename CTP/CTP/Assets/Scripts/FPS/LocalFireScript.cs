using UnityEngine;
using System.Collections;

public class LocalFireScript : MonoBehaviour {

    public GameObject fireParticleEffect;
    public GunSelect gunSelect;
    private GameObject currentGun;
    private Transform barrelExitPoint;
    private GameObject playerCamera;

    // Use this for initialization
    void Start()
    {
        playerCamera = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //get required data
            GetGunData();
            //fire particle
            ParticleEffect();
            //fire raycast
            FireRaycast();
        }
    }

    void GetGunData()
    {
        currentGun = gunSelect.GetCurrrentGun();
        barrelExitPoint = currentGun.transform.GetChild(0).GetChild(0).GetChild(0).transform;
    }

    void ParticleEffect()
    {
        GameObject particleEffect = (GameObject)Instantiate(fireParticleEffect, barrelExitPoint.position, currentGun.transform.rotation);
        Destroy(particleEffect, 1f);
    }

    void FireRaycast()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Target")
            {
                print("hit target");
            }
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.green, 99f);
    }
}
