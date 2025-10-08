public class SponsorDto
{
    public int Id { get; set; }
    public string OrganizationName { get; set; }
    public string OrganizationType { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }

    // UI fields
    public string ContactPerson { get; set; }
    public string Address { get; set; }
    public string Budget { get; set; }
    public string StudentCriteria { get; set; }
    public string StudyLevels { get; set; }

    // Only for insert
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public int? RoleId { get; set; }
    public string? CreatedBy { get; set; }

    // For update
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
