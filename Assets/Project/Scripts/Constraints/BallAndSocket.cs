using UnityEngine;

public class BallAndSocket : MonoBehaviour
{
    private float coneAngle; 
    private float minTwist; 
    private float maxTwist;
    
    public BallAndSocket(float _coneAngle, float _minTwist, float _maxTwist) 
    { 
        coneAngle = _coneAngle;
        minTwist = _minTwist;
        maxTwist = _maxTwist;
    }

    public MyQuaternion Constrain(MyQuaternion requested)
    {
        MyQuaternion q_twist, q_swing;
        SwingTwist.Decompose(requested, out q_swing, out q_twist); 

        Vector2 swing = SwingTwist.GetAxisSwing(q_swing);

        float swingAngle = (float)Mathf.Sqrt(swing.x * swing.x + swing.y * swing.y);

        if (swingAngle > coneAngle && swingAngle > 0f)
        {
            float scale = coneAngle / swingAngle; 
            swing.x *= scale;
            swing.y *= scale;
        }

        float twistAngle = SwingTwist.GetTwistAngle(q_twist);
        twistAngle = SwingTwist.Clamp(twistAngle, minTwist, maxTwist);

        q_swing = SwingTwist.FromSwingCoordinates(swing); 
        q_twist = new MyQuaternion(new Vector3(0f,0f,1f), twistAngle);

        MyQuaternion q = MyQuaternion.Multiply(q_twist, q_swing);
        q.Normalize();

        return q; 
    }
}
