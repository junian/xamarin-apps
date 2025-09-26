using System.Windows.Input;
using Xamarin.Forms;

namespace TaskApp.Behaviors
{
    public class EventToCommandBehavior : Behavior<CheckBox>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        protected override void OnAttachedTo(CheckBox bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.CheckedChanged += OnCheckedChanged;
        }

        protected override void OnDetachingFrom(CheckBox bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.CheckedChanged -= OnCheckedChanged;
        }

        private void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter ?? (sender as CheckBox).BindingContext);
            }
        }
    }
}