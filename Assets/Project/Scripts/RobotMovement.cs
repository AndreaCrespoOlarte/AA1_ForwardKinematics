using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forwardAcceleration = 7f;
    [SerializeField] private float turningVelocity = 90f; 
    [SerializeField] private Camera cam;

    private float moveInput;
    private float turnInput;

    private void Start()
    {
        MyVector3 v = new MyVector3 (3f, 4f, 0f);
        v = v.Normalize();
        Debug.Log(v.x + " " + v.Magnitude());
    }

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
        MyVector3 dir = cam.transform.forward;
        dir.y = 0f;
        dir = dir.Normalize();

        MyVector3 targetVelocity = dir * (moveInput * forwardAcceleration);
        rb.linearVelocity = new MyVector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

        if (turnInput != 0f)
        {
            float angle = turnInput * turningVelocity * Time.fixedDeltaTime * (MyMath.PI / 180f);

            MyQuaternion current = rb.rotation;                    // Quaternion -> MyQuaternion
            MyQuaternion delta = new MyQuaternion(Vector3.up, angle);

            rb.MoveRotation((MyQuaternion.Multiply(current, delta)).Normalize());        // MyQuaternion -> Quaternion
        }
    }
}