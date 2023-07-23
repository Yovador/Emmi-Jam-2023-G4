using UniRx;
using UnityEngine;

/// <summary>
/// Contains the data for the movement of the Player
/// </summary>
[System.Serializable]
public class PlayerMovementProfil
{
#if UNITY_EDITOR
    [ReadOnly]
    [SerializeField]
    private string _profilName;
#endif
    [SerializeField]
    private PlayerMovementProfilType _type;
    [SerializeField]
    private AnimationCurve _accelerationCurve;
    [SerializeField]
    private AnimationCurve _decelerationCurve;
    [SerializeField]
    private float _speed; 
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField, Range(-1f, 1f)]
    private float _instantTurnThreadshold;
    public PlayerMovementProfilType Type { get => _type; private set => _type = value; }
    public AnimationCurve AccelerationCurve { get => _accelerationCurve; private set => _accelerationCurve = value; }
    public AnimationCurve DecelerationCurve { get => _decelerationCurve; private set => _decelerationCurve = value; }
    public float Speed { get => _speed; private set => _speed = value; }
    public float RotationSpeed { get => _rotationSpeed; private set => _rotationSpeed = value; }
    public float InstantTurnThreadshold { get => _instantTurnThreadshold; private set => _instantTurnThreadshold = value; }

    public PlayerMovementProfil(PlayerMovementProfilType type)
    {
        Type = type;
        Speed = 1;
        AccelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1)});
        DecelerationCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 0)});
        InstantTurnThreadshold = 0.70f;
        SetupProfilNameInspector();
    }

    public PlayerMovementProfil(PlayerMovementProfilType type, float speed)
    {
        Type = type;
        Speed = speed;
        AccelerationCurve = new AnimationCurve();
        DecelerationCurve = new AnimationCurve();
        InstantTurnThreadshold = 0.70f;
        SetupProfilNameInspector();
    }


    private void SetupProfilNameInspector()
    {
#if UNITY_EDITOR
        _profilName = Type.ToString();
#endif
    }
}
