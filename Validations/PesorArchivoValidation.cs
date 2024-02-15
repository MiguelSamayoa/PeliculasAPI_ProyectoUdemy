using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI_Udemy.Validations
{
    public class PesorArchivoValidation : ValidationAttribute
    {
        private readonly int MaxPesoMB;

        public PesorArchivoValidation(int PesoValidationMB)
        {
            MaxPesoMB = PesoValidationMB;
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

            if (formFile.Length > MaxPesoMB * 1024 * 1024)
            {
                return new ValidationResult($"El peso de la imagen no debe ser mayor a {MaxPesoMB}MB");
            }

            return ValidationResult.Success;
        }
    }
}
