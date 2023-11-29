using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    private float currentRotation = 0f;

    void Update()
    {
        currentRotation += rotationSpeed * Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);

        // Als de rotatie de 360 graden overschrijdt, reset de rotatie
        if (currentRotation >= 360f)
        {
            currentRotation = 0f;
        }
    }
}
