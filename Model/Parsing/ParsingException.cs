namespace Model.Parsing
{
    public class ParsingException : Exception
    {
        public IEnumerable<ParsingErrorPart> ErrorParts { get; private set; }

        public ParsingException(IEnumerable<ParsingErrorPart> errorParts) =>
            ErrorParts = errorParts;
    }
}
