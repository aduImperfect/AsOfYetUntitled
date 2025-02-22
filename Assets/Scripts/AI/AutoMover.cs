using UnityEngine;

public class AutoMover : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distance;

    public float interpT;
    float speedDamper;

    Vector3 startPos;
    Vector3 endPos;

    bool isBegun;

    public void Start()
    {
        interpT = 0;
        speedDamper = 0.1f;
        isBegun = false;
        startPos = this.transform.position;
    }

    public void Update()
    {
        if (!isBegun)
        {
            startPos = this.transform.position;
            endPos = new Vector3(this.transform.position.x - distance, this.transform.position.y, this.transform.position.z);
            isBegun = true;
        }
        else
        {
            this.transform.position = new Vector3(Mathf.Lerp(startPos.x, endPos.x, interpT), this.transform.position.y, this.transform.position.z);
        }

        interpT += 0.5f * speed * speedDamper * Time.deltaTime;

        if (interpT > 1.0f)
        {
            isBegun = false;
            this.transform.position = startPos;
            interpT = 0.0f;
        }
    }
}
