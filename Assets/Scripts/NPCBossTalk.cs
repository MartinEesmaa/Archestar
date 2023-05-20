using System.Collections;
using UnityEngine;
using Valve.VR;
using UnityEngine.XR;
using UnityEngine.UI;
using TMPro;

public class NPCBossTalk : MonoBehaviour
{
    public string dialogue;
    public GameObject dialogueCanvas;
    public TextMeshProUGUI dialogueText;

    public string playerTag = "Player";
    public KeyCode activationKey = KeyCode.JoystickButton0;

    private bool inRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            inRange = false;
            dialogueCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (inRange && ColliderContainsPlayer() && (Input.GetKeyDown(activationKey) || Input.GetButtonDown("Fire1")))
        {
            dialogueCanvas.SetActive(true);
            StartCoroutine(DisplayDialogue());
        }
    }

    bool ColliderContainsPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 2f);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag(playerTag))
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator DisplayDialogue()
    {
        // Lock the VR position
        XRDevice.DisableAutoXRCameraTracking(Camera.main, true);

        // Display the canvas
        dialogueCanvas.SetActive(true);
        dialogueText.text = dialogue;

        // Wait for the dialogue to finish
        yield return new WaitForSeconds(3f);

        // Hide the canvas
        dialogueText.text = "";
        dialogueCanvas.SetActive(false);

        // Unlock the VR position
        XRDevice.DisableAutoXRCameraTracking(Camera.main, false);
    }
}