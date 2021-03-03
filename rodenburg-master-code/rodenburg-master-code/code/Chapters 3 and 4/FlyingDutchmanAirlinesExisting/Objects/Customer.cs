namespace FlyingDutchmanAirlinesExisting.Objects
{
    public class Customer
    {
        public int CustomerID;
        public string Name;
        public string SSN;

        public Customer(int customerID, string name, string ssn)
        {
            CustomerID = customerID;
            Name = name;
            SSN = ssn;
        }
    }
}


