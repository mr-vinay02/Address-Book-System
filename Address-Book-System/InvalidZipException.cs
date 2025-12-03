
// uc 6 custom exception
namespace Address_Book_System
{
    
    internal class InvalidZipException : Exception
    {
       

        public InvalidZipException(string? message) : base(message)
        {
        }

       
    }
}