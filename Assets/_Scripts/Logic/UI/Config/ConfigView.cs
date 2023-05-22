using TMPro;
using UnityEngine;

namespace Logic.UI.ConfigUI
{
    public class ConfigView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField addressInputField;
        [SerializeField] private TMP_InputField portInputField;

        public void SetAddress(string address) => 
            addressInputField.text = address;

        public void SetPort(string port) =>
            portInputField.text = port;
    }
}