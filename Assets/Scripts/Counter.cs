/*
Created By OFGONEN
*/
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {

    #region Variables
    public static Counter instance;
    public Text _text_counter;

    public float _time_round;

    public float _time_star_3;
    public float _time_star_2;
    public float _time_star_1;
    public float _time_star_0;

    public float _counter;

    private bool _can_counter;

    #endregion

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start () 
	{
        _counter = _time_round;
        _text_counter.text = "" + (int)_counter;
        
    }
	
	void Update () 
	{
        if (_can_counter)
        {
            _counter -= Time.deltaTime;
            _text_counter.text = "" + (int)_counter;
        }

        if (_counter <= 0.05f)
        {
            _can_counter = false;
            //Gamelogic EndGame;
        }
	}

    #region Methods

    public void TooglePause(int pause)
    {
        if(pause == 1)
        {
            _can_counter = true;
        }
        else
        {
            _can_counter = false;
        }

    }
    #endregion
}
