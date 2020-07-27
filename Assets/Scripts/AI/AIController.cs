using UnityEngine;

[RequireComponent(typeof(IInputActions))]
public class AIController : MonoBehaviour
{
    private IInputActions inputActions;
    private ISelectRocket selectRocket;

    private IUpdateTimer updateFireTimer;

    private IUpdateTimer updateSensorTimer;

    /// <summary>
    /// TODO: To Config
    /// </summary>
    private float aiRadius = 15;

    private Collider[] overlapColliders;

    private Camera cameraMain;

    private void Start()
    {
        inputActions = GetComponent<IInputActions>();
        selectRocket = GetComponent<ISelectRocket>();

        updateFireTimer = new UpdateTimer(3, false);
        updateSensorTimer = new UpdateTimer(5, false);

        selectRocket.SelectRocket(0);

        overlapColliders = Physics.OverlapSphere(transform.position, aiRadius, 1 << LayerMask.NameToLayer(Constants.PLANET_LAYER));

        this.cameraMain = Camera.main;
    }

    private void Update()
    {
        updateSensorTimer.ExecuteUpdate(() =>
        {
            this.overlapColliders = Physics.OverlapSphere(transform.position, aiRadius, 1 << LayerMask.NameToLayer(Constants.PLANET_LAYER));
        });
         
        if (this.overlapColliders.Length > 0)
        {
            this.updateFireTimer.ExecuteUpdate(() =>
            {
                if (!this.overlapColliders[0])
                    return;

                Vector3 pos = this.overlapColliders[0].transform.position + this.RandomOffset();

                this.inputActions.SelectTarget(pos, cameraMain.WorldToScreenPoint(pos));

                this.selectRocket.SelectRocket(Random.Range(0, 3));

                this.inputActions.Fire(true);
            });
        }
        else
        {
            this.inputActions.Fire(false);
        }
    }

    private Vector3 RandomOffset()
    {
        return new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3));
    }
}
