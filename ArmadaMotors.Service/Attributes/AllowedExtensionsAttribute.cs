using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaMotors.Service.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        private readonly bool _isArray;
        public AllowedExtensionsAttribute(string[] extensions, bool isArray = false)
        {
            _extensions = extensions;
            _isArray = isArray;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            if (_isArray)
            {
                var files = value as IEnumerable<IFormFile>;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        if (!_extensions.Contains(extension.ToLower()))
                        {
                            return new ValidationResult(GetErrorMessage());
                        }
                    }          
                }
            }
            else
            {
                var file = value as IFormFile;
                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (!_extensions.Contains(extension.ToLower()))
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This photo extension is not allowed!";
        }
    }
}
