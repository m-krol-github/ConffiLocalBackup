using Conffi.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Conffi.UI
{
    public sealed class UIRoot : BaseView
    {
        [SerializeField] private Button create4ElementsBtn;
        [SerializeField] private Button create3ElementsBtn;
        [SerializeField] private Button create2ElementsBtn;
        [SerializeField] private Button clearElementsBtn;
        
        [SerializeField] private SofaPresenter _sofaPresenter;

        [SerializeField] private JsonPresetLoader _presetLoader2Segments;
        [SerializeField] private JsonPresetLoader _presetLoader3Segments;
        [SerializeField] private JsonPresetLoader _presetLoader4Segments;
        
        
        private void Awake()
        {
            create2ElementsBtn.onClick.AddListener(() =>
            {
                _sofaPresenter.ActivatePreset(_presetLoader2Segments.Preset);
            });
            
            create3ElementsBtn.onClick.AddListener(() =>
            {
                _sofaPresenter.ActivatePreset(_presetLoader3Segments.Preset);
            });
            
            create4ElementsBtn.onClick.AddListener(() =>
            {
                _sofaPresenter.ActivatePreset(_presetLoader4Segments.Preset);
            });

            clearElementsBtn.onClick.AddListener(() =>
            {
                _sofaPresenter.Clear();
            });
        }
    }
}