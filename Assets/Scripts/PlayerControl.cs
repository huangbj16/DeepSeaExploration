using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public AudioSource collisionAudioEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float verticalForce = Input.GetAxis("Vertical");
        float horizontalForce = Input.GetAxis("Horizontal");
        bool throttleButton = Input.GetButton("Jump");
        bool isUnderControl = false;
        // Debug.Log("vertical = " + verticalForce + ", horizontal = "+ horizontalForce);
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
        if (throttleButton == true)
        {
            isUnderControl = true;
            this.transform.position += this.transform.forward * Time.deltaTime;
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
