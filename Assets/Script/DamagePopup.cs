using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Pool;

public class DamagePopup : MonoBehaviour
{
    TextMeshPro textMeshPro;
    ObjectPool objectPool;
    Color textColor;
    Color baseTextColor;

    float moveSpeed = 5;
    float disappearSpeed = 2;
    float duration = 2;

    private void Awake()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        baseTextColor = textMeshPro.color;
    }


    void Start()
    {
        //textMeshPro = GetComponentInChildren<TextMeshPro>();
        objectPool = ObjectPool.Instance;
        textColor = textMeshPro.color;
        moveSpeed = 3;
        duration = 3;
    }

    private void Update()
    {
        transform.position += new Vector3 (0, 0 ,moveSpeed) * Time.deltaTime;
        if(duration > 0)
        {
            duration -= Time.deltaTime;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMeshPro.color = textColor;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        textColor = baseTextColor;
        textMeshPro.color = textColor;
        //moveSpeed = 3;
        duration = 3;
        //transform.rotation = 
        //StartCoroutine(DamagePopupDuration());
    }


    public void Setup(int damage)
    {
        //Debug.Log("SetUpDame");
        textMeshPro.text = damage.ToString();

        //textMeshPro.order = $"{damage} aaa";
    }

    IEnumerator DamagePopupDuration()
    {
        yield return new WaitForSecondsRealtime(3f);
        gameObject.SetActive(false);
        //objectPool.ReturnObjectToPool("DamagePopup", this.gameObject);
    }
}
