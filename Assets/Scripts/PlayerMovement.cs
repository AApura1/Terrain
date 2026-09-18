using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Current Movement Settings")]

    [SerializeField]
    private float maxSpeed = 8f;

    [SerializeField]
    private float timeToReachMax = 0.3f;

    [SerializeField]
    private float timeToStop = 0.1f;

    [SerializeField]
    private float turnSpeed = 0.5f;

    [SerializeField]
    private float turnSpeedScalar = 2f;


    [Header("Normal Movement Settings")]

    [SerializeField]
    private float normalMaxSpeed = 8f;

    [SerializeField]
    private float normalTimeToReachMax = 0.3f;

    [SerializeField]
    private float normalTimeToStop = 0.1f;

    [SerializeField]
    private float normalTurnSpeed = 0.5f;

    [SerializeField]
    private float normalTurnSpeedScalar = 2f;

    private float windForce = 0f;

    float acceleration => maxSpeed / timeToReachMax;

    float deceleration => maxSpeed / timeToStop;

    float turnRate => (maxSpeed * turnSpeedScalar) / turnSpeed;


    Vector3 velocity = Vector3.zero;


    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");

        float targetSpeed = maxSpeed * input;

        float targetAccel = ChooseAccel(input);

        velocity.x = Mathf.MoveTowards(
            velocity.x,
            targetSpeed,
            targetAccel * Time.deltaTime
        );

        velocity.x += windForce * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
    }


    float ChooseAccel(float input)
    {
        bool isTurning =
            !Mathf.Approximately(
                Mathf.Sign(input),
                Mathf.Sign(velocity.x)
            );

        if (Mathf.Abs(input) > 0)
        {
            if (!isTurning)
                return acceleration;

            return turnRate;
        }

        return deceleration;
    }


    public void SetTerrain(TerrainZone.TerrainType terrain)
    {
        switch (terrain)
        {
            case TerrainZone.TerrainType.Sticky:

                maxSpeed = 2f;
                timeToReachMax = 2f;
                timeToStop = 1f;
                turnSpeed = 2f;
                turnSpeedScalar = 1f;
                windForce = 0f;

                Debug.Log("Entered Sticky Terrain");

                break;

            case TerrainZone.TerrainType.Icy:

                maxSpeed = 10f;
                timeToReachMax = 0.8f;
                timeToStop = 2f;
                turnSpeed = 1.5f;
                turnSpeedScalar = 1f;
                windForce = 0f;

                Debug.Log("Entered Icy Terrain");

                break;

            case TerrainZone.TerrainType.Windy:

                maxSpeed = 5f;
                timeToReachMax = 0.15f;
                timeToStop = 0.2f;
                turnSpeed = 1.5f;
                turnSpeedScalar = 1f;
                windForce = 3f;

                Debug.Log("Entered Windy Terrain");

                break;
        }
    }


    public void SetNormalMovement()
    {
        maxSpeed = normalMaxSpeed;
        timeToReachMax = normalTimeToReachMax;
        timeToStop = normalTimeToStop;
        turnSpeed = normalTurnSpeed;
        turnSpeedScalar = normalTurnSpeedScalar;
        windForce = 0f;

        Debug.Log("Returned to Normal Terrain");
    }
}