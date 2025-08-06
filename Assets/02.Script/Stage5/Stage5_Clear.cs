using System.Collections;
using UnityEngine;

public class Stage5_Clear : StageClearBase
{
    public GameObject blueObject;
    public Animator anim;

    protected override void OnEnable()
    {
        
        anim = blueObject.GetComponent<Animator>(); // 
        //anim.Play("MushRoom_Idle", 0, 0);
        base.OnEnable();
    }

    protected override IEnumerator Clear()
    {
        Debug.Log("¼º°ø");
        yield return null;
    }

    protected override IEnumerator Fail()
    {
        anim.SetTrigger("IsShoesDown"); // 


        yield return null;
    }
}
