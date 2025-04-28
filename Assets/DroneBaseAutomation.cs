using UnityEngine;
using System.Collections;

public class DroneBaseAutomation : MonoBehaviour
{
    // Public variables to assign in the Inspector
    public GameObject platform;       // The platform that raises the drone
    public GameObject scissorMechanism; // The scissor mechanism
    public GameObject roof;           // The roof that slides open
    public float raiseSpeed = 2.0f;     // Speed of the platform/scissor raise
    public float roofSlideSpeed = 2.0f; // Speed of the roof slide
    public float targetRaiseHeight = 3.0f; // Target height for the platform
    public float roofSlideDistance = 5.0f;  // Distance the roof slides

    // Private variables to store initial positions and target positions
    private Vector3 platformInitialPosition;
    private Vector3 scissorInitialPosition;
    private Vector3 roofInitialPosition;
    private Vector3 roofTargetPosition;

    private bool isRaising = false;
    private bool isOpeningRoof = false;
    private float startTime;

    void Start()
    {
        // Store the initial positions of the platform, scissor, and roof
        platformInitialPosition = platform.transform.position;
        scissorInitialPosition = scissorMechanism.transform.position; // You might need to adjust this depending on how you want the scissor to move
        roofInitialPosition = roof.transform.position;
        roofTargetPosition = roofInitialPosition + roof.transform.right * roofSlideDistance; // Assuming roof slides along its right axis
    }

    void Update()
    {
        if (isRaising)
        {
            float timeElapsed = Time.time - startTime;
            float percentageComplete = timeElapsed / (targetRaiseHeight / raiseSpeed);

            if (percentageComplete >= 1.0f)
            {
                percentageComplete = 1.0f;
                isRaising = false;
            }

            // Raise the platform
            platform.transform.position = Vector3.Lerp(platformInitialPosition, platformInitialPosition + Vector3.up * targetRaiseHeight, percentageComplete);

            // Animate the scissor mechanism (this will likely need fine-tuning based on your model's hierarchy and how the scissor is rigged)
            // This is a placeholder - you'll need to figure out the correct transformations (rotation, scaling) for your scissor.
            scissorMechanism.transform.position = Vector3.Lerp(scissorInitialPosition, scissorInitialPosition + Vector3.up * (targetRaiseHeight / 2), percentageComplete);
            scissorMechanism.transform.localScale = Vector3.Lerp(scissorMechanism.transform.localScale, scissorMechanism.transform.localScale * (1 + percentageComplete * 0.5f), percentageComplete); // Example scaling

        }

        if (isOpeningRoof)
        {
            float timeElapsed = Time.time - startTime;
            float percentageComplete = timeElapsed / (roofSlideDistance / roofSlideSpeed);

            if (percentageComplete >= 1.0f)
            {
                percentageComplete = 1.0f;
                isOpeningRoof = false;
            }

            // Slide the roof open
            roof.transform.position = Vector3.Lerp(roofInitialPosition, roofTargetPosition, percentageComplete);
        }
    }

    // Public method to start the raising and opening sequence
    public void StartRaiseAndOpen()
    {
        isRaising = true;
        isOpeningRoof = true;
        startTime = Time.time;
    }

    // You can add a method to reverse the animation if needed
    public void StartLowerAndClose()
    {
        isRaising = true;
        isOpeningRoof = true;
        startTime = Time.time;

        // Swap target and initial positions for lowering/closing
        Vector3 tempPlatform = platformInitialPosition;
        platformInitialPosition = platform.transform.position;
        platformTargetPosition = tempPlatform; // You'll need to define platformTargetPosition

        Vector3 tempRoof = roofInitialPosition;
        roofInitialPosition = roof.transform.position;
        roofTargetPosition = tempRoof;
    }
}
