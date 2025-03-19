using UnityEngine;
using UnityEngine.UI;

public class TensionController : MonoBehaviour
{
    public Pile pile; // Reference to the Pile instance
    public Slider tensionSlider; // Reference to the Slider component

    private void Start()
    {
        // Initialize the slider's value to the current tension
        tensionSlider.value = (float)pile.GetTension();

        // Add a listener to the slider to call the OnTensionChanged method when the value changes
        tensionSlider.onValueChanged.AddListener(OnTensionChanged);
    }

    private void OnTensionChanged(float newValue)
    {
        // Update the tension in the Pile instance
        pile.SetTension(newValue);
    }
}