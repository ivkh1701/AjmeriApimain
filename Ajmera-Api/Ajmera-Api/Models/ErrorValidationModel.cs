namespace Ajmera_Api.Models
{
    public class ErrorValidationModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class ErrorModel
    {
        public string? FieldName { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class ErrorResponse
    {
        public IList<ErrorModel> Errors = new List<ErrorModel>();
    }
}
