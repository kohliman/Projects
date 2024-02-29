namespace classandstruct
{
 public class Account {
        /*private double _balance { get => _balance; set => _balance = value; }*/
        //so this is assigneing the value of balance to itself and will occupy a llot of space in it so to remove this error we need to use the following code
        private double _balance; // This is a private field to store the actual balance value

        public double Balance
        {
            get { return _balance; } // This is a public property to access the balance value
            private set { _balance = value; } // This is a private setter to allow modification only within the class
        }
        //also make sure atleast one modifier of the accesor should match to that of the property modifer
        static void Main(string[] args)
        {
        Account account = new Account();
            Console.WriteLine(account.Balance);
            account.Deposit(account.Balance);//still 0
            account.Deposit(23.3);
            Console.WriteLine(account.Balance);

        }
        public void Deposit(double balance) {
            if (balance >= 0)

            {
               _balance += balance;
                Console.WriteLine("You deposited a amount of" + balance);
            }
            else {
                Console.WriteLine("Please enter the valid amount");
            }

        }
        public void Withdraw(double with) {
            if (with <= _balance)
            {
                if (with >= 0)
                {
                    _balance -= with;

                    Console.WriteLine("You Withdrawn a amount of" + with);
                }
                else
                {
                    Console.WriteLine("Please enter the valid amount");
                }
            }


        }

        public Account() {
            _balance = 0;        
        }

        public Account(double balance)
        {
            _balance = balance;
        }

    }
}