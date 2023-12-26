using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public AudioSource collisionAudioEffect;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float verticalForce = Input.GetAxis("Vertical");
        float horizontalForce = Input.GetAxis("Horizontal");
        float throttleForce = Input.GetAxis("RightTrigger");
        float lookUp = Input.GetAxis("LookUp");
        float lookRight = Input.GetAxis("LookRight");
        bool isUnderControl = false;
        Debug.Log("vertical = " + verticalForce + ", horizontal = "+ horizontalForce + ", RT = " + throttleForce);
        if (Mathf.Abs(verticalForce) >= 1e-6)
        {
            isUnderControl = true;
            this.transform.position += this.transform.up * verticalForce * Time.deltaTime;
        }
        if (Mathf.Abs(horizontalForce) >= 1e-6)
        {
            isUnderControl= true;
            transform.Rotate(new Vector3(0, 45, 0) * horizontalForce * Time.deltaTime);
            //this.transform.position += this.transform.up * horizontalForce * Time.deltaTime;
        }
        if (Mathf.Abs(throttleForce) >= 1e-6)
        {
            isUnderControl = true;
            this.transform.position += this.transform.forward * throttleForce * Time.deltaTime;
        }
        Debug.Log("lookup = " + lookUp + ", lookright = " + lookRight);
        if (Mathf.Abs(lookUp) >= 1e-6 || Mathf.Abs(lookRight) >= 1e-6)
        {
            isUnderControl = true;
            Vector3 rotationVector = new Vector3(0, 45, 0) * lookRight * Time.deltaTime + new Vector3(45, 0, 0) * lookUp * Time.deltaTime;
            mainCamera.transform.Rotate(rotationVector);
            mainCamera.transform.localRotation = Quaternion.Euler(mainCamera.transform.localRotation.eulerAngles[0], mainCamera.transform.localRotation.eulerAngles[1], 0);
            //mainCamera.transform.localRotation = Quaternion.Euler(rotationVector);
        }
        else
        {
            mainCamera.transform.localRotation = Quaternion.identity;
        }

        this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles[1], 0);
        if (isUnderControl)//override the rigidbody collision effect if under control
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("collision object = " + collision.gameObject);
        if (collision.gameObject.name.Contains("Rock"))
        {
            // Debug.Log("play collision audio");
            collisionAudioEffect.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Debug.Log("collision ends");
    }

}
