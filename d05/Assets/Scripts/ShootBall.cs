using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    public float gaugeSpeed;
    public float clubStrength;

    bool isTryingToShoot;
    int increase = 1;
    float gaugeHeight = 1;
    float maxGaugeHeight;
    Image gaugeLevel;
    GameObject powerGauge;
    Rigidbody rb;
    Transform arrow;
    Transform ball;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        arrow = GetComponentsInChildren<Transform>()[1];
        arrow.gameObject.SetActive(false);
        ball = GameObject.FindWithTag("Ball").transform;
        Debug.Log(GameObject.FindWithTag("PowerLevel"));
        gaugeLevel = GameObject.FindWithTag("PowerLevel").GetComponent<Image>();
        powerGauge = GameObject.FindWithTag("PowerGauge");
        maxGaugeHeight = gaugeLevel.rectTransform.sizeDelta.y;
        powerGauge.gameObject.SetActive(false);
    }

    void DetermineShotStrength()
    {
        if (gaugeHeight >= maxGaugeHeight || gaugeHeight <= 0)
            increase *= -1;
        gaugeHeight += gaugeSpeed * increase;
        gaugeLevel.rectTransform.sizeDelta = new Vector2(gaugeLevel.rectTransform.sizeDelta.x, gaugeHeight);
    }

    void StopVelocity()
    {
        ball.transform.rotation = Quaternion.Euler(Vector3.zero);
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Shoot()
    {
        rb.constraints = RigidbodyConstraints.None;
        float strengthPower = gaugeHeight / maxGaugeHeight;
        Vector3 dir = arrow.transform.position - ball.transform.position;
        rb.AddForce(dir * clubStrength * strengthPower, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gm.isInFreeView)
        {
            if (!isTryingToShoot)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isTryingToShoot = true;
                    arrow.gameObject.SetActive(true);
                    powerGauge.gameObject.SetActive(true);
                    arrow.transform.position = ball.transform.position + new Vector3(0, 1, 1);
                    //StartCoroutine(DetermineShotStrength());
                }
            }
            else
            {
                DetermineShotStrength();

                if (Input.GetKey(KeyCode.A))
                    arrow.transform.RotateAround(ball.transform.position, Vector3.up, 100 * Time.deltaTime);
                else if (Input.GetKey(KeyCode.D))
                    arrow.transform.RotateAround(ball.transform.position, Vector3.up, -100 * Time.deltaTime);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Shoot();
                    isTryingToShoot = false;
                    arrow.gameObject.SetActive(false);
                    powerGauge.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(CheckWhenToStop());
    }

    IEnumerator CheckWhenToStop()
    {
        while (rb.velocity.magnitude > 5)
        {
            Debug.Log("Yo");
            yield return new WaitForSeconds(0.5f);
        }
        StopVelocity();

    }
}
