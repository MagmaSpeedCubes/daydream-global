using UnityEngine;
using System.Linq;
using System;
public class CustomerGenerator : MonoBehaviour
{
    public static CustomerGenerator instance;
    protected string[] customerNames = new string[] {
    "James", "Mary", "Michael", "Patricia", "John", "Jennifer", "Robert", "Linda", "David", "Elizabeth",
    "William", "Barbara", "Richard", "Susan", "Joseph", "Jessica", "Thomas", "Karen", "Christopher", "Sarah",
    "Charles", "Lisa", "Daniel", "Nancy", "Matthew", "Sandra", "Anthony", "Ashley", "Mark", "Emily",
    "Steven", "Kimberly", "Donald", "Betty", "Andrew", "Margaret", "Joshua", "Donna", "Paul", "Michelle",
    "Kenneth", "Carol", "Kevin", "Amanda", "Brian", "Melissa", "Timothy", "Deborah", "Ronald", "Stephanie",
    "Jason", "Rebecca", "George", "Sharon", "Edward", "Laura", "Jeffrey", "Cynthia", "Ryan", "Amy",
    "Jacob", "Kathleen", "Nicholas", "Angela", "Gary", "Dorothy", "Eric", "Shirley", "Jonathan", "Emma",
    "Stephen", "Brenda", "Larry", "Nicole", "Justin", "Pamela", "Benjamin", "Samantha", "Scott", "Anna",
    "Brandon", "Katherine", "Samuel", "Christine", "Gregory", "Debra", "Alexander", "Rachel", "Patrick", "Olivia",
    "Frank", "Carolyn", "Jack", "Maria", "Raymond", "Janet", "Dennis", "Heather", "Tyler", "Diane",
    "Aaron", "Catherine", "Jerry", "Julie", "Jose", "Victoria", "Nathan", "Helen", "Adam", "Joyce"
    
    };

    protected string[] useCases = new string[] {
        "", "Physics Simulation", "AI/ML", "Music Production", "3D Modeling & Rendering", "Video Editing", "Software Compiling","Photo Editing","Gaming", "Web Browsing"  
    };

    [SerializeField] protected Customer[] specialCustomers;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of CustomerGenerator detected. Destroying duplicate.");
            Destroy(this);
        }
    }

    public static Customer GenerateCustomerStatic()
    {
        return instance.GenerateCustomer();
    }

    public Customer GenerateCustomer()
    {
        Customer c = new Customer();
        System.Random r = new System.Random();
        int specialCustomer = r.Next(0, 50);
        if (specialCustomer == 0)
        {
            int randIndex = r.Next(0, specialCustomers.Length);
            c = specialCustomers[randIndex];

        }
        else
        {
            int randIndex = r.Next(0, customerNames.Length);
            c.customerName = customerNames[randIndex];

            double t = Math.Pow(r.NextDouble(), 2.5);
            c.budget = (int)(600 + t * (6000 - 600));

            float usePercentage = 100;
            for (int i = useCases.Length-1; i >=0; i--)
            {
                int included = r.Next(1, useCases.Length);
                if (i >= included)
                {
                    System.Array.Resize(ref c.useCases, c.useCases.Length + 1);
                    System.Array.Resize(ref c.useFrequency, c.useFrequency.Length + 1);
                    c.useCases[c.useCases.Length - 1] = useCases[i];


                    c.useFrequency[c.useFrequency.Length - 1] = r.Next(0, (int)usePercentage);
                    usePercentage -= c.useFrequency[c.useFrequency.Length - 1];
                }
                //can be less than 100, some customers dont use their pc very often

                c.flexibility = (int)Math.Sqrt(r.NextDouble() * 10000);
                c.mood = (int)Math.Sqrt(r.NextDouble() * 10000);
                c.patience = (int)Math.Sqrt(r.NextDouble() * 10000);

            }
        }
        return c;
    }
}
