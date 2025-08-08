using System.Collections;
using UnityEngine;

public class Stage8_Clear : StageClearBase
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
        anim.SetTrigger("IsDumbbell"); // 
        yield return null;
    }

    protected override IEnumerator Fail()
    {
        


        yield return null;
    }
}
