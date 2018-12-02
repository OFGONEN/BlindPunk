/*
Created By OFGONEN
*/
using UnityEngine;

public class AnimationPicker : MonoBehaviour {
	
	#region Variables
    public enum AnimationPick { horizontal_long , horizontal_mid , horizontal_short, vertical_long  , vertical_mid , vertical_short};
    public Animator anim;
    public AnimationPick pick;
    #endregion


    void Start () 
	{
        switch ((int)pick)
        {
            case 0: anim.SetTrigger("horizontal_long"); break;
            case 1: anim.SetTrigger("horizontal_mid"); break;
            case 2: anim.SetTrigger("horizontal_short"); break;
            case 3: anim.SetTrigger("vertical_long"); break;
            case 4: anim.SetTrigger("vertical_mid"); break;
            case 5: anim.SetTrigger("vertical_short"); break;
        }
	}
	
	

    #region Methods
    #endregion
}
