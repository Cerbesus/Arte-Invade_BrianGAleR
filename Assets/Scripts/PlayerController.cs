using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController controller;
    public float gravity = 240;
    public float rotationSpeed = 100;
    public float speed = 100;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        if (!controller.isGrounded)
        {
            Vector3 descenso = Vector3.down;
            descenso.y = descenso.y * gravity * Time.deltaTime;
            controller.Move(descenso);
        }
        transform.Rotate(transform.up * Horizontal * rotationSpeed * Time.deltaTime);
        controller.Move(transform.forward * Vertical * speed * Time.deltaTime);

    }
}
