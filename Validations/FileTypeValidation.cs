using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI_Udemy.Validations
{
    public class FileTypeValidation:ValidationAttribute
    {
        private readonly string[] ValidType;

        public FileTypeValidation(string[] type)
        {
            this.ValidType = type;
        }

        public FileTypeValidation(FileTypeGroup typeGroup)
        {
            if (typeGroup == FileTypeGroup.Imagen)
            {
                ValidType = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (!ValidType.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo de Archivo debe ser: {string.Join(',', ValidType)}");
            }

            return ValidationResult.Success;
        }
    }
}
