using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HL.Character.Player
{
    /// <summary>
    /// The player controller including movement
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5.0f;
        public float rotationSpeed = 700.0f;
        private Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 movement = transform.right * horizontal + transform.forward * vertical;

            // Move and rotate the player
            if (movement.magnitude > 0.1f )
            {
                float targetAngle = Mathf.Atan2(movement.x, movement.z)*Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation,rotation,rotationSpeed));
                rb.MovePosition(transform.position + movement * speed * Time.deltaTime); 
            }
        }
    }
}