/*
Created By OFGONEN
*/
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour {

    #region Variables
    #endregion



    #region Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
    #endregion
}
