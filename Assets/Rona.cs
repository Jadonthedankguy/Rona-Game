using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Rona : MonoBehaviour
{ 
    Rigidbody rigidBody;
    public Camera eyes;
    public Text txt;
    public int amountFuel = 5000;
    public float sensitivity;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        rigidBody = GetComponent<Rigidbody>();
        foreach(Camera c in Camera.allCameras)
        {
            if(c.gameObject.name == "Eyes")
            {
                eyes = c;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessUI();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space) && amountFuel > 0)
        {
            rigidBody.AddRelativeForce(Vector3.up);
            amountFuel--;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddRelativeForce(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddRelativeForce(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rigidBody.AddRelativeForce(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidBody.AddRelativeForce(Vector3.back);
        }
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        if (rotateHorizontal != 0)
        {
            transform.Rotate(new Vector3(0, rotateHorizontal, 0));
        }
        else if (rotateVertical != 0)
        {
            eyes.transform.Rotate(new Vector3(-rotateVertical, 0, 0));
        }
    }
    private void ProcessUI()
    {
        txt.text = "Fuel: " + amountFuel.ToString();
    }
    void OnCollisonEnter(Collision col)
    {
        StartCoroutine(LoadYourAsyncScene());
        IEnumerator LoadYourAsyncScene()
        {
            if (col.collider.name == "Finish Platform")
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level 2");
                while (!asyncLoad.isDone)
                {
                    yield return null;
                }
            }
        }

    }
}
