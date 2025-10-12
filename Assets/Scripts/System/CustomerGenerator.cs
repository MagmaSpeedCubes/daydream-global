using UnityEngine;
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
            Destory(this);
        }
    }

    public static Customer generateCustomer()
    {
        return instance.generateCustomer();
    }

    public Customer generateCustomer()
    {
        Customer c = new Customer();
        int specialCustomer = Random.Range(0, 50);
        if (specialCustomer == 0)
        {
            int randIndex = Random.Range(0, specialCustomers.Length);
            c = specialCustomers[randIndex];

        }
        else
        {
            int randIndex = Random.Range(0, customerNames.Length);
            c.customerName = customerNames[randIndex];

            c.budget = Math.Pow(Random.Range(30, 100), 2) * 0.5;
            for (int i = 1; i < useCases.Length; i++)
            {
                int included = Random.Range(1, useCases.Length);
                if (i >= included)
                {
                    
                }
            }
            
            //             public int budget;
            // public string[] useCases;
            // public float[] useFrequency;
            // public float performanceExpectation;
            // public float flexibility; //how flexible they are with budget and performance
            // public float mood; //how likely they are to be satisfied with the build
            // public float patience; //how long they are willing to wait for the build
            // public string[] prefferedBrands; //brand loyalty
            // public string[] dislikedBrands; //brand aversionc.customerName = customerNames[randIndex];

        }


        return c;



        

    }
}
