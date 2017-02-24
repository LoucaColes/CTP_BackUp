using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour {

    public int amountOfMagazines, amountOfRoundsPerMag;
    public bool reloading;
    private Transform downSightsPos, normalPos;
    private int currentAmountOfAmmoInMag, currentAmountOfMags;
    public Transform playerCameraTransform;
    public GameObject playerCamera;
    public Transform gunSpawnPoint;
    [SerializeField] private float fireRate;
    private float nextFire;
    private GameObject playerHud;
    public GameObject shotParticle;
    public Transform particleSpawnPoint;
    [SerializeField]
    private float particleTime;

    // Use this for initialization
    void Start () {
        reloading = false;
        playerCamera = (GameObject)transform.parent.parent.gameObject;
        currentAmountOfMags = amountOfMagazines;
        playerHud = GameObject.FindGameObjectWithTag("HUD");
        playerHud.GetComponent<LocalPlayerHUD>().SetGun(gameObject);
        playerHud.GetComponent<PlayerHUD>().SetGun(gameObject);
        //playerCamera = playerCameraTransform.gameObject;

    }
	
	// Update is called once per frame
	void Update () {

        //Check for reload
        CheckReloading();

        //Check for firing
        CheckFiring();

        //CheckSightsChange();
	}

    void CheckReloading()
    {
        //check if magazine is empty
        if (CheckForEmptyMagazine())
        {
            Reload(); //if empty reload
        }
        else
        {
            if (!CheckForFullMagazine() && Input.GetButtonDown("Reload"))
            {
                Reload();
            }
        }
    }

    bool CheckForFullMagazine()
    {
        bool full = false;

        if (currentAmountOfAmmoInMag == amountOfRoundsPerMag)
        {
            full = true;
        }

        return full;
    }

    bool CheckForEmptyMagazine()
    {
        bool empty = false;

        //if ammo count is less than or equal 0 then mag is empty
        if (currentAmountOfAmmoInMag <= 0)
        {
            empty = true;
        }

        return empty;
    }

    void Reload()
    {
        //check if player has enough mags to reload
        if(!CheckMagCount())
        {
            return;
        }

        //set reloading to true so that player cant fire
        reloading = true;

        //set current ammo count to amount of ammo per mag
        currentAmountOfAmmoInMag = amountOfRoundsPerMag;

        //subtract one from amount of mags being held
        currentAmountOfMags--;

        //set reloading to false when done
        reloading = false;
    }

    bool CheckMagCount()
    {
        bool hasMagsLeft = true;

        if(currentAmountOfMags <= 0)
        {
            hasMagsLeft = false;
            //print("Has no mag left");
        }
        //print("hasMagLeft: " + hasMagsLeft);
        return hasMagsLeft;
    }

    void CheckFiring()
    {
        bool doesHaveMag = CheckMagCount();
        if (!reloading && doesHaveMag)
        {
            if ((Input.GetButton("Fire1") || Input.GetButtonDown("Fire1") )&& Time.time > nextFire)
            {
                FireRayCast();
                SpawnParticle();
                nextFire = Time.time + fireRate;
                currentAmountOfAmmoInMag--;
                //print("number of bullets: " + currentAmountOfAmmoInMag);
            }
        }
    }

    void SpawnParticle()
    {
        GameObject tempParticle = (GameObject)Instantiate(shotParticle, particleSpawnPoint.position, particleSpawnPoint.rotation);
        Destroy(tempParticle, particleTime);
    }

    void FireRayCast()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Target")
            {
                hit.collider.gameObject.GetComponent<Player>().TakeDamage(10);
            }
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.green, 99f);
    }

    public int GetMagCount()
    {
        return currentAmountOfMags;
    }

    public int GetAmmoCount()
    {
        return currentAmountOfAmmoInMag;
    }

    //void CheckSightsChange()
    //{
    //    if (Input.GetButtonDown("Fire2"))
    //    {
    //        SetDownSightsPos();
    //    }
    //    else if (Input.GetButtonUp("Fire2"))
    //    {
    //        SetNormalPos();
    //    }
    //}

    //public void SetDownSightsPos(Transform newPos)
    //{
    //    downSightsPos = newPos;
    //}

    //public void SetNormalPos(Transform newPos)
    //{
    //    normalPos = newPos;
    //}
}
