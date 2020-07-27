using UnityEngine;

[RequireComponent(typeof(IInputActions))]
public class InputController : MonoBehaviour
{
    private IInputActions inputActions;

    private Vector3 mousePos;

    private bool shot;
    private bool lastShot;

    private Camera mainCamera;

    private float mouseHeight;

    private Transform sunRoot;

    private void Start()
    {
        mainCamera = Camera.main;

        sunRoot = GameObject.FindGameObjectWithTag(Constants.SUN_TAG).transform;

        inputActions = GetComponent<IInputActions>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsOnPanel.Check())
        {
            mousePos = Input.mousePosition;

            mouseHeight = mainCamera.transform.position.y - sunRoot.position.y;

            mousePos.z = mouseHeight;

            inputActions.SelectTarget(mainCamera.ScreenToWorldPoint(mousePos), mousePos);
        }

        shot = Input.GetKeyDown(KeyCode.Space);

        if (shot != lastShot)
        {
            lastShot = shot;
            inputActions.Fire(lastShot);
        }
    }
}
