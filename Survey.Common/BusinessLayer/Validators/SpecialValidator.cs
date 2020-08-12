
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Survey.Common.BusinessLayer.Validators
{
    public class SpecialValidator : Attribute, IValidatorAttribute
    {

        private SpecialValidators Type { get; set; }
        private delegate Boolean DoValidation(String val);

        private Dictionary<SpecialValidators,DoValidation> Validations { get; set; }
        public SpecialValidator(SpecialValidators specialValidator)
        {
            this.makeValidators();
            this.Type = specialValidator;
        }
        private void makeValidators()
        {
            this.Validations = new Dictionary<SpecialValidators, DoValidation>();
            this.Validations.Add(SpecialValidators.Email,this.EmailValidation);
            this.Validations.Add(SpecialValidators.Mobile, this.MobileValidation);
            this.Validations.Add(SpecialValidators.NationalCode, this.NationalCodeValiation);
            this.Validations.Add(SpecialValidators.Password, this.PasswordValidation);
            this.Validations.Add(SpecialValidators.UserName, this.UserNameValidation);
        }
        public Boolean IsValid(object val)
        {
            return this.Validations[this.Type]( val != null ? val.ToString() : "");
        }

        private bool EmailValidation(string value)
        {
            if (String.IsNullOrEmpty(value))
                return true;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(value);
            if (match.Success)
                return true;
            else
                return false;
        }

        private bool MobileValidation(string value)
        {
            if (String.IsNullOrEmpty(value))
                return true;
            if (value.Length != 11)
                throw new Exception("لطفا شماره موبایل 11 رقمی وارد نمایید");
            if (value.Length == 11 && !value.StartsWith("09"))
                throw new Exception("توجه نمایید شماره موبایل با 09 شروع شود.");
            return true;
        }

        private bool NationalCodeValiation(string nationalCode)
        {
            if (String.IsNullOrEmpty(nationalCode))
                throw new Exception("لطفا کد ملی را صحیح وارد نمایید");


            //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
            if (nationalCode.Length != 10)
                throw new Exception("طول کد ملی باید ده کاراکتر باشد");

            //در صورتی که کد ملی ده رقم عددی نباشد
            var regex = new Regex(@"\d{10}");
            if (!regex.IsMatch(nationalCode))
                throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");

            //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalCode)) return false;


            //عملیات شرح داده شده در بالا
            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }

        private bool UserNameValidation(string value)
        {
            return true; ;
        }

        private bool PasswordValidation(string value)
        {
            return true;
        }

    }
    public enum SpecialValidators
    {
        NationalCode,
        Email,
        Mobile,
        UserName,
        Password,
    }
}
