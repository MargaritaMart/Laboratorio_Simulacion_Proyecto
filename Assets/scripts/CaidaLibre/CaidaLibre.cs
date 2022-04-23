using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CaidaLibre : MonoBehaviour
{
    public float altura, velocidad, gravedad = 3.7f, aire, masa = 1000.0f;
    private Slider alturaSlider, velociSlider, aireSlider;
    public Text datoAltura, datoVeloci, datoAire;
    public Button iniciar, modificar, salir;
    public Dropdown gravedades;
    private bool start = false;
    private float coeArrastre = 0.6f, radio, resAire;
    // Start is called before the first frame update
    void Start()
    {
        alturaSlider = GameObject.Find("SliderAltura").GetComponent<Slider>();
        velociSlider = GameObject.Find("SliderVeloci").GetComponent<Slider>();
        aireSlider = GameObject.Find("SliderAire").GetComponent<Slider>();
        datoAltura = GameObject.Find("TextDatoAltura").GetComponent<Text>();
        datoVeloci = GameObject.Find("TextDatoVeloci").GetComponent<Text>();
        datoAire = GameObject.Find("TextDatoAire").GetComponent<Text>();
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
            Caida();
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, altura, this.transform.position.z);
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
        altura = alturaSlider.value;
        aire = aireSlider.value;
        datoAltura.text = altura.ToString();
        datoVeloci.text = velocidad.ToString();
        datoAire.text = aire.ToString();
        radio = this.transform.localScale.x / 2;
    }
    void Caida()
    {
        if (altura > 0)
        {
            resAire = (aire * velocidad * velocidad * ((radio * radio) * Mathf.PI) * coeArrastre) / (2 * masa);
            velocidad = velocidad + ((resAire - gravedad) * Time.deltaTime);
            altura = altura + (velocidad * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, altura, 0);
        }
    }
}
