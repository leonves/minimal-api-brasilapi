using BrasilApiIntegration.Data.Entities.Core;

namespace BrasilApiIntegration.Data.Entities
{
    public class Log : Entity
    {
        public string Message { get; set; }

        public Log(string Message)
        {
            this.Message = Message;
        }
    }
}
