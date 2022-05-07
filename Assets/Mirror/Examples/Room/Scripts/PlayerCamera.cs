using UnityEngine;
using UnityEngine.SceneManagement;

// This sets up the scene camera for the local player

namespace Mirror.Examples.NetworkRoom
{
    public class PlayerCamera : NetworkBehaviour
    {
        Camera mainCam;
        [SerializeField] private Transform cameraPoint;

        [SerializeField] Transform character;
        [SerializeField] Transform flashlight;
        public float sensitivity = 2;
        public float smoothing = 1.5f;

        Vector2 velocity;
        Vector2 frameVelocity;

        void Awake()
        {
            mainCam = Camera.main;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void OnStartLocalPlayer()
        {
            if (mainCam != null)
            {
                // configure and make camera a child of player with 3rd person offset
                mainCam.orthographic = false;
                mainCam.transform.SetParent(cameraPoint);
                mainCam.transform.localPosition = new Vector3(0f, 0, 0f);
                mainCam.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

                flashlight.transform.SetParent(mainCam.transform);
                flashlight.transform.localPosition = new Vector3(-0.620000005f, -0.769999981f, 0.0900000036f);
            }
        }

        public override void OnStopClient()
        {
            if (isLocalPlayer && mainCam != null)
            {
                mainCam.transform.SetParent(null);
                SceneManager.MoveGameObjectToScene(mainCam.gameObject, SceneManager.GetActiveScene());
                mainCam.orthographic = true;
                mainCam.transform.localPosition = new Vector3(0f, 0f, 0f);
                mainCam.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
            }
        }

        void Update()
        {
            if (isLocalPlayer == false)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else if (Cursor.lockState == CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
            

            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            // Rotate camera up-down and controller left-right from velocity.
            cameraPoint.transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
            mainCam.transform.localPosition = new Vector3(0f, 0, 0f);
            mainCam.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}
