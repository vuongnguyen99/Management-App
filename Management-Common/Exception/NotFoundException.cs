namespace Management_Common.Exception
{
    public class NotFoundException: System.Exception
    {
        public NotFoundException()
        {

        }
        public NotFoundException(string message): base(message)
        {

        }
        public NotFoundException(string message, SystemException inner): base(message, inner)
        {

        }
    }
}
