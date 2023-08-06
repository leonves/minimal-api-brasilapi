namespace BrasilApiIntegration.Model
{
    public class ServiceResult
    {
        public bool IsSuccessful { get; private set; }
        public string ErrorMessage { get; private set; }

        private ServiceResult(bool isSuccessful, string errorMessage)
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }

        public static ServiceResult Success()
        {
            return new ServiceResult(true, null);
        }

        public static ServiceResult Error(string errorMessage)
        {
            return new ServiceResult(false, errorMessage);
        }
    }
}
