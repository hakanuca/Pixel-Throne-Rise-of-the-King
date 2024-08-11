using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float defDistanceRay = 100f;
    public Transform laserFirePoint;
    public LineRenderer laserLine;
    Transform laserOrigin;
    private void Awake()
    {
        laserOrigin = GetComponent<Transform>();
    }

    void LaserAttack()
    {
        if (Physics2D.Raycast(laserOrigin.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserOrigin.position, transform.right);
            Drw2DRay(laserOrigin.position, _hit.point, Color.red);
        }
        else
        {
            Drw2DRay(laserOrigin.position, laserOrigin.position + transform.right * defDistanceRay, Color.green);
        }
    }
    void Drw2DRay(Vector3 laserOriginPosition, Vector3 transformRight, Color green)
    {
        laserLine.SetPosition(0, laserOriginPosition);
        laserLine.SetPosition(1, transformRight);
    }
}
