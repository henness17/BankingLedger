using System;

namespace BankingLedger
{
  /// <summary>
  /// The entry point of the console line application, it handles most
  /// of the application's prompting.
  /// </summary>
  public class Program
  {
    /// <summary>
    /// The application's main loop.
    /// </summary>
    public static void Main()
    {
      Console.WriteLine(StringResources.WelcomeMessage);
      while (true)
      {
        Program.WriteUserOptions();
        string userInput = Console.ReadLine();
        if (LoginManager.UserIsLoggedIn)
        {
          switch (userInput)
          {
            case "1":
              Program.PromptDeposit();
              break;
            case "2":
              Program.PromptWithdrawal();
              break;
            case "3":
              TransactionManager.CheckBalance();
              break;
            case "4":
              TransactionManager.ViewTransactions();
              break;
            case "5":
              LoginManager.LogOut();
              break;
            default:
              Console.WriteLine(StringResources.InvalidOptionMessage);
              break;
          }
        }
        else
        {
          switch (userInput)
          {
            case "1":
              Program.PromptCreateNewAccount();
              break;
            case "2":
              Program.PromptLogUserIn();
              break;
            case "3":
              return;
            default:
              Console.WriteLine(StringResources.InvalidOptionMessage);
              break;
          }
        }
      }
    }

    #region Private Methods
    private static void PromptCreateNewAccount()
    {
      string firstName;
      while (true)
      {
        Console.WriteLine("{0}:", StringResources.FirstNameCaption);
        firstName = Console.ReadLine();
        if (Program.IsValidUserInput(firstName))
          break;
      }

      string lastName;
      while (true)
      {
        Console.WriteLine("{0}:", StringResources.LastNameCaption);
        lastName = Console.ReadLine();
        if (Program.IsValidUserInput(lastName))
          break;
      }

      string username;
      while (true)
      {
        Console.WriteLine("{0}:", StringResources.UsernameCaption);
        username = Console.ReadLine();
        if (Program.IsValidUserInput(username) && Program.IsValidNewUsername(username))
          break;
      }

      string password;
      while (true)
      {
        Console.WriteLine("{0}:", StringResources.PasswordCaption);
        password = Program.ReadPassword();
        if (Program.IsValidUserInput(password))
          break;
      }

      LoginManager.CreateNewAccount(firstName, lastName, username, password);
    }

    private static bool IsValidUserInput(string input)
    {
      if (string.IsNullOrEmpty(input))
      {
        Console.WriteLine(StringResources.InputCannotBeEmptyMessage);
        return false;
      }

      return true;
    }

    private static bool IsValidNewUsername(string username)
    {
      if (LoginManager.UserExists(username))
      {
        Console.WriteLine(StringResources.UsernameAlreadyExistsMessage);
        return false;
      }

      return true;
    }

    private static void PromptLogUserIn()
    {
      Console.WriteLine("{0}:", StringResources.UsernameCaption);
      string username = Console.ReadLine();
      Console.WriteLine("{0}:", StringResources.PasswordCaption);
      string password = Program.ReadPassword();
      if (LoginManager.LogIn(username, password))
        Console.WriteLine(StringResources.LoginSuccessfulMessage);
      else
        Console.WriteLine(StringResources.LoginUnsuccessfulMessage);
    }

    private static void PromptDeposit()
    {
      Console.WriteLine(StringResources.HowManyDollarsToDepositMessage);
      string inputAmount = Console.ReadLine();
      if (double.TryParse(inputAmount, out double amount))
      {
        try
        {
          TransactionManager.Deposit(amount);
          Console.WriteLine(StringResources.DepositSuccessfulMessage);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
      else
        Console.WriteLine(StringResources.EnterANumberMessage);
    }

    private static void PromptWithdrawal()
    {
      Console.WriteLine(StringResources.HowManyDollarsToWithdrawMessage);
      string inputAmount = Console.ReadLine();
      if (double.TryParse(inputAmount, out double amount))
      {
        try
        {
          TransactionManager.Withdraw(amount);
          Console.WriteLine(StringResources.WithdrawalSuccessfulMessage);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
      else
        Console.WriteLine(StringResources.EnterANumberMessage);
    }

    private static string ReadPassword()
    {
      string password = string.Empty;
      while (true)
      {
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
        {
          password = password.Substring(0, password.Length - 1);
          Console.Write("\b \b");
        }
        else if (key.Key == ConsoleKey.Enter)
        {
          Console.WriteLine();
          break;
        }
        else if (key.Key != ConsoleKey.Backspace)
        {
          password += key.KeyChar;
          Console.Write("*");
        }
      }

      return password;
    }

    private static void WriteUserOptions()
    {
      Console.WriteLine("---------------------------------------------");
      if (LoginManager.UserIsLoggedIn)
      {
        Console.WriteLine(string.Format(
          StringResources.HelloUserMessage,
          LoginManager.CurrentUser.FirstName, 
          LoginManager.CurrentUser.LastName));
        Console.WriteLine("[1] {0}", StringResources.DepositCaption);
        Console.WriteLine("[2] {0}", StringResources.WithdrawCaption);
        Console.WriteLine("[3] {0}", StringResources.CheckBalanceCaption);
        Console.WriteLine("[4] {0}", StringResources.ViewTransactionHistoryCaption);
        Console.WriteLine("[5] {0}", StringResources.LogOutCaption);
      }
      else
      {
        Console.WriteLine(StringResources.NotLoggedInChooseOptionMessage);
        Console.WriteLine("[1] {0}", StringResources.CreateNewAccountCaption);
        Console.WriteLine("[2] {0}", StringResources.LogInCaption);
        Console.WriteLine("[3] {0}", StringResources.CloseCaption);
      }
    }
    #endregion
  }
}
