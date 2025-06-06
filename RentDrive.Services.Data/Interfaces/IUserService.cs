namespace RentDrive.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<string> GetOwnerNameById(Guid? id);
    }
}
