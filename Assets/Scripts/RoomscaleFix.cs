using UnityEngine;
using Valve.VR;

public class RoomscaleFix : MonoBehaviour
{
    public float playAreaHeight = 2f;
    public float playAreaWidth = 2f;

    private CharacterController _character;

    void Start()
    {
        _character = GetComponent<CharacterController>();
        if (_character == null)
        {
            _character = gameObject.AddComponent<CharacterController>();
        }
    }

    void FixedUpdate()
    {
        var playArea = new HmdQuad_t();
        SteamVR_PlayArea.GetBounds(SteamVR_PlayArea.Size.Calibrated, ref playArea);
        var centerPoint = SteamVR_Render.Top().head.position;
        _character.center = new Vector3(centerPoint.x - transform.position.x, playAreaHeight / 2 + _character.skinWidth, centerPoint.z - transform.position.z);
        _character.height = playAreaHeight;
        _character.radius = Mathf.Min(playArea.vCorners0.v0 - playArea.vCorners2.v0, playArea.vCorners2.v2 - playArea.vCorners0.v2) / 2f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + _character.center, new Vector3(playAreaWidth, playAreaHeight, _character.radius * 2f));
    }
}