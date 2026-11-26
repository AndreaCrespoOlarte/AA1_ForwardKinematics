using UnityEngine;
using System.Collections.Generic; 

public class TestJoint : MonoBehaviour
{
    public Transform baseJoint;
    public Transform ballJoint;
    public Transform endEffector;

    public List<Transform> joints;

    public float targetSwingXDegrees; 
    public float targetSwingYDegrees;
    public float targetTwistDegrees;

    public float coneAngleLimit = 60f; 
    public float minTwist = -30f; 
    public float maxTwist = 30f;

    private MyQuaternion initialJointPosition; 
    private Vector3 Link;

    private bool rotate = true;

    private void Start()
    {
        Quaternion startingRotation = ballJoint.rotation;
        initialJointPosition = new MyQuaternion(startingRotation.w, startingRotation.x,
                                                startingRotation.y, startingRotation.z);
        initialJointPosition.Normalize(); 

        Link = endEffector.position - ballJoint.position;

        joints.Add(baseJoint); 
        joints.Add(ballJoint); 
        joints.Add(endEffector); 
    }

    private void Update()
    {
        if (rotate)
        {
            float swingX = targetSwingXDegrees * Mathf.PI / 180f; //En el proyecto hay que implementar una funcion para esto
            float swingY = targetSwingYDegrees * Mathf.PI / 180f;
            float twist = targetTwistDegrees * Mathf.PI / 180f;

            MyQuaternion requestedSwing = SwingTwist.FromSwingCoordinates(new Vector2(swingX, swingY));
            MyQuaternion requestedTwist = new MyQuaternion(new Vector3(0f, 0f, 1f), twist);
            MyQuaternion requested = MyQuaternion.Multiply(requestedSwing, requestedTwist);

            float coneLimit = coneAngleLimit * Mathf.PI / 180f;
            float lowerTwist = minTwist * Mathf.PI / 180f;
            float upperTwist = maxTwist * Mathf.PI / 180f;

            BallAndSocket constraint = new BallAndSocket(coneLimit, lowerTwist, upperTwist);

            MyQuaternion constrained = constraint.Constrain(requested);
            FK.ApplyFK(joints, constrained, 1);

            rotate = false;
        }
        
    }
}
