using UnityEngine;

public class SwingTwist
{
    public static float Clamp(float angle, float min, float max)
    {
        if(angle < min)
            return min;
        if (angle > max)
            return max;
        return angle; 
    }

    public static void Decompose(MyQuaternion q, out MyQuaternion swing, out MyQuaternion twist)
    {
        q.Normalize();

        float length = (float)Mathf.Sqrt(q.w * q.w + q.z * q.z);

        twist = new MyQuaternion(q.w / length, 0f, 0f, q.z/length);

        //q = q_swing * q_twist
        //q_swing = q * (q_twist)^{-1} || q_swing = q * (q_twist.conjugated())

        swing = MyQuaternion.Multiply(q, twist.Conjugate()); 
        swing.Normalize();

    }

    public static Vector2 GetAxisSwing(MyQuaternion swing)
    {
        float length = (float) Mathf.Sqrt(swing.x * swing.x + swing.y * swing.y);

        float angle = 2f * (float)Mathf.Atan2(length, swing.w);

        return new Vector2(swing.x * angle/length, swing.y * angle / length); 
    }

    public static MyQuaternion FromSwingCoordinates(Vector2 swing)
    {
        float angle = (float)Mathf.Sqrt(swing.x * swing.x + swing.y * swing.y);

        Vector3 axis = new Vector3(swing.x / angle, swing.y / angle, 0);

        return new MyQuaternion(axis, angle); 
    }

    public static float GetTwistAngle(MyQuaternion twist)
    {
        return 2f * (float)Mathf.Atan2(twist.z, twist.w);
    }

}
