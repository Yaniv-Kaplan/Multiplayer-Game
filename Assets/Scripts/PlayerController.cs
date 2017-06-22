using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float m_speed = 5f;
    [SerializeField]
    private float m_rotationSensitivity = 3f;
    [SerializeField]
    private float m_thrustForce = 1000f;

    [Header("Joint Options")]
    [SerializeField]
    private float m_jointSpring = 20f;
    [SerializeField]
    private float m_jointMaxForce = 40f;

    private ConfigurableJoint m_joint;
    private PlayerMotor m_motor;

    private void Start()
    {
        m_motor = GetComponent<PlayerMotor>();
        m_joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(m_jointSpring);
    }

    private void Update()
    {
        //calculate movement velocity
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;
        
        //final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * m_speed;

        //apply movement
        m_motor.Move(_velocity);

        //rotation
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * m_rotationSensitivity;

        m_motor.Rotate(_rotation);

        //Camera rotation
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * m_rotationSensitivity;

        m_motor.RotateCamera(_cameraRotationX);

        //thruster force
        Vector3 _thrustForce = Vector3.zero;

        if (Input.GetButton("Jump"))
        {
            _thrustForce = Vector3.up * m_thrustForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(m_jointSpring);
        }
        m_motor.ApplyThruster(_thrustForce);

    }

    private void SetJointSettings(float _jointSpring)
    {
        m_joint.yDrive = new JointDrive
        {
            positionSpring = _jointSpring,
            maximumForce = m_jointMaxForce
        };
    }

}
