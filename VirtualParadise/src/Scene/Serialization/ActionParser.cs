namespace VirtualParadise.Scene.Serialization
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Commands;
    using Commands.Parsing;
    using Triggers;

    #endregion

    public static class ActionParser
    {
        #region Fields

        private const char CommandChar = ',';

        private const char TriggerChar = ';';

        /// <summary>
        /// Backing field for <see cref="RegisteredCommands"/>.
        /// </summary>
        private static readonly Dictionary<string, Type> registeredCommands = new Dictionary<string, Type>();

        /// <summary>
        /// Backing field for <see cref="RegisteredTriggers"/>.
        /// </summary>
        private static readonly Dictionary<string, Type> registeredTriggers = new Dictionary<string, Type>();

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor for <see cref="ActionParser"/>.
        /// </summary>
        static ActionParser()
        {
            RegisterTrigger<ActivateTrigger>();
            RegisterTrigger<AdoneTrigger>();
            RegisterTrigger<BumpTrigger>();
            RegisterTrigger<BumpEndTrigger>();
            RegisterTrigger<CreateTrigger>();
            RegisterCommand<AmbientCommand>();
            RegisterCommand<AnimateCommand>();
            RegisterCommand<AstartCommand>();
            RegisterCommand<AstopCommand>();
            RegisterCommand<CameraCommand>();
            RegisterCommand<ColorCommand>();
            RegisterCommand<DiffuseCommand>();
            RegisterCommand<FrameRateCommand>();
            RegisterCommand<LightCommand>();
            RegisterCommand<MoveCommand>();
            RegisterCommand<NameCommand>();
            RegisterCommand<NoiseCommand>();
            RegisterCommand<NormalMapCommand>();
            RegisterCommand<OpacityCommand>();
            RegisterCommand<PictureCommand>();
            RegisterCommand<RotateCommand>();
            RegisterCommand<ScaleCommand>();
            RegisterCommand<ShearCommand>();
            RegisterCommand<SignCommand>();
            RegisterCommand<SolidCommand>();
            RegisterCommand<SoundCommand>();
            RegisterCommand<SpecularCommand>();
            RegisterCommand<SpecularMapCommand>();
            RegisterCommand<TeleportCommand>();
            RegisterCommand<TeleportXyzCommand>();
            RegisterCommand<TextureCommand>();
            RegisterCommand<VisibleCommand>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the commands registered to the parser.
        /// </summary>
        public static IEnumerable<Type> RegisteredCommands =>
            registeredCommands.Values;

        /// <summary>
        /// Gets the triggers registered to the parser.
        /// </summary>
        public static IEnumerable<Type> RegisteredTriggers =>
            registeredTriggers.Values;

        #endregion

        #region Methods

        /// <summary>
        /// Registers a command that is recognized by the parser.
        /// </summary>
        /// <typeparam name="TCommand">A <see cref="Command"/> derived type.</typeparam>
        public static void RegisterCommand<TCommand>() where TCommand : Command
        {
            RegisterCommand(typeof(TCommand));
        }

        /// <summary>
        /// Registers a command that is recognized by the parser.
        /// </summary>
        /// <param name="type">The command type.</param>
        public static void RegisterCommand(Type type)
        {
            if (!type.IsSubclassOf(typeof(Command)) && !typeof(Command).IsAssignableFrom(type)) {
                return;
            }

            if (!(type.GetCustomAttribute<CommandAttribute>() is { } command)) {
                return;
            }

            registeredCommands?.Add(command.Name.ToUpperInvariant(), type);
        }

        /// <summary>
        /// Registers a trigger that is recognized by the parser.
        /// </summary>
        /// <typeparam name="TTrigger">A <see cref="Trigger"/> derived type.</typeparam>
        public static void RegisterTrigger<TTrigger>() where TTrigger : Trigger
        {
            RegisterTrigger(typeof(TTrigger));
        }

        /// <summary>
        /// Registers a command that is recognized by the parser.
        /// </summary>
        /// <param name="type">The command type.</param>
        public static void RegisterTrigger(Type type)
        {
            if (!type.IsSubclassOf(typeof(Trigger)) && !typeof(Trigger).IsAssignableFrom(type)) {
                return;
            }

            if (!(type.GetCustomAttribute<TriggerAttribute>() is { } trigger)) {
                return;
            }

            registeredTriggers?.Add(trigger.Name.ToUpperInvariant(), type);
        }

        /// <summary>
        /// Parses an action string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="throwOnError">Optional. Whether or not the parser should throw an exception if an operation
        /// failed. Defaults to <see langword="false"/>.</param>
        /// <returns>Returns an instance of <see cref="Action"/>.</returns>
        public static Action Parse(string input, bool throwOnError = false)
        {
            return ParseAsync(input, throwOnError).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Asynchronously parses an action string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="throwOnError">Optional. Whether or not the parser should throw an exception if an operation
        /// failed. Defaults to <see langword="false"/>.</param>
        /// <returns>Returns an instance of <see cref="Action"/>.</returns>
        public static async Task<Action> ParseAsync(string input, bool throwOnError = false)
        {
            if (String.IsNullOrWhiteSpace(input)) {
                return Action.Empty;
            }

            // TODO delegate this beauty to X10D - ensure to divide by needle length
            // discovered at https://stackoverflow.com/questions/541954/how-would-you-count-occurrences-of-a-string-actually-a-char-within-a-string
            int potentialTriggerCount = input.Length - input.Replace(TriggerChar.ToString(), "").Length;

            ActionBuilder builder       = new ActionBuilder();
            StringBuilder triggerBuffer = new StringBuilder();
            List<string>  triggers      = new List<string>(potentialTriggerCount + 1); // guaranteed to not exceed this
            bool          triggerQuote  = false;

            foreach (char ch in input) {
                // split triggers into a List<string>

                await ParseTriggerCharAsync(ch, triggerBuffer, triggers, ref triggerQuote)
                   .ConfigureAwait(false);
            }

            string tmp = triggerBuffer.ToString().Trim();
            if (!String.IsNullOrWhiteSpace(tmp)) {
                // add remaining buffer to the known triggers
                triggers.Add(tmp);
            }

            triggerBuffer.Clear();

            foreach (string triggerStr in triggers) {
                string[] triggerWords =
                    triggerStr.Split(Array.Empty<char>(), 2, StringSplitOptions.RemoveEmptyEntries);

                if (triggerWords.Length == 0) {
                    continue;
                }

                string triggerWord      = triggerWords[0].ToUpperInvariant();
                string triggerRemainder = triggerWords.Length > 1 ? triggerWords[1] : String.Empty;

                if (!registeredTriggers.ContainsKey(triggerWord)) {
                    // unrecognized trigger - ignore it
                    continue;
                }

                Type    triggerType = registeredTriggers[triggerWord];
                Trigger trigger;
                try {
                    // earlier type-checking "guarantees" this to pass.
                    // it this should not throw an exception but it's good to be robust I suppose
                    trigger = Activator.CreateInstance(triggerType) as Trigger;

                    if (trigger is null) {
                        throw new NullReferenceException("Instantiated command " + triggerType.Name + " is null");
                    }
                } catch when (!throwOnError) {
                    continue;
                }

                builder.AddTrigger(trigger);

                int potentialCommandCount = triggerRemainder.Length -
                                            triggerRemainder.Replace(CommandChar.ToString(), "").Length;

                // use previously declared chars to avoid duplicate memory
                StringBuilder commandBuffer = new StringBuilder();
                List<string>  commands      = new List<string>(potentialCommandCount);
                bool          commandQuote  = false;

                foreach (char ch in triggerRemainder) {
                    await ParseCommandCharAsync(ch, commandBuffer, commands, ref commandQuote)
                       .ConfigureAwait(false);
                }

                tmp = commandBuffer.ToString().Trim();
                if (!String.IsNullOrWhiteSpace(tmp)) {
                    // add remaining buffer to the known commands
                    commands.Add(tmp);
                }

                commandBuffer.Clear();

                foreach (string commandStr in commands) {
                    string[] commandWords =
                        commandStr.Split(Array.Empty<char>(), 2, StringSplitOptions.RemoveEmptyEntries);

                    if (commandWords.Length == 0) {
                        continue;
                    }

                    string commandWord      = commandWords[0].ToUpperInvariant();
                    string commandRemainder = commandWords.Length > 1 ? commandWords[1] : String.Empty;

                    if (!registeredCommands.ContainsKey(commandWord)) {
                        if (throwOnError) {
                            throw new Exception("Command " + commandWord + " not recognized");
                        }

                        continue;
                    }

                    Type    commandType = registeredCommands[commandWord];
                    Command command;
                    try {
                        // again, earlier type-checking "guarantees" this to pass... but here we are
                        command = Activator.CreateInstance(commandType) as Command;
                        if (command is null) {
                            throw new NullReferenceException("Instantiated command " + commandType.Name + " is null");
                        }
                    } catch when (!throwOnError) {
                        continue;
                    }

                    CommandAttribute attribute = commandType.GetCustomAttribute<CommandAttribute>();
                    if (Activator.CreateInstance(attribute.Parser) is CommandParser parser) {
                        command = await parser.ParseAsync(command.GetType(), commandRemainder)
                                              .ConfigureAwait(true);
                    }

                    builder.AddCommand(command);
                }
            }

            return builder.Build();
        }

        private static Task ParseCommandCharAsync(char     ch, StringBuilder builder, List<string> commands,
                                                  ref bool quote)
        {
            switch (ch) {
                case '"':
                    quote = !quote;
                    goto default;

                case CommandChar:
                    if (!quote) {
                        string buffer = builder.ToString().Trim();
                        builder.Clear();
                        commands.Add(buffer);
                        break;
                    }

                    goto default;

                default:
                    builder.Append(ch);
                    break;
            }

            return Task.CompletedTask;
        }

        private static Task ParseTriggerCharAsync(char     ch, StringBuilder builder, List<string> triggers,
                                                  ref bool quote)
        {
            switch (ch) {
                case '"':
                    quote = !quote;
                    goto default;

                case TriggerChar:
                    if (!quote) {
                        string buffer = builder.ToString().Trim();
                        builder.Clear();
                        triggers.Add(buffer);
                        break;
                    }

                    goto default;

                default:
                    builder.Append(ch);
                    break;
            }

            return Task.CompletedTask;
        }
        
        #endregion
    }
}
