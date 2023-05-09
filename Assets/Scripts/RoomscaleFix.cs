using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(XROrigin))]

public class RoomscaleFix : MonoBehaviour
{
    CharacterController _character;
    XROrigin _xrOrigin;
    void Start()
    {
        _character = GetComponent<CharacterController>();
        _xrOrigin = GetComponent<XROrigin>();
    }

    void FixedUpdate()
    {
        var centerPoint = transform.InverseTransformPoint(_xrOrigin.Camera.transform.position);
        _character.center = new Vector3(centerPoint.x, _character.height / 2 + _character.skinWidth, centerPoint.z);
        _character.Move(new Vector3(0.001f, -0.001f, 0.001f));
        _character.Move(new Vector3(-0.001f, -0.001f, -0.001f));
    }
}
