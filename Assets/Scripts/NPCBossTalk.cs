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

    private bool inRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            dialogueCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetButtonDown("Fire1"))
        {
            dialogueCanvas.SetActive(true);
            StartCoroutine(DisplayDialogue());
        }
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