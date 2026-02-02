namespace MyRecipeBook.Exception.MyRecipebookException
{
    public class ErrorOnValidationException : MyRecipebookException
    {
        public IList<string> ErrorMessages { get; }

        public ErrorOnValidationException(IList<string> errors)
        {
            ErrorMessages = errors;
        }
    }
}
