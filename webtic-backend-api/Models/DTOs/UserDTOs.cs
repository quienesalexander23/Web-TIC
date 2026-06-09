namespace WebTIC.API.Models.DTOs
{
    public class UsuarioDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }

    public class CreateUsuarioDto
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class UpdateUsuarioDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
