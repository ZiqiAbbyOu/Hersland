using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HL.Character.Player
{
    /// <summary>
    /// The player controller including movement
    /// </summary>
    public class PlayerMovementController : MonoBehaviour
    {
        public float speed = 5.0f;
        public float rotationSpeed = 700.0f;
        private Rigidbody rb;
        public Transform cameraTransform;

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

            Vector3 movement = cameraTransform.right * horizontal + cameraTransform.forward * vertical;

            movement.y = 0; // ignore camera's y rotation

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