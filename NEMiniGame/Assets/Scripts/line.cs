using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    public GameObject sphere;
    public PlayerControl Player;
    public LineRenderer linerender;
    public float line_length;
    public Vector3 p0, p1, p2;
    private Vector3 curdir;
    // Start is called before the first frame update
    void Start()
    {
        sphere = GameObject.FindGameObjectWithTag("Player");
        if (sphere != null)
            Player = sphere.GetComponent<PlayerControl>();
        linerender = GetComponent<LineRenderer>();
        linerender.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.show_line) linerender.enabled = false;
        else linerender.enabled = true;

        p0 = sphere.transform.position;
        p0.y -= (float)0.5;
        curdir = Player.dir;
        //Debug.Log(curdir);
        linerender.SetPosition(0, p0);


        RaycastHit hit;
        if (Physics.Raycast(p0, curdir, out hit, line_length, 1 << LayerMask.NameToLayer("wall")))
        {

            linerender.positionCount = 3;
            Vector3 rdir = Vector3.Reflect(curdir, hit.normal);
            rdir.y = 0;
            float uselength = Vector3.Distance(hit.point, p0);
            p1 = p0 + uselength * curdir;
            p2 = hit.point + rdir * (line_length - uselength);
            linerender.SetPosition(1, p1);
            linerender.SetPosition(2, p2);
        }
        else
        {
            linerender.positionCount = 2;
            p1 = p0 + line_length * curdir;
            linerender.SetPosition(1, p1);

        }


    }
}
