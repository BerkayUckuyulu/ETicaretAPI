using ETİcaretAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETİcaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Ürün adını boş geçmeyiniz.")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Lütfen Ürün adını 150 ile 5 karakter arasında giriniz");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Lütfen stok bilgisini doldurunuz.")
                .Must(s => s >= 0)
                .WithMessage("Stok bilgisi negatif olamaz");

            RuleFor(p => p.Price)
        .NotEmpty()
        .NotNull()
        .WithMessage("Lütfen fiyat bilgisini doldurunuz.")
        .Must(s => s >= 0)
        .WithMessage("Fiyat bilgisi negatif olamaz");
        }
    }
}
