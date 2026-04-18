using FluentValidation;

public class RowerValidator : AbstractValidator<Rower>
{
    public RowerValidator()
    {
        RuleFor(x => x.Model)
            .Matches(@"^[A-ZĄĆĘŁŃÓŚŻŹ][a-ząćęłńóśżź]{2,}$")
            .WithMessage("Niepoprawny model");

        RuleFor(x => x.Marka)
            .Matches(@"^[A-ZĄĆĘŁŃÓŚŻŹ][a-ząćęłńóśżź]{2,}$")
            .WithMessage("Niepoprawna marka");

        RuleFor(x => x.Typ)
            .Matches(@"^[A-ZĄĆĘŁŃÓŚŻŹ][a-ząćęłńóśżź]{2,}$")
            .WithMessage("Niepoprawny typ roweru");

        RuleFor(x => x.CenaZaDzien)
            .GreaterThan(10)
            .WithMessage("Cena musi być większa od 10");
    }
}