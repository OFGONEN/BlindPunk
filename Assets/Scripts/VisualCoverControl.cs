using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCoverControl : MonoBehaviour {

    public static VisualCoverControl instance = null;

	[Range(0,1)]
	public float percent;
	public Vector2 radius;
	public Vector2 noiseSpeed;
	public Vector2 noiseFrequency;
	public Vector2 noiseMultipler;
	public Vector2 noiseOffset;
	public Vector2 gradientOffset;

    public float sign; // Determines the increment or disincrement of visual cover effect. If sign is 1 effect increces.

	public Material mat;
    public GameObject player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        //We start with sign -1 and percent 0 which means 0 visual effect at the start.When ever sign is 1 visual effect increces over time and reaches it fullest in 1 second.
        //If sign is -1 visual effect decreaases over time and fully wears off at 0.
        percent += sign * Time.deltaTime;
        percent = Mathf.Clamp(percent, 0f, 1f); // Keeps the effect between 0 and 1 no matter what.
    }

    public void ChangeSign()
    {
        sign *= -1;
    }

    private void OnRenderImage( RenderTexture source, RenderTexture destination )
	{
		Vector2 k = Camera.current.WorldToScreenPoint( player.transform.position );
		mat.SetVector( "_PlayerPos", new Vector4(k.x, k.y,  Camera.current.pixelWidth , Camera.current.pixelHeight));
		mat.SetFloat( "_NoiseFreq", getMappedValue(percent, noiseFrequency));
		mat.SetFloat( "_NoiseSpeed", getMappedValue( percent, noiseSpeed ) );
		mat.SetFloat( "_NoiseMult", getMappedValue( percent, noiseMultipler ) );
		mat.SetFloat( "_PixelOffset", getMappedValue( percent,noiseOffset ) );
		mat.SetFloat( "_GradientOffset", getMappedValue( percent, gradientOffset) );
		mat.SetFloat( "_Radius", getMappedValue( percent, radius ) );


		Graphics.Blit( source, destination, mat );
	}


	float getMappedValue(float val, Vector2 toConvert)
	{
		return ( val * ( toConvert.y - toConvert.x ) ) + toConvert.x;

	}


}
