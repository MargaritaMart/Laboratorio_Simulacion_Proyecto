using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LabMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button caiLibre, planoIncli, tiroPara, masaResorte, coLineal, co2D, co3D, coOBS, salir;
    void Start()
    {
        caiLibre.onClick.AddListener(CaidaLibre);
        planoIncli.onClick.AddListener(PlanoInclinado);
        tiroPara.onClick.AddListener(TiroParabolico);
        masaResorte.onClick.AddListener(MasaResorte);
        coLineal.onClick.AddListener(ColisionLineal);
        co2D.onClick.AddListener(Colision2D);
        co3D.onClick.AddListener(Colision3D);
        coOBS.onClick.AddListener(ColisionOBS);
        salir.onClick.AddListener(Exit);
    }
    void CaidaLibre()
    {
        SceneManager.LoadScene("simulacionCaidaLibre");
    }
    void PlanoInclinado()
    {
        SceneManager.LoadScene("simulacionPendiente");
    }
    void TiroParabolico()
    {
        SceneManager.LoadScene("simulacionTiroParabolico");
    }
    void MasaResorte()
    {
        SceneManager.LoadScene("SimulacionMasaResorte");
    }
    void ColisionLineal()
    {
        SceneManager.LoadScene("SimulacionCoLineal");
    }
    void Colision2D()
    {
        SceneManager.LoadScene("SimulacionCo2D");
    }
    void Colision3D()
    {
        SceneManager.LoadScene("SimulacionCo3D");
    }
    void ColisionOBS()
    {
        SceneManager.LoadScene("SimulacionCoObs");
    }
    public void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
