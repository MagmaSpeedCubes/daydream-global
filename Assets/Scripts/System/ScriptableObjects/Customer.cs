using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "Scriptable Objects/Customer")]
public class Customer : ScriptableObject
{
    public string customerName;
    public int budget;
    public string[] useCases = new string[0];
    public float[] useFrequency = new float[0];
    public float performanceExpectation;
    public float flexibility; //how flexible they are with budget and performance
    public float mood; //how likely they are to be satisfied with the build
    public float patience; //how long they are willing to wait for the build
    public BuiltPC finalBuild;

    public string ToString()
    {
        string output = "";
        output += "Name: " + customerName + ", ";
        output += "Budget: $" + budget + ", ";
        output += "\n---Use Cases--- ";
        for (int i = 0; i < useCases.Length; i++)
        {
            output += useCases[i] + ": " + useFrequency[i] + "% , ";
        }
        output += "Flexibility: " + flexibility + ", ";
        output += "Mood: " + mood + ", ";
        output += "Patience: " + patience + ".";

        return output;
    }

    public string ToFormattedString()
    {
        string output = "";
        output += "Name: " + customerName + "\n";
        output += "Budget: $" + budget + "\n";
        output += "---Use Cases--- \n";
        for (int i = 0; i < useCases.Length; i++)
        {
            output += useCases[i] + ": " + useFrequency[i] + "% \n";
        }
        output += "Flexibility: " + flexibility + "\n";
        output += "Mood: " + mood + "\n";
        output += "Patience: " + patience + "\n";

        return output;
    }

}
