namespace Management.Common.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.") { }
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
