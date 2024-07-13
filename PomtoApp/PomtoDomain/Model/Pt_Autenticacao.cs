using PomtoDomain.ShareData;

namespace PomtoDomain.Model
{
    public class Pt_Autenticacao : ModelBase
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
