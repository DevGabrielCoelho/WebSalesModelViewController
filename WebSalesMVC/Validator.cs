using System.ComponentModel.DataAnnotations;

namespace WebSalesMVC
{
    public static class Validator
    {
        public static bool ValidateProperty(object obj, string propertyName)
        {
            if (obj == null)
            {
                return false;
            }

            var property = obj.GetType().GetProperty(propertyName);
            if (property == null)
            {
                return false;
            }

            if (propertyName == "Name")
            {
                string nameValue = (string)property.GetValue(obj);
                return !string.IsNullOrEmpty(nameValue) && nameValue.Length > 2;
            }

            if (propertyName == "Email")
            {
                string emailValue = (string)property.GetValue(obj);
                var a = new EmailAddressAttribute().IsValid(emailValue);
                return a;
            }

            if (propertyName == "BirthDate")
            {
                var birthDateValue = property.GetValue(obj);
                return birthDateValue is DateTime;
            }

            if (propertyName == "BaseSalary")
            {
                double baseSalaryValue = (double)property.GetValue(obj);
                return baseSalaryValue >= 100.00 && baseSalaryValue <= 50000.00;
            }

            return true;
        }
    }
}
