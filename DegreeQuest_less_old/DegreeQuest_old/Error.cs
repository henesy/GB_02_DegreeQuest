using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeQuest
{
    /* Intended to be a form of more advanced error-handling format to provide a string and the exception */
    class Error
    {
        string message;
        Exception e;

        public Error(string s)
        {
            message = s;
            e = null;
        }
        
        public Error(String s, Exception ex)
        {
            message = s;
            e = ex;
        }

        public Error()
        {
            message = null;
            e = null;
        }

        public void setException(Exception ex)
        {
            e = ex;
        }

        public void setString(string s)
        {
            message = s;
        }

        public bool NoError()
        {
            if (message == null && e == null)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return message + "\n" + e.InnerException.ToString();
        }
    }
}
