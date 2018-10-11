using System;
using System.ComponentModel.DataAnnotations;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public class PasswordValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;
            if (user.UserId == null && user.Password != null)
            {
                return ValidationResult.Success;
            }
            else if (user.UserId != null && user.Password == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("密码不能为空");
        }
    }
}