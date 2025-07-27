using System.Text.RegularExpressions;

namespace ClientRegistration.Behaviors
{
    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (sender is Entry entry)
            {
                // Se o texto estiver vazio, não fazemos nada
                if (string.IsNullOrEmpty(args.NewTextValue))
                    return;

                // Verificar se o texto contém apenas números
                bool isValid = Regex.IsMatch(args.NewTextValue, "^[0-9]*$");

                // Se não for válido, reverter para o valor anterior
                if (!isValid)
                {
                    entry.Text = args.OldTextValue;
                }
            }
        }
    }
}