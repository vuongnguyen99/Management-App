namespace Management_Common.Exception
{
    public class ValidationException : System.Exception
    {
        public ValidationException()
        {

        }
        public ValidationException(string message) : base(message)
        {

        }
        public ValidationException(string message, SystemException inner) : base(message, inner)
        {

        }
    }
}
