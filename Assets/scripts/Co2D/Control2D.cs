using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control2D : MonoBehaviour
{
    public Bol2D[] bolitas, aux;
    private bool start = false;
    private Slider posiX1Slider, posiX2Slider, posiZ1Slider, posiZ2Slider, velociX1Slider, velociX2Slider, velociZ1Slider, velociZ2Slider;
    public Text datoPosiX1, datoPosiX2, datoPosiZ1, datoPosiZ2, datoVelociX1, datoVelociX2, datoVelociZ1, datoVelociZ2;
    public Button iniciar, modificar, salir;
    // Start is called before the first frame update
    void Start()
    {
        posiX1Slider = GameObject.Find("SliderPosiX1").GetComponent<Slider>();
        posiX2Slider = GameObject.Find("SliderPosiX2").GetComponent<Slider>();
        posiZ1Slider = GameObject.Find("SliderPosiZ1").GetComponent<Slider>();
        posiZ2Slider = GameObject.Find("SliderPosiZ2").GetComponent<Slider>();

        velociX1Slider = GameObject.Find("SliderVeloX1").GetComponent<Slider>();
        velociX2Slider = GameObject.Find("SliderVeloX2").GetComponent<Slider>();
        velociZ1Slider = GameObject.Find("SliderVeloZ1").GetComponent<Slider>();
        velociZ2Slider = GameObject.Find("SliderVeloZ2").GetComponent<Slider>();

        datoPosiX1 = GameObject.Find("TextDatoPosiX1").GetComponent<Text>();
        datoPosiX2 = GameObject.Find("TextDatoPosiX2").GetComponent<Text>();
        datoPosiZ1 = GameObject.Find("TextDatoPosiZ1").GetComponent<Text>();
        datoPosiZ2 = GameObject.Find("TextDatoPosiZ2").GetComponent<Text>();

        datoVelociX1 = GameObject.Find("TextDatoVeloX1").GetComponent<Text>();
        datoVelociX2 = GameObject.Find("TextDatoVeloX2").GetComponent<Text>();
        datoVelociZ1 = GameObject.Find("TextDatoVeloZ1").GetComponent<Text>();
        datoVelociZ2 = GameObject.Find("TextDatoVeloZ2").GetComponent<Text>();

        iniciar.onClick.AddListener(iniciarPro);
        modificar.onClick.AddListener(modifi);
        salir.onClick.AddListener(Exit);

        bolitas = this.gameObject.GetComponentsInChildren<Bol2D>();
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
    }
    void TomaDatos()
    {
        bolitas[0].vPosition.x = posiX1Slider.value;
        datoPosiX1.text = posiX1Slider.value.ToString();
        bolitas[0].vPosition.z = posiZ1Slider.value;
        datoPosiZ1.text = posiZ1Slider.value.ToString();
        bolitas[1].vPosition.x = posiX2Slider.value;
        datoPosiX2.text = posiX2Slider.value.ToString();
        bolitas[1].vPosition.z = posiZ2Slider.value;
        datoPosiZ2.text = posiZ2Slider.value.ToString();

        bolitas[0].vInitVelocity.x = velociX1Slider.value;
        datoVelociX1.text = velociX1Slider.value.ToString();
        bolitas[0].vInitVelocity.z = velociZ1Slider.value;
        datoVelociZ1.text = velociZ1Slider.value.ToString();
        bolitas[1].vInitVelocity.x = velociX2Slider.value;
        datoVelociX2.text = velociX2Slider.value.ToString();
        bolitas[1].vInitVelocity.z = velociZ2Slider.value;
        datoVelociZ2.text = velociZ2Slider.value.ToString();
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
