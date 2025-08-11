using System.Collections;
using UnityEngine;

public class Stage5_Clear : StageClearBase
{
    public GameObject blueObject;
    private Animator anim;

    protected override void OnEnable()
    {
        
        anim = blueObject.GetComponent<Animator>(); // 
        //anim.Play("MushRoom_Idle", 0, 0);
        base.OnEnable();
    }

    protected override IEnumerator Clear()
    {
        yield return null;
        SoundManager.Instance.Play("Clear_1_1");

    }

    protected override IEnumerator Fail()

    {
        yield return null;
        anim.SetTrigger("IsShoesDown"); // 

    }
}
