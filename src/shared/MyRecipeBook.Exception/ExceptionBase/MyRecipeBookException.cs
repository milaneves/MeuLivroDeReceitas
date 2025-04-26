namespace MyRecipeBook.Exception.ExceptionBase
{
    public class MyRecipeBookException : SystemException
    {
        public MyRecipeBookException(string message) : base(message) { }
    }
}
