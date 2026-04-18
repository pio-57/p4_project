using FluentValidation;

public class KlientValidator : AbstractValidator<Klient>
{
    public KlientValidator()
    {
        RuleFor(x => x.Imie)
            .Matches(@"^[A-ZĄĆĘŁŃÓŚŻŹ][a-ząćęłńóśżź]{2,}$")
            .WithMessage("Niepoprawne imię");

        RuleFor(x => x.Nazwisko)
            .Matches(@"^[A-ZĄĆĘŁŃÓŚŻŹ][a-ząćęłńóśżź]{2,}$")
            .WithMessage("Niepoprawne nazwisko");

        RuleFor(x => x.Telefon)
            .Matches(@"^[0-9]{9}$")
            .WithMessage("Telefon musi mieć 9 cyfr");

        RuleFor(x => x.Email)
            .Matches(@"^[a-zA-Z0-9]+[@][a-zA-Z0-9]+[.][a-zA-Z]{1,3}$")
            .WithMessage("Niepoprawny email");
    }
}