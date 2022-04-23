using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pendiente : MonoBehaviour
{
    public float angulo, velocidad, distancia, gravedad = 3.7f, actTime, posiX, posiY;
    public GameObject rampita;
    private float aceleracion;
    private Slider anguloSlider, veloSlider, distSlider;
    public Button iniciar, modificar, salir;
    public Text datoAng, datoVel, datoDist;
    public Dropdown gravedades;
    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        anguloSlider = GameObject.Find("SliderAng").GetComponent<Slider>();
        distSlider = GameObject.Find("SliderDist").GetComponent<Slider>();
        datoAng = GameObject.Find("TextDatoAng").GetComponent<Text>();
        datoDist = GameObject.Find("TextDatoDist").GetComponent<Text>();
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
            if (posiY > 0)
            {
                NextDistance();
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
        angulo = anguloSlider.value;
        distancia = distSlider.value;
        datoAng.text = angulo.ToString();
        datoDist.text = distancia.ToString();
        this.transform.rotation = Quaternion.Euler(0, 0, angulo);
        rampita.transform.rotation = Quaternion.Euler(0, 0, angulo);
        angulo = angulo * Mathf.PI / 180.0f;
        aceleracion = -gravedad * Mathf.Sin(angulo);
        posiX = distancia * Mathf.Cos(angulo);
        posiY = distancia * Mathf.Sin(angulo);
    }
    void NextDistance()
    {
        actTime += Time.deltaTime;
        distancia = (aceleracion * actTime * Time.deltaTime) + distancia;
        posiX = distancia * Mathf.Cos(angulo);
        posiY = distancia * Mathf.Sin(angulo);
    }
}
