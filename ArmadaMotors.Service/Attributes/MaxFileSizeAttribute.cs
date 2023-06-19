using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmadaMotors.Service.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        private readonly bool _isArray;
        public MaxFileSizeAttribute(int maxFileSize, bool isArray = false)
        {
            _maxFileSize = maxFileSize;
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
                        if (file.Length > _maxFileSize)
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
                    if (file.Length > _maxFileSize)
                    {
                        return new ValidationResult(GetErrorMessage());
                    }
                }
            }
            
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is {_maxFileSize} bytes.";
        }
    }
}
