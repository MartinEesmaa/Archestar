using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem.Sample
{
public class NewButton : MonoBehaviour
{
    int num = 0;
    public void OnButtonDown(Hand fromHand)

        {
            ColorSelf(Color.red);
            num++;
        }
    public void OnButtonUp(Hand fromHand)

        {
            ColorSelf(Color.blue);
        }
    private void ColorSelf(Color newColor)

    {

    Renderer[] renderers = this.GetComponentsInChildren<Renderer>();

    for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
    {

        renderers[rendererIndex].material.color = newColor;
    }
    }
}
}