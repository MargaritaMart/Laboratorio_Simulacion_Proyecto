using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlOBS : MonoBehaviour
{
    // public BolOBS[] bolitas, aux;
    public List<BolOBS> bolitas, aux;
    private bool start = false;
    private Slider posiX1Slider, posiZ1Slider, velociX1Slider, velociZ1Slider;
    public Text datoPosiX1, datoPosiZ1, datoVelociX1, datoVelociZ1;
    public Button iniciar, modificar, salir;
    public GameObject balitas;
    public GameObject[] goBolitas;
    // Start is called before the first frame update
    void Start()
    {
        posiX1Slider = GameObject.Find("SliderPosiX1").GetComponent<Slider>();
        posiZ1Slider = GameObject.Find("SliderPosiZ1").GetComponent<Slider>();

        velociX1Slider = GameObject.Find("SliderVeloX1").GetComponent<Slider>();
        velociZ1Slider = GameObject.Find("SliderVeloZ1").GetComponent<Slider>();

        datoPosiX1 = GameObject.Find("TextDatoPosiX1").GetComponent<Text>();
        datoPosiZ1 = GameObject.Find("TextDatoPosiZ1").GetComponent<Text>();

        datoVelociX1 = GameObject.Find("TextDatoVeloX1").GetComponent<Text>();
        datoVelociZ1 = GameObject.Find("TextDatoVeloZ1").GetComponent<Text>();

        iniciar.onClick.AddListener(iniciarPro);
        modificar.onClick.AddListener(modifi);
        salir.onClick.AddListener(Exit);

        for (int i = 0; i < 10; i++)
        {
            var ball = Instantiate(balitas, new Vector3(Random.Range(0, 15), 0, Random.Range(0, 6)), Quaternion.identity);
        }

        goBolitas = GameObject.FindGameObjectsWithTag("Bolita");

        foreach (var item in goBolitas)
        {
            bolitas.Add(item.GetComponent<BolOBS>());
        }

        aux = bolitas;


    }
    void iniciarPro()
    {
        start = true;

        foreach (var compadrePedro in bolitas)
        {
            compadrePedro.start = true;
        }
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
    void modifi()
    {
        start = false;

        foreach (var compadrePedro in bolitas)
        {
            compadrePedro.start = false;
        }
    }
    void TomaDatos()
    {
        bolitas[0].vPosition.x = posiX1Slider.value;
        datoPosiX1.text = posiX1Slider.value.ToString();
        bolitas[0].vPosition.z = posiZ1Slider.value;
        datoPosiZ1.text = posiZ1Slider.value.ToString();

        bolitas[0].vInitVelocity.x = velociX1Slider.value;
        datoVelociX1.text = velociX1Slider.value.ToString();
        bolitas[0].vInitVelocity.z = velociZ1Slider.value;
        datoVelociZ1.text = velociZ1Slider.value.ToString();
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
