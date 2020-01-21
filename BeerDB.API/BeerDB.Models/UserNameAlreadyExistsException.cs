using System;
using System.Collections.Generic;
using System.Text;

namespace BeerDB.Models
{
    public class UserNameAlreadyExistsException : Exception 
    {
        public UserNameAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
