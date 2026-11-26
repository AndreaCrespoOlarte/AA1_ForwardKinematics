using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forwardAcceleration = 7f;
    [SerializeField] private float turningVelocity = 90f; 
    [SerializeField] private Camera cam;

    private float moveInput;
    private float turnInput;

    void Update()
    {
        moveInput = 0f;
        if (Input.GetKey(KeyCode.W)) moveInput += 1f;
        if (Input.GetKey(KeyCode.S)) moveInput -= 1f;

        turnInput = 0f;
        if (Input.GetKey(KeyCode.D)) turnInput += 1f;
        if (Input.GetKey(KeyCode.A)) turnInput -= 1f;
    }

    private void FixedUpdate()
    {
        Vector3 dir = cam.transform.forward;
        dir.y = 0f;
        dir.Normalize();

        Vector3 targetVelocity = dir * (moveInput * forwardAcceleration);
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

        //HAY QUE CAMVBIAR UNA VEZ IMPLEMENTADOS LOS SCRIPTS DEL RODRIGO DE QUATERNIONSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
        if (turnInput != 0f)
        {
            float turnAmount = turnInput * turningVelocity * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0f, turnAmount, 0f);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}