using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CausticAnim : MonoBehaviour
{
    public float timeFactor;
    void Start()
    {
        transform.DOMove(new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z + 2f), 20 * timeFactor).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(transform.eulerAngles.x, 10, transform.eulerAngles.z), 10 * timeFactor).SetLoops(-1, LoopType.Yoyo);
    }


}
