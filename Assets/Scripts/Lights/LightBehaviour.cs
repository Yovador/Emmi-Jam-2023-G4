using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Light))]
public class LightBehaviour : Toggleable
{
    [Inject]
    private List<ILightReceiver> _receivers;
    [SerializeField]
    private LayerMask _mask;
    private Light _light;

    [Inject]
    private void Injection()
    {
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        if (IsOn)
        {
            CastLight();
        }
    }

    private void CastLight()
    {
        foreach (ILightReceiver target in _receivers)
        {
            Transform targetTransform = (target as MonoBehaviour).transform;
            Vector3 direction = Vector3.Normalize(targetTransform.position - transform.position);
            float angle = Vector3.Angle(transform.forward, direction);
            float demiSpotAngle = _light.spotAngle / 2;
            if(angle > demiSpotAngle) 
            {
                continue; 
            }
            CastRay(target, targetTransform, direction);
        }
    }

    private void CastRay(ILightReceiver target, Transform targetTransform, Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit, Mathf.Infinity, _mask);
        Collider collider = raycastHit.collider;
        ILightReceiver receiver = collider.GetComponents<Component>().OfType<ILightReceiver>().FirstOrDefault();
        if (receiver == target)
        {
            Debug.Log("[Lgt] Light");
            receiver.ReceiveLight();
        }
    }

    protected override void Activate()
    {
        _light.enabled = true;
    }

    protected override void Deactivate()
    {
        _light.enabled = false;
    }
}
