using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GenericBar : MonoBehaviour, IObserverGenericBar
{
    private float _maxValue;

    [SerializeField]
    protected GameObject _entityForValidate;
    private IObservableToGenericBar _entity;
    [SerializeField]
    private Image _bar;
    private void OnValidate()
    {
        /*
        _entity =_entityForValidate.GetComponent<IObservableToGenericBar>();
        if (_entityForValidate !=null)
        {
            _entity.Suscribe(this);
        }
        else
        {
            Debug.LogError("Error fatal, no se asigno entidad a esta barra");
            return;
        }
        */
        /*
        if(entity!=null)
        SetTarget(entity.GetComponent<IObservableToGenericBar>());
        */

    }

    protected virtual void Start()
    {
     
        _bar = GetComponent<Image>();
        if (_bar == null)
        {
            Debug.LogError("Error fatal GenericBar no tiene el componente Image");
        }
    }
    protected void SetTarget(IObservableToGenericBar target)
    {
        _entity = target;
        _entity.Suscribe(this);
    }
    public void RefreshValue(float value)
    {
        _bar.fillAmount = value / _maxValue;
    }

    public void SetMaxValue(float value)
    {
        _maxValue = value;
    }

    public void NotifyBarIsEmpty(bool barIsEmpty)
    {
        gameObject.transform.parent.gameObject.SetActive(barIsEmpty);   
    }
}
