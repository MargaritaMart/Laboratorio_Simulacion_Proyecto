using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tiroParabolico : MonoBehaviour
{
    public float velocidad, angulo, gravedad = 3.7f, posiX, posiY, actTime;
    private Slider velociSlider, anguloSlider;
    public Button iniciar, modificar, salir;
    public Text datoVel, datoAng;
    public Dropdown gravedades;
    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        velociSlider = GameObject.Find("SliderVel").GetComponent<Slider>();
        anguloSlider = GameObject.Find("SliderAng").GetComponent<Slider>();
        datoAng = GameObject.Find("TextDatoAng").GetComponent<Text>();
        datoVel = GameObject.Find("TextDatoVel").GetComponent<Text>();
        iniciar.onClick.AddListener(iniciarPro);
        modificar.onClick.AddListener(modifi);
        salir.onClick.AddListener(Exit);
        var dropdown = gravedades.transform.GetComponent<Dropdown>();
        gravedades.onValueChanged.AddListener(delegate { Gravis(dropdown); });
    }
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (posiY > -0.01f)
            {
                CalX();
                CalY();
                this.transform.position = new Vector3(posiX, posiY, this.transform.position.z);
            }
        }
        else
        {
            this.transform.position = new Vector3(posiX, posiY, this.transform.position.z);
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
        posiX = 0;
        posiY = 0;
        actTime = 0;
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
    void Gravis(Dropdown dropdown)
    {
        int index = dropdown.value;
        if (!start)
        {
            switch (dropdown.options[index].text)
            {
                case "Mercurio (3.7)":
                    gravedad = 3.7f;
                    break;
                case "Venus (8.87)":
                    gravedad = 8.87f;
                    break;
                case "Tierra (9.8)":
                    gravedad = 9.8f;
                    break;
                case "Luna (1.62)":
                    gravedad = 1.62f;
                    break;
                case "Marte (3.71)":
                    gravedad = 3.71f;
                    break;
                case "Jupiter (24.79)":
                    gravedad = 24.79f;
                    break;
                case "Saturno (10.44)":
                    gravedad = 10.44f;
                    break;
                case "Urano (8.87)":
                    gravedad = 8.87f;
                    break;
                case "Neptuno (11.15)":
                    gravedad = 11.15f;
                    break;
                case "Pluton (0.62)":
                    gravedad = 0.62f;
                    break;
            }
        }
    }
    void TomaDatos()
    {
        velocidad = velociSlider.value;
        angulo = anguloSlider.value;
        datoAng.text = angulo.ToString();
        datoVel.text = velocidad.ToString();
        angulo = angulo * Mathf.PI / 180.0f;
    }
    void CalX()
    {
        float k1, k2, k3, k4;
        k1 = velocidad * Mathf.Cos(angulo) * Time.deltaTime;
        k4 = k3 = k2 = k1;
        posiX = posiX + (k1 / 6) + (k2 / 3) + (k3 / 3) + (k4 / 6);
    }
    void CalY()
    {
        float k1, k2, k3, k4;
        actTime = actTime + Time.deltaTime;
        k1 = (velocidad * Mathf.Sin(angulo) * Time.deltaTime) - (gravedad * actTime * Time.deltaTime);
        k2 = ((velocidad * Mathf.Sin(angulo)) - (gravedad * (actTime + (Time.deltaTime / 2)))) * Time.deltaTime;
        k3 = ((velocidad * Mathf.Sin(angulo)) - (gravedad * (actTime + (Time.deltaTime / 2)))) * Time.deltaTime;
        k4 = ((velocidad * Mathf.Sin(angulo)) - (gravedad * (actTime + Time.deltaTime))) * Time.deltaTime;
        posiY = posiY + (k1 / 6) + (k2 / 3) + (k3 / 3) + (k4 / 6);
    }

}
