using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : VisualElement
{
    public int width { get; set; }
    public int height { get; set; }

    private VisualElement hbParent;
    private VisualElement hbBackground;
    private VisualElement hbForeground;

    public new class UxmlFactory : UxmlFactory<HealthBar, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlIntAttributeDescription m_width = new UxmlIntAttributeDescription() { name = "width", defaultValue = 300 };
        UxmlIntAttributeDescription m_height = new UxmlIntAttributeDescription() { name = "height", defaultValue = 50 };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as HealthBar;
            ate.width = m_width.GetValueFromBag(bag, cc);
            ate.height = m_height.GetValueFromBag(bag, cc);

            ate.Clear();
            VisualTreeAsset vt = Resources.Load<VisualTreeAsset>("Assets/10.UI/Stage/HealthBar.cs");
            VisualElement healthBar = vt.Instantiate();

            ate.hbParent = healthBar.Q<VisualElement>("healthbar");
            ate.hbBackground = healthBar.Q<VisualElement>("background");
            ate.hbForeground = healthBar.Q<VisualElement>("foregorund");
            ate.Add(healthBar);

            ate.hbParent.style.width = ate.width;
            ate.hbParent.style.height = ate.height;
            ate.style.width = ate.width;
            ate.style.height = ate.height;
        }
    }
}
