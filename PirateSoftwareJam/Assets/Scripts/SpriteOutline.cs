using UnityEngine;

[ExecuteInEditMode]
public class SpriteOutline : MonoBehaviour {
	public Color color = Color.cyan;

	[SerializeField]
	private SpriteRenderer _spriteRenderer;
	private MaterialPropertyBlock mpb;

	public SpriteRenderer spriteRenderer{
		get{
			if(_spriteRenderer == null){
				_spriteRenderer = GetComponent<SpriteRenderer>();
			}
			return _spriteRenderer;
		}
	}
	[SerializeField]
	private float _outlineSize = 0;

	private Material _preMat;

	void OnEnable() {
		_preMat = spriteRenderer.sharedMaterial;
		spriteRenderer.sharedMaterial = defaultMaterial;
		UpdateOutline(_outlineSize);
	}

	void OnDisable() {
		spriteRenderer.sharedMaterial = _preMat;
	}

	public void UpdateOutline(float outline) {
		if (mpb == null)
		{
			mpb = new MaterialPropertyBlock();
		}
		spriteRenderer.GetPropertyBlock(mpb);
		mpb.SetFloat("_OutlineSize", outline);
		mpb.SetColor("_OutlineColor", color);
		spriteRenderer.SetPropertyBlock(mpb);
	}

	void OnValidate(){
		if(enabled){
			UpdateOutline(_outlineSize);
		}
	}


	private static Material _defaultMaterial = null;
	public static Material defaultMaterial{
		get{
			if(_defaultMaterial == null){
				_defaultMaterial = Resources.Load<Material>("SpriteOutline");
			}
			return _defaultMaterial;
		}
	}
}