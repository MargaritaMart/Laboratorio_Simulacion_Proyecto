using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Resorte : MonoBehaviour
{
    private float masa, miu, k, distancia, velInicial;
    private Slider masaSlider, miuSlider, kSlider, distSlider, velSlider;
    public Button iniciar, modificar, salir;
    public Text datoMasa, datoMiu, datoK, datoDist, datoVel;
    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        masaSlider = GameObject.Find("SliderMasa").GetComponent<Slider>();
        miuSlider = GameObject.Find("SliderMiu").GetComponent<Slider>();
        kSlider = GameObject.Find("SliderK").GetComponent<Slider>();
        distSlider = GameObject.Find("SliderDist").GetComponent<Slider>();
        velSlider = GameObject.Find("SliderVel").GetComponent<Slider>();
        datoMasa = GameObject.Find("TextDatoMasa").GetComponent<Text>();
        datoMiu = GameObject.Find("TextDatoMiu").GetComponent<Text>();
        datoK = GameObject.Find("TextDatoK").GetComponent<Text>();
        datoDist = GameObject.Find("TextDatoDist").GetComponent<Text>();
        datoVel = GameObject.Find("TextDatoVel").GetComponent<Text>();
        iniciar.onClick.AddListener(iniciarPro);
        modificar.onClick.AddListener(modifi);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            this.transform.position = new Vector3(distancia, this.transform.position.y, this.transform.position.z);
            distancia = (velInicial * Time.deltaTime) + distancia;
            velInicial = velocidad(distancia, velInicial);
        }
        else
        {
            this.transform.position = new Vector3(distancia, this.transform.position.y, this.transform.position.z);
            TomaDatos();
        }
    }

    void iniciarPro()
    {
        start = true;
    }

    void modifi()
    {
        start = false;
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    void TomaDatos()
    {
        masa = masaSlider.value;
        miu = miuSlider.value;
        k = kSlider.value;
        distancia = distSlider.value;
        velInicial = velSlider.value;
        datoMasa.text = masa.ToString();
        datoMiu.text = miu.ToString();
        datoK.text = k.ToString();
        datoDist.text = distancia.ToString();
        datoVel.text = velInicial.ToString();
    }

    float velocidad(float x, float velocidad)
    {
        float a, b, k1, k2, k3, k4, newVelocidad;
        a = (-miu / masa);
        b = (k / masa);
        k1 = ((a * velocidad) - (b * x)) * Time.deltaTime;
        k2 = ((a * (velocidad + (k1 / 2))) - (b * x)) * Time.deltaTime;
        k3 = ((a * (velocidad + (k2 / 2))) - (b * x)) * Time.deltaTime;
        k4 = ((a * (velocidad + k3)) - (b * x)) * Time.deltaTime;
        newVelocidad = velocidad + (k1 / 6) + (k2 / 3) + (k3 / 3) + (k4 / 6);
        return newVelocidad;
    }

}
