namespace Gate.Application.DTOs.Response
{
    public class UserLoginResponse
    {
        public bool Success => Errors?.Count() == 0;

        public string? AccessToken { get; set; }
        
        public DateTime ExpirationDate { get; set; }

        public List<string>? Errors { get; private set; }

        public UserLoginResponse() => Errors = new List<string>();

        public void AddError(string error) => Errors?.Add(error);

        public void AddMultipleErrors(IEnumerable<string> errors) => Errors?.AddRange(errors);
    }
}