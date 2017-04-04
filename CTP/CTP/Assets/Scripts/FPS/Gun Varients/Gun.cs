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
    private LocalPlayer m_playerScript;
    private int m_playerId;
    private string m_playerTeam;

    // Use this for initialization
    void Start () {
        reloading = false;
        playerCamera = (GameObject)transform.parent.parent.gameObject;
        m_playerScript = playerCamera.transform.parent.gameObject.GetComponent<LocalCameraMovement>().m_playerScript;
        currentAmountOfMags = amountOfMagazines;
        playerHud = playerCamera.transform.GetChild(2).gameObject;
        playerHud.GetComponent<LocalPlayerHUD>().SetGun(gameObject);
        //playerHud.GetComponent<PlayerHUD>().SetGun(gameObject);
        //playerCamera = playerCameraTransform.gameObject;
        m_playerId = m_playerScript.m_playerID;
        m_playerTeam = m_playerScript.m_team;
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
            if (!CheckForFullMagazine() && Input.GetButtonDown("Reload"+m_playerId))
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
            if ((Input.GetAxis("Fire" + m_playerId) == 1) || (Input.GetButtonDown("Fire" + m_playerId)) && Time.time > nextFire)
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
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            print(hit.collider.gameObject.tag);
            if (hit.collider.tag == "Player")
            {
                print("hit something");

                if (hit.collider.gameObject.GetComponent<LocalPlayer>().m_team != m_playerTeam)
                {
                    hit.collider.gameObject.GetComponent<LocalPlayer>().TakeDamage(10);
                }
            }
        }
        Debug.DrawLine(ray.origin, hit.point, Color.black, 99f);
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
