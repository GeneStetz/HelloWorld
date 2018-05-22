namespace HelloWorldApi.Models
{
    public interface IMessage
    {
        /// <summary>
        /// We'll start by assuming that messages can be indexed or identified by some Id value, 
        /// although in the future this could change to a list of multiple parameters for identifying them (all depends upon future requirements).
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// This is the "raw" message.  We'll let the calling program decide how, if any, formatting should be done to this.
        /// </summary>
        string RawMessage { get; set; }
    }
}