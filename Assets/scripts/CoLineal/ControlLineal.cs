using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlLineal : MonoBehaviour
{
    public BolLineal[] bolitas, aux;
    private bool start = false;
    private Slider velociX1Slider, velociX2Slider;
    public Text datoVelociX1, datoVelociX2;
    public Button iniciar, modificar, salir;
    // Start is called before the first frame update
    void Start()
    {

        velociX1Slider = GameObject.Find("SliderVeloX1").GetComponent<Slider>();
        velociX2Slider = GameObject.Find("SliderVeloX2").GetComponent<Slider>();

        datoVelociX1 = GameObject.Find("TextDatoVeloX1").GetComponent<Text>();
        datoVelociX2 = GameObject.Find("TextDatoVeloX2").GetComponent<Text>();

        iniciar.onClick.AddListener(iniciarPro);
        modificar.onClick.AddListener(modifi);
        salir.onClick.AddListener(Exit);

        bolitas = this.gameObject.GetComponentsInChildren<BolLineal>();
        aux = bolitas;
    }
    void iniciarPro()
    {
        start = true;
        bolitas[0].start = true;
        bolitas[1].start = true;
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
    void modifi()
    {
        start = false;
        bolitas[0].start = false;
        bolitas[1].start = false;

        bolitas[0].transform.position = new Vector3(-6, 0, 0);
        bolitas[1].transform.position = new Vector3(6, 0, 0);

        bolitas[0].vPosition = new Vector3(-6, 0, 0);
        bolitas[1].vPosition = new Vector3(6, 0, 0);
    }
    void TomaDatos()
    {
        bolitas[0].vInitVelocity.x = velociX1Slider.value;
        datoVelociX1.text = velociX1Slider.value.ToString();
        bolitas[1].vInitVelocity.x = velociX2Slider.value;
        datoVelociX2.text = velociX2Slider.value.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            CalColision();
        }
        else
        {
            TomaDatos();
        }
    }
    void CalColision()
    {
        foreach (var bolita in bolitas)
        {
            foreach (var bolita2 in aux)
            {
                Vector3 n = Vector3.zero, vr = Vector3.zero, Fi = Vector3.zero;
                float vrn, J;

                //colision with obstacles
                float r, s;
                Vector3 d;

                r = bolita.fRadius + bolita2.fRadius;
                d = bolita.vPosition - bolita2.vPosition;
                s = d.magnitude - r;

                if (s <= 0.0f && bolita != bolita2)
                {
                    d.Normalize();
                    n = d;
                    vr = bolita.vVelocity - bolita2.vVelocity;
                    vrn = Vector3.Dot(vr, n);

                    if (vrn < 0.0f)
                    {
                        J = -(Vector3.Dot(vr, n)) * (bolita.restitucion + 1) /
                            (1 / bolita.fMass + 1 / bolita2.fMass);
                        Fi = n;
                        Fi *= J / Time.deltaTime;
                        bolita.vImpactForces += Fi;

                        bolita.vPosition -= n * s;
                        bolita.bCollision = true;
                    }
                }
            }
        }
    }
}
