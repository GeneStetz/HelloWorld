namespace HelloWorldApi.Models
{
    public class StandardMessage : IMessage
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public string RawMessage { get; set; }
    }
}