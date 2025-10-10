using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_View : MonoBehaviour
{
    [SerializeField] private MeshRenderer iconRenderer;
    [SerializeField] private Transform viewTransform;
    [SerializeField] private float transitionSpeed = 10f;
    [SerializeField] private Vector3 revealedRotation = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 hiddenRotation = new Vector3(0, 0, 180);
    [SerializeField] private float hideSpeed = 5f;
    
    private ICard_MaterialCreator _materialCreator;
    
    private Card _card;
    private Vector3 _targetRotation;
    private Transform _cachedTransform;

    void Start()
    {
        _cachedTransform = transform;
    }
    void OnEnable()
    {
        _materialCreator = DI.Get<ICard_MaterialCreator>();
        _targetRotation = revealedRotation;
    }
    
    public void SetCard(Card card)
    {
        _card = card;
        iconRenderer.material = _materialCreator.GetMaterial(card);
    }

    void Update()
    {
        _cachedTransform.localPosition = Vector3.Lerp(_cachedTransform.localPosition, Vector3.zero, Time.deltaTime * transitionSpeed);
        _cachedTransform.localRotation = Quaternion.Slerp(_cachedTransform.localRotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * transitionSpeed);
        
        viewTransform.localRotation = Quaternion.Slerp(viewTransform.localRotation, Quaternion.Euler(_targetRotation), Time.deltaTime * transitionSpeed);
    }

    public void HideCard()
    {
        _targetRotation = hiddenRotation;
    }
    public void ShowCard()
    {
        _targetRotation = revealedRotation;
    }


    public Card GetCard()
    {
        return _card;
    }
}

