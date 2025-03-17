using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

public class Calculating_Controller : MonoBehaviour
{
    private TextField showNumber; // Display area
    private string currentInput = ""; // Store user input

    void Start()
    {
        // Call InitializeUI at the start to set up the UI components
        InitializeUI();
    }

    void InitializeUI()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Check if the root is found
        if (root == null)
        {
            Debug.LogError("UIDocument rootVisualElement is null!");
            return;
        }

        // Find the TextField and set it to read-only
        showNumber = root.Q<TextField>("Show_number");
        if (showNumber == null)
        {
            Debug.LogError("Show_number TextField not found!");
            return;
        }
        showNumber.isReadOnly = true; // Prevent manual input

        // Register number buttons
        RegisterButton(root, "one", "1");
        RegisterButton(root, "two", "2");
        RegisterButton(root, "three", "3");
        RegisterButton(root, "four", "4");
        RegisterButton(root, "five", "5");
        RegisterButton(root, "six", "6");
        RegisterButton(root, "seven", "7");
        RegisterButton(root, "eight", "8");
        RegisterButton(root, "nine", "9");
        RegisterButton(root, "zero", "0");

        // Register operator buttons
        RegisterButton(root, "addition", " + ");
        RegisterButton(root, "minus", " - ");
        RegisterButton(root, "multiple", " * ");
        RegisterButton(root, "divided", " / ");

        // Register equal button
        var equalButton = root.Q<Button>("equal");
        if (equalButton != null)
        {
            equalButton.clicked += CalculateResult;
        }
        else
        {
            Debug.LogError("Equal button not found!");
        }

        // Register delete button
        var deleteButton = root.Q<Button>("delete");
        if (deleteButton != null)
        {
            deleteButton.clicked += ClearInput;
        }
        else
        {
            Debug.LogError("Delete button not found!");
        }
    }

    // Function to register number & operator buttons
    void RegisterButton(VisualElement root, string buttonName, string value)
    {
        var button = root.Q<Button>(buttonName);
        if (button != null)
        {
            button.clicked += () =>
            {
                Debug.Log($"Button {buttonName} clicked!"); // Debugging statement to check button press
                AddToInput(value);
            };
        }
        else
        {
            Debug.LogError($"Button '{buttonName}' not found!");
        }
    }

    // Add the pressed button's value to the current input
    void AddToInput(string value)
    {
        currentInput += value; // Append the value (number or operator) to the current input
        showNumber.value = currentInput; // Update the display with current input
    }

    // Calculate the result from the input string
    void CalculateResult()
    {
        try
        {
            // Use C#'s built-in method to evaluate the expression
            var result = new System.Data.DataTable().Compute(currentInput, null);
            currentInput = result.ToString(); // Set result as the current input
            showNumber.value = currentInput; // Update the display with result
        }
        catch (Exception e)
        {
            // If there's an error (e.g., invalid syntax), show an error message
            currentInput = "Error";
            showNumber.value = currentInput;
            Debug.LogError($"Calculation error: {e.Message}");
        }
    }

    // Clear the current input
    void ClearInput()
    {
        currentInput = ""; // Reset the input string
        showNumber.value = currentInput; // Clear the display
    }
}
