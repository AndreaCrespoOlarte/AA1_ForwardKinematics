using UnityEngine;

public static class MyMath
{
    public const float PI = 3.14159265358979f;
    public const float TwoPi = 2f * PI;
    public const float HalfPi = PI / 2f;

    // ------------------------------------------------------------
    // SIN (Taylor series)
    // sin(x) = x - x^3/3! + x^5/5! - x^7/7! + ...
    // The series is only accurate near 0, so we first wrap the angle
    // into [-PI/2, PI/2] using the symmetries of sine.
    // ------------------------------------------------------------
    public static float Sin(float x)
    {
        // 1. Wrap into [-PI, PI]
        x = x - TwoPi * Floor(x / TwoPi + 0.5f);

        // 2. Fold into [-PI/2, PI/2]  (sin(PI - x) == sin(x))
        if (x > HalfPi) x = PI - x;
        else if (x < -HalfPi) x = -PI - x;

        // 3. Taylor series: each term = previous * -x^2 / ((2n)(2n+1))
        float term = x;
        float sum = x;
        float x2 = x * x;
        for (int n = 1; n <= 8; n++)
        {
            term *= -x2 / ((2 * n) * (2 * n + 1));
            sum += term;
        }
        return sum;
    }

    // ------------------------------------------------------------
    // COS  (cos(x) = sin(x + PI/2))
    // ------------------------------------------------------------
    public static float Cos(float x)
    {
        return Sin(x + HalfPi);
    }

    // ------------------------------------------------------------
    // SQRT (Newton-Raphson / Babylonian method)
    // Repeatedly improve a guess:  guess = (guess + x / guess) / 2
    // ------------------------------------------------------------
    public static float Sqrt(float x)
    {
        if (x < 0f) return float.NaN;
        if (x == 0f || float.IsPositiveInfinity(x)) return x;

        float guess = x >= 1f ? x * 0.5f : 1f;
        for (int i = 0; i < 100; i++)
        {
            float next = 0.5f * (guess + x / guess);
            if (Abs(next - guess) <= 1e-7f * next) // converged
                return next;
            guess = next;
        }
        return guess;
    }

    // ------------------------------------------------------------
    // ACOS (bisection using our own Cos)
    // On [0, PI], cos is strictly decreasing, so we can binary-search
    // for the angle whose cosine equals x.
    // ------------------------------------------------------------
    public static float Acos(float x)
    {
        if (x > 1f || x < -1f) return float.NaN;
        if (x == 1f) return 0f;
        if (x == -1f) return PI;

        float low = 0f;
        float high = PI;
        for (int i = 0; i < 40; i++)
        {
            float mid = (low + high) * 0.5f;
            if (Cos(mid) > x)
                low = mid;   // cos(mid) too big -> angle must be larger
            else
                high = mid;
        }
        return (low + high) * 0.5f;
    }

    // ------------------------------------------------------------
    // Helpers (so we don't rely on System.Math / Mathf)
    // ------------------------------------------------------------
    public static float Abs(float x)
    {
        return x < 0f ? -x : x;
    }

    public static float Floor(float x)
    {
        int i = (int)x;               // truncates toward zero
        return (x < i) ? i - 1 : i;   // fix up negatives
    }
}