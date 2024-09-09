using JetBrains.Annotations;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    [SerializeField] GameObject Playerlightlight;
    private bool Playerlighton = false;

    void Start()
    {
        Playerlightlight.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            /* if (!Playerlighton)
            {
                Playerlightlight.gameObject.SetActive(true);
                Playerlighton = true;
            }
            else
            {
                Playerlightlight.gameObject.SetActive(false);
                Playerlighton = false;
            }
            */
            Playerlightlight.gameObject.SetActive(!Playerlighton);
            Playerlighton =!Playerlighton;
        }
    }
    public bool isPlayerlighton()
    {
        return Playerlighton;
    }
}

