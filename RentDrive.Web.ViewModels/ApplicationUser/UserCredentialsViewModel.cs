namespace RentDrive.Web.ViewModels.ApplicationUser
{
    public class UserCredentialsViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateTime MemberSince { get; set; }
        public bool IsCompanyEmployee { get; set; }
        
    }
}
