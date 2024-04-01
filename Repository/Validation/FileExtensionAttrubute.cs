using System.ComponentModel.DataAnnotations;

namespace WebsiteDemo.Repository.Validation
{
    public class FileExtension: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            { 
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "png", "jpeg", "webp" };
                bool result = extensions.Any(x => extension.EndsWith(x));
                if (!result)
                {
                    return new ValidationResult("Allowed extensions are jpg ,png , jpeg or webp");
                }
            }
            return ValidationResult.Success;
        }
        
            
    }
}
