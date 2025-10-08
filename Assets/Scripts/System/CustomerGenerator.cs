using UnityEngine;
public class CustomerGenerator : MonoBehaviour
{
    private string[] customerNames;
    public static Customer generateCustomer()
    {
        Customer c = new Customer();


        c.customerName = "";
        return c;

        

    }
}
