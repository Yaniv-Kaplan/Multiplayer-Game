using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Vector3 m_velocity = Vector3.zero;
    private Rigidbody m_player;
    private Vector3 m_rotation = Vector3.zero;
    private float m_cameraRotationX = 0f;
    private float currentCameraRoationX = 0f;
    private Vector3 m_thrustForce = Vector3.zero;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float m_cameraRotationLimit = 85f;


    private void Start()
    {
        m_player = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        m_velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        m_rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        m_cameraRotationX = _cameraRotationX;
    }

    public void ApplyThruster(Vector3 _thrusterForce)
    {
        m_thrustForce = _thrusterForce;
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if(m_velocity != Vector3.zero)
        {
            m_player.MovePosition(m_player.position + m_velocity * Time.fixedDeltaTime);
        }

        if(m_thrustForce != Vector3.zero)
        {
            m_player.AddForce(m_thrustForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    void PerformRotation()
    {
        m_player.MoveRotation(m_player.rotation * Quaternion.Euler(m_rotation));
        if(cam != null)
        {
            currentCameraRoationX -= m_cameraRotationX;
            currentCameraRoationX = Mathf.Clamp(currentCameraRoationX, -m_cameraRotationLimit, m_cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCameraRoationX, 0f, 0f);
        }
    }
}
