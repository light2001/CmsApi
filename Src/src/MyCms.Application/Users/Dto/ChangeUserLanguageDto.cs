using System.ComponentModel.DataAnnotations;

namespace MyCms.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}