using UnityEngine;
using System.Collections;

public class LocalCameraMovement : MonoBehaviour {

    public float mouseSensitivity = 5.0f;
    public float verticalLimit = 60.0f;
    private float verticalRotation = 0;
    private GameObject playerCamera;
    public LocalPlayer m_playerScript;
    private int m_playerId;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = gameObject.transform.GetChild(0).gameObject;
        m_playerId = m_playerScript.m_playerID;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        float rotLeftRight = Input.GetAxis("RightHorizontal" + m_playerId) * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("RightVertical" + m_playerId) * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLimit, verticalLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
