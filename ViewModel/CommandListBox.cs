﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewModel
{
    public class CommandListBox : ListBox, ICommandSource
    {
        public CommandListBox()
        : base()
        {
        }

        #region ICommand Interface Members

        // Make Command a dependency property so it can use databinding.
        public static readonly DependencyProperty CommandProperty =
    DependencyProperty.Register(
    "Command",
    typeof(ICommand),
    typeof(CommandListBox),
    new PropertyMetadata((ICommand)null,
    new PropertyChangedCallback(CommandChanged)));

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty ExecutedProperty =
        DependencyProperty.Register(
        "Executed",
        typeof(object),
        typeof(CommandListBox),
        new PropertyMetadata((object)null));

        public object Executed
        {
            get
            {
                return (object)GetValue(ExecutedProperty);
            }
            set
            {
                SetValue(ExecutedProperty, value);
            }
        }

        public static readonly DependencyProperty CanExecuteProperty =
        DependencyProperty.Register(
        "CanExecute",
        typeof(object),
        typeof(CommandListBox),
        new PropertyMetadata((object)null));

        public object CanExecute
        {
            get
            {
                return (object)GetValue(CanExecuteProperty);
            }
            set
            {
                SetValue(CanExecuteProperty, value);
            }
        }

        public static readonly DependencyProperty CommandTargetProperty =
        DependencyProperty.Register(
        "CommandTarget",
        typeof(IInputElement),
        typeof(CommandListBox),
        new PropertyMetadata((IInputElement)null));

        public IInputElement CommandTarget
        {
            get
            {
                return (IInputElement)GetValue(CommandTargetProperty);
            }
            set
            {
                SetValue(CommandTargetProperty, value);
            }
        }

        public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(
        "CommandParameter",
        typeof(object),
        typeof(CommandListBox),
        new PropertyMetadata((object)null));

        public object CommandParameter
        {
            get
            {
                return (object)GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        #endregion

        // Command dependency property change callback.
        private static void CommandChanged(DependencyObject d,
    DependencyPropertyChangedEventArgs e)
        {
            CommandListBox clb = (CommandListBox)d;
            clb.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }
        // Add a new command to the Command Property.
        private void HookUpCommand(ICommand oldCommand, ICommand newCommand)
        {
            // If oldCommand is not null, then we need to remove the handlers.
            if (oldCommand != null)
            {
                RemoveCommand(oldCommand, newCommand);
            }
            AddCommand(oldCommand, newCommand);
        }

        // Remove an old command from the Command Property.
        private void RemoveCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = CanExecuteChanged;
            oldCommand.CanExecuteChanged -= handler;

        }

        // Add the command.
        private void AddCommand(ICommand oldCommand, ICommand newCommand)
        {
            EventHandler handler = new EventHandler(CanExecuteChanged);
            canExecuteChangedHandler = handler;
            if (newCommand != null)
            {
                newCommand.CanExecuteChanged += canExecuteChangedHandler;
            }
        }
        private void CanExecuteChanged(object sender, EventArgs e)
        {

            if (this.Command != null)
            {
                RoutedCommand command = this.Command as RoutedCommand;

                // If a RoutedCommand.
                if (command != null)
                {
                    if (command.CanExecute(CommandParameter, CommandTarget))
                    {
                        this.IsEnabled = true;
                    }
                    else
                    {
                        this.IsEnabled = false;
                    }
                }
                // If a not RoutedCommand.
                else
                {
                    if (Command.CanExecute(CommandParameter))
                    {
                        this.IsEnabled = true;
                    }
                    else
                    {
                        this.IsEnabled = false;
                    }
                }
            }
        }

        private static EventHandler canExecuteChangedHandler;

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (this.Command != null)
            {
                RoutedCommand command = Command as RoutedCommand;

                if (command != null)
                {
                    command.Execute(CommandParameter, CommandTarget);
                }
                else
                {
                    ((ICommand)Command).Execute(CommandParameter);
                }
            }
        }
    }
}
