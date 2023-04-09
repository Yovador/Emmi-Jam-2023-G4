using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class WallsBehaviour : MonoBehaviour
{
    [System.Serializable]
    private class PairDirectionWall
    {
        public CameraDirection CameraDirection;
        public GameObject Wall;
    }

    [Inject]
    private PlayerBehavior _player;
    [SerializeField]
    private List<PairDirectionWall> _pairDirectionWalls;

    private void Start()
    {
        _player.ObserveEveryValueChanged(x => x.CameraRotation).Subscribe(x => OnCameraUpdate(x));
    }

    private void OnCameraUpdate(float camRot)
    {
        CameraDirection target;
        camRot = camRot + 45;
        camRot = (360 + camRot)%360;
        target = (CameraDirection)camRot;
        Debug.Log($"{camRot} / {target}");
        OnCameraUpdate(target);
    }
    private void OnCameraUpdate(CameraDirection target)
    {
        switch (target)
        {
            case CameraDirection.North:
                ActivateWall(CameraDirection.North, true);
                ActivateWall(CameraDirection.West, true);
                ActivateWall(CameraDirection.South, false);
                ActivateWall(CameraDirection.East, false);
                break;
            case CameraDirection.West:
                ActivateWall(CameraDirection.North, true);
                ActivateWall(CameraDirection.West, false);
                ActivateWall(CameraDirection.South, false);
                ActivateWall(CameraDirection.East, true);
                break;
            case CameraDirection.South:
                ActivateWall(CameraDirection.North, false);
                ActivateWall(CameraDirection.West, false);
                ActivateWall(CameraDirection.South, true);
                ActivateWall(CameraDirection.East, true);
                break;

            case CameraDirection.East:
                ActivateWall(CameraDirection.North, false);
                ActivateWall(CameraDirection.West, true);
                ActivateWall(CameraDirection.South, true);
                ActivateWall(CameraDirection.East, false);
                break;
            default:
                break;
        }
    }

    private void ActivateWall(CameraDirection target, bool state)
    {
        GameObject wall = _pairDirectionWalls.FirstOrDefault(x => x.CameraDirection == target).Wall;
        wall.SetActive(state);
    }
}

public enum CameraDirection
{
    North = 0,
    West = 90,
    South = 180,
    East = 270
}
