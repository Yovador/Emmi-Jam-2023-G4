using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovableBloc : MonoBehaviour, IInteractable
{
    public enum MovementDirection {
        Horizontal = 0,
        Vertical = 1,
        All = 2
    }

    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public bool isActive;
    [SerializeField]
    public MovementDirection movementDirection;
    private AudioSource _audioSource;
    public GameObject callToAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isActive) return;
        if (rb.velocity == Vector3.zero)
        {
            _audioSource.Stop();
        }
        else if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
            _audioSource.loop = true;
        }
    }

    public void ActivateMove(bool active)
    {
       
        Debug.Log($"[Mvb] ActivateMove {active}");
        if (active)
        {
            isActive = true;
            switch (movementDirection)
            {
                case MovementDirection.Horizontal:
                    Debug.Log($"[Mvb] Horizontal {active}");
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY/* | RigidbodyConstraints.FreezePositionZ*/;
                    break;
                case MovementDirection.Vertical:
                    Debug.Log($"[Mvb] Vertical {active}");
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY /*| RigidbodyConstraints.FreezePositionX*/;
                    break;
                case MovementDirection.All:
                    Debug.Log($"[Mvb] Vertical {active}");
                    rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY /*| RigidbodyConstraints.FreezePositionX*/;
                    break;
            }
        }
        else
        {
            _audioSource.Stop();
            _audioSource.loop = false;
            isActive = false;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
        Debug.Log($"[Mvb] Constraints {rb.constraints}");
    }

    public void ShowCallToAction()
    {
        callToAction.SetActive(true);
    }

    public void HideCallToAction()
    {
        callToAction.SetActive(false);
    }
}
