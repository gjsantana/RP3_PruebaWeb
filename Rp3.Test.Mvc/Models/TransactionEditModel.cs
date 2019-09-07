using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Rp3.Test.Mvc.Models
{
    public class TransactionEditModel
    {
        [Display(Prompt ="Id", Name="Id")]
        public int TransactionId { get; set; }

        [Display(Prompt = "Tipo de transacción", Name = "Tipo de transacción")]
        public short TransactionTypeId { get; set; }

        [Required(ErrorMessage = "El campo categoría es obligatorio")]
        [Display(Prompt = "Categoría", Name = "Categoría")]
        public int CategoryId { get; set; }

        [DataType(DataType.Date)]
        [Display(Prompt = "Fecha de transacción", Name = "Fecha")]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode =true)]
        [CustomDateRange(ErrorMessage = "La fecha no debe ser menor a 30 días de la fecha actual")]
        public DateTime RegisterDate { get; set; }

        [Required(ErrorMessage ="El campo descripción es obligatorio")]
        [Display(Prompt = "Descripción", Name = "Descripción")]
        public string ShortDescription { get; set; }

        [Display(Prompt = "Nota", Name = "Nota")]
        public string Notes { get; set; }

        [DataType(DataType.Currency)] 
        [Display(Prompt = "Valor", Name = "Valor")]
        [Range(1, 10000, ErrorMessage ="El monto a registrar debe ser mayor a 0")]               
        public decimal Amount { get; set; }

        public SelectList CategorySelectList { get; set; }
        public SelectList TransactionTypeSelectList { get; set; }

        public class CustomDateRangeAttribute : RangeAttribute, IClientValidatable
        {
            public CustomDateRangeAttribute() : base(typeof(DateTime), DateTime.Now.AddDays(-30).ToShortDateString(), DateTime.Now.ToShortDateString())
            {

            }
            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata m, ControllerContext c)
            {
                var modelClientValidationRule = new ModelClientValidationRule
                {
                    ValidationType = "customdaterange",
                    ErrorMessage = ErrorMessage
                };
                modelClientValidationRule.ValidationParameters.Add("min", DateTime.Now.AddDays(-30).ToShortDateString());
                return new List<ModelClientValidationRule> { modelClientValidationRule };
            }
            public override bool IsValid(object value)
            {
                return true;
            }
        }
    }
}