using System.ComponentModel.DataAnnotations;

namespace WebsiteDemo.Repository.Validation
{
    public class FileExtensionMp3 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "mp3" };
                bool result = extensions.Any(x => extension.EndsWith(x));
                if (!result)
                {
                    return new ValidationResult("Allowed extensions MP3 File  are .mp3");
                }
            }
            return ValidationResult.Success;
        }
    }
}
