using UnityEngine;

public class FloorMove : MonoBehaviour
{
    private Renderer _renderer = default;
    [SerializeField, Header("�ǂꂭ�炢�̑��x�ňړ������邩")]
    private float _floorMoveSpeed = 0.1f;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        float offset = _floorMoveSpeed * Time.time * -1;
        _renderer.material.SetTextureOffset("_MainTex", new Vector2(0,offset));
    }

    public void UpdateMoveSpeed(float value)
    {
        _floorMoveSpeed += value;
    }
}
