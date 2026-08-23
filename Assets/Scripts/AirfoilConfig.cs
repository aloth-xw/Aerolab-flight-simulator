using UnityEngine;

public class AirfoilConfig : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve liftCoefficientCurve;

    [SerializeField]
    private AnimationCurve dragCoefficientCurve;

    public float GetLiftCoefficient(float aoa)
    {
        return liftCoefficientCurve.Evaluate(aoa);
    }

    public float GetDragCoefficient(float aoa)
    {
        return dragCoefficientCurve.Evaluate(aoa);
    }
}
