using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float verticalForce = Input.GetAxis("Vertical");
        float horizontalForce = Input.GetAxis("Horizontal");
        bool throttleButton = Input.GetButton("Jump");
        Debug.Log("vertical = " + verticalForce + ", horizontal = "+ horizontalForce);
        if (Mathf.Abs(verticalForce) >= 1e-6)
        {
            this.transform.position += this.transform.up * verticalForce * Time.deltaTime;
        }
        if (Mathf.Abs(horizontalForce) >= 1e-6)
        {
            transform.Rotate(new Vector3(0, 45, 0) * horizontalForce * Time.deltaTime);
            //this.transform.position += this.transform.up * horizontalForce * Time.deltaTime;
        }
        if (throttleButton == true)
        {
            this.transform.position += this.transform.forward * Time.deltaTime;
        }

    }
}
