using EncomposApi.Types.Optional;

namespace EncomposApi.Models;

public class CustomerModel
{
    public Optional<string> Id { get; set; }
    public Optional<string> FirstName { get; set; }
    public Optional<string> LastName { get; set; }
    public Optional<string> Phone { get; set; }
    public Optional<string> Email { get; set; }
    public Optional<string> AltId { get; set; }
    public Optional<long?> GroupId { get; set; }
    public Optional<bool> CanText { get; set; }
    public Optional<bool> HasCardOnFile { get; set; }
    public Optional<string[]> OtherEmails { get; set; }

    public void Normalize() 
    {
        Email = Email
            .Map(value => value?.Trim())
            .NotNullOrEmpty();

        FirstName = FirstName
            .Map(value => value?.Trim().MaxLength(30))
            .NotNullOrEmpty();

        LastName = LastName
            .Map(value => value?.Trim().MaxLength(30))
            .NotNullOrEmpty();

        Phone = Phone
            .Map(value => value?.Trim().NormalizePhone().MaxLength(25))
            .NotNullOrEmpty();

        OtherEmails = OtherEmails.NotNull();
    }
}
