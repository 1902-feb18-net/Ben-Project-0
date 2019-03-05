using System;
using System.Collections.Generic;
using System.Text;

namespace Project_0.Lib
{
    public class CustomerImp
    {
        public string FirstName
        {
            get => FirstName;
            set
            {
                Console.WriteLine(value);
                // "value" is the value passed to the setter.
                if (value.Length == 0)
                {
                    // good practice to provide useful messages when throwing exceptions,
                    // as well as the name of the relevant parameter if applicable.
                    throw new ArgumentException("First Name must not be empty.", nameof(value));
                }
                FirstName = value;
            }
        }

        public string LastName
        {
            get => LastName;
            set
            {
                // "value" is the value passed to the setter.
                if (value.Length == 0)
                {
                    // good practice to provide useful messages when throwing exceptions,
                    // as well as the name of the relevant parameter if applicable.
                    throw new ArgumentException("Last Name must not be empty.", nameof(value));
                }
                LastName = value;
            }
        }
        public StoreImp Default { get; set; }

        public DateTime OrderTime { get; set; }
        
        public int Id { get; set; }

        public int DefaultStoreId { get; set; }

        public bool CheckHours
        {
            get
            {
                if (OrderTime != null)
                {
                    DateTime now = DateTime.Now;
                    if ((now - OrderTime).TotalHours > 2)
                        return true;
                }
                return false;
            }
        }
    }
}
