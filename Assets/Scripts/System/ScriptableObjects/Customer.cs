using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "Scriptable Objects/Customer")]
public class Customer : ScriptableObject
{
    public string customerName;
    public int budget;
    public string[] useCases;
    public float[] useFrequency;
    public float performanceExpectation;
    public float flexibility; //how flexible they are with budget and performance
    public float mood; //how likely they are to be satisfied with the build
    public float patience; //how long they are willing to wait for the build
    public string[] prefferedBrands; //brand loyalty
    public string[] dislikedBrands; //brand aversion
    
    public BuiltPC finalBuild;

}
