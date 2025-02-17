using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    public bool mouseLock;
    AimBaseState currentState;
    public HipfireState Hip = new HipfireState();
    public AimState Aim = new AimState();
    [SerializeField] float mouseSense = 1;
    float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    [HideInInspector] public Animator animator;
    private CinemachineVirtualCamera vCam;
    public float adsFov = 40;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10;

    [SerializeField] Transform aimPos;
    [SerializeField] float aimSmoothSpeed;
    [SerializeField] LayerMask aimMask;
    public Vector3 mouseWorldPosition = Vector3.zero;

    private void Start()
    {
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        animator = GetComponent<Animator>();
        currentFov = hipFov;
        SwitchState(Hip);
    }

    private void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);
        currentState.UpdateState(this);
        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        HandleCursor();
        UpdateAimPosition();
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void HandleCursor()
    {
        Cursor.lockState = mouseLock ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void UpdateAimPosition()
    {
        Vector2 screenCentrePoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCentrePoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimMask))
        {
            aimPos.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;

            // Debug para verificar el raycast
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
        }
    }
}
