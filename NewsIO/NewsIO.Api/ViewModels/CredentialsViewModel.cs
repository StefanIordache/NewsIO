namespace NewsIO.Api.ViewModels
{
    //[Validation(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
